using UnityEngine;
using System.Collections;

namespace PostProcessing
{
    [ExecuteInEditMode]
    public class Bloom : MonoBehaviour
    {
        [SerializeField]
        private float radius = 1f;

        [SerializeField]
        private float threshold = 1f;
        [SerializeField]
        private float blurScale = 1f;

        [SerializeField]
        private float intensity = 1f;

        private Material material;
        
        private int iterations;
        private static int maxIterations = 16;

        RenderTexture[] blurBuffer1 = new RenderTexture[maxIterations];
        RenderTexture[] blurBuffer2 = new RenderTexture[maxIterations];

        void OnEnable ()
        {
            material = new Material(Shader.Find("Hidden/Bloom Shader"));
        }

        void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            material.SetFloat("_Threshold", threshold);
            material.SetFloat("_BlurScale", blurScale);
            material.SetFloat("_Intensity", intensity);

            RenderTexture prefiltered = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.DefaultHDR);

            Graphics.Blit(source, prefiltered, material, 0);

            RenderTexture previousTexture = prefiltered;

            int logh = (int)(Mathf.Log(source.height, 2) + radius - 8);
            iterations = Mathf.Clamp(logh, 1, maxIterations);

            //  Downsample
            for (int i = 0; i < iterations; i++)
            {
                blurBuffer1[i] = RenderTexture.GetTemporary(previousTexture.width / 2, previousTexture.height / 2, 0, RenderTextureFormat.DefaultHDR);

                int pass = (i == 0 ? 1 : 2);
                Graphics.Blit(previousTexture, blurBuffer1[i], material, pass);

                previousTexture = blurBuffer1[i];
            }

            //  Upsample and combine
            for (int i = iterations - 2; i >= 0; i--)
            {
                RenderTexture basetex = blurBuffer1[i];
                material.SetTexture("_BaseTex", basetex);

                blurBuffer2[i] = RenderTexture.GetTemporary(basetex.width, basetex.height, 0, RenderTextureFormat.DefaultHDR);

                Graphics.Blit(previousTexture, blurBuffer2[i], material, 3);
                previousTexture = blurBuffer2[i];
            }

            material.SetTexture("_BaseTex", source);
            Graphics.Blit(previousTexture, destination, material, 4);

            RenderTexture.ReleaseTemporary(prefiltered);

            for (int i = 0; i < iterations; i++)
            {
                RenderTexture.ReleaseTemporary(blurBuffer1[i]);
                RenderTexture.ReleaseTemporary(blurBuffer2[i]);
            }
        }

        void OnDisable ()
        {
            DestroyImmediate(material);
        }
    }
}
