Shader "Hidden/NoiseGenerator"
{
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 uv : TEXCOORD0;
            };

            float IGN(float2 p)
            {
                float3 magic = float3(0.06711056, 0.00583715, 52.9829189);
                return frac(magic.z * frac(dot(p, magic.xy)));
            }

            Varyings Vertex(Attributes input)
            {
                Varyings output;
                VertexPositionInputs vertexInputs = GetVertexPositionInputs(input.positionOS);
                output.positionHCS = vertexInputs.positionCS;
                output.uv = vertexInputs.positionNDC;
                return output;
            }

            float4 Fragment(Varyings input) : SV_Target
            {
                float2 uv = (input.positionHCS.xy / input.positionHCS.w);
                uv = floor(uv / 2.0f);
                float k = IGN(uv);
                return float4(k, k, k, 1.0f);
            }
            ENDHLSL
        }
    }
}