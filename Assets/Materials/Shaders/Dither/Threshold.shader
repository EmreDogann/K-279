Shader "Hidden/Dither/Threshold"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BG ("Background", Color) = (0,0,0,0)
        _FG ("Foreground", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment

            #pragma shader_feature _ USE_RAMP_TEX

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 positionHCS : SV_POSITION;
            };

            Varyings Vertex(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS);
                output.uv = input.uv;
                
                return output;
            }

            TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

            TEXTURE2D(_ColorRampTex);
			SAMPLER(sampler_ColorRampTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _BG;
                float4 _FG;
            CBUFFER_END

            float4 Fragment (Varyings input) : SV_Target
            {
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                #if USE_RAMP_TEX
                    // Doesn't work, need to use luminance of pixel as x-axis UV instead.
					float4 output = SAMPLE_TEXTURE2D(_ColorRampTex, sampler_ColorRampTex, float2(col.r, 0.5f));
                #else
            		float4 output = lerp(_BG, _FG, round(col.r));
            	#endif

                return output;
            }
            ENDHLSL
        }
    }
}
