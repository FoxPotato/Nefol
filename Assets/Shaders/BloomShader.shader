Shader "Hidden/Bloom Shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		CGINCLUDE

		#define USE_RGBM defined(SHADER_API_MOBILE)

		sampler2D _MainTex;
		sampler2D _BaseTex;
		float2 _MainTex_TexelSize;
		float2 _BaseTex_TexelSize;

		uniform half _Threshold = 1;
		uniform half _BlurScale = 1;
		uniform half _Intensity = 1;

		float brightness(float3 color)
		{
			return max(max(color.r, color.g), color.b);
		}

		half3 downsampleFilter(float2 uv)
		{
			float4 d = _MainTex_TexelSize.xyxy * float4(-1, -1, +1, +1);

			half3 s;
			s = tex2D(_MainTex, uv + d.xy);
			s += tex2D(_MainTex, uv + d.zy);
			s += tex2D(_MainTex, uv + d.xw);
			s += tex2D(_MainTex, uv + d.zw);

			return 0.25 * s;
		}
		
		half3 downsampleAntiFlickerFilter(float2 uv)
		{
			float4 d = _MainTex_TexelSize.xyxy * float4(-1, -1, +1, +1);

			half3 s1 = tex2D(_MainTex, uv + d.xy);
			half3 s2 = tex2D(_MainTex, uv + d.zy);
			half3 s3 = tex2D(_MainTex, uv + d.xw);
			half3 s4 = tex2D(_MainTex, uv + d.zw);

			// Karis's luma weighted average (using brightness instead of luma)
			half s1w = 1 / (brightness(s1) + 1);
			half s2w = 1 / (brightness(s2) + 1);
			half s3w = 1 / (brightness(s3) + 1);
			half s4w = 1 / (brightness(s4) + 1);
			half one_div_wsum = 1 / (s1w + s2w + s3w + s4w);

			return (s1 * s1w + s2 * s2w + s3 * s3w + s4 * s4w) * one_div_wsum;
		}

		half3 upsampleFilter(float2 uv)
		{
			float4 d = _MainTex_TexelSize.xyxy * float4(-1, -1, +1, +1) * _BlurScale;

			half3 s;
			s = tex2D(_MainTex, uv + d.xy);
			s += tex2D(_MainTex, uv + d.zy);
			s += tex2D(_MainTex, uv + d.xw);
			s += tex2D(_MainTex, uv + d.zw);

			return s * (1.0 / 4);
		}

		struct appdata
		{
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		struct v2f_multitex
		{
			float4 vertex : SV_POSITION;
			float2 uv_main : TEXCOORD0;
			float2 uv_base : TEXCOORD1;
		};

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv = v.texcoord;
			return o;
		}

		v2f_multitex vert_multitex(appdata v)
		{
			v2f_multitex o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.uv_main = v.texcoord;
			o.uv_base = v.texcoord;
			return o;
		}

		fixed3 frag_prefilter (v2f i) : SV_Target
		{
			fixed3 col = tex2D(_MainTex, i.uv).rgb;
			return step(_Threshold, brightness(col)) * col;
		}

		fixed3 frag_downsampleAntiFlicker (v2f i) : SV_Target
		{
			return downsampleAntiFlickerFilter(i.uv);
		}

		fixed3 frag_downsample(v2f i) : SV_Target
		{
			return downsampleFilter(i.uv);
		}

		fixed3 frag_upsample(v2f_multitex i) : SV_Target
		{
			return upsampleFilter(i.uv_main);
		}

		fixed4 frag_final(v2f_multitex i) : SV_Target
		{
			return float4(_Intensity * tex2D(_MainTex, i.uv_main).rgb + tex2D(_BaseTex, i.uv_base).rgb, 1);
		}

		ENDCG

		Pass	//	0
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag_prefilter
			ENDCG
		}

		Pass	//	1
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag_downsampleAntiFlicker
			ENDCG
		}

		Pass	//	2
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag_downsample
			ENDCG
		}

		Pass	//	3
		{
			CGPROGRAM
			#pragma vertex vert_multitex
			#pragma fragment frag_upsample
			ENDCG
		}

		Pass	//4
		{
			CGPROGRAM
			#pragma vertex vert_multitex
			#pragma fragment frag_final
			ENDCG
		}
	}
}
