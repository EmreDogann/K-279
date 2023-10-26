Shader "Custom/TessellationLit"
{
    Properties
    {
        [Header(Textures)] [Space]
        [MainTexture] _MainTex("Main Texture", 2D) = "white" {}
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [NoScaleOffset] _FogCube("Fog Cube Texture", CUBE) = "" {}
        [NoScaleOffset] _WaterNormal1("Water Normal Map 1", 2D) = "bump" {}
        [NoScaleOffset] _WaterNormal2("Water Normal Map 2", 2D) = "bump" {}
        _SimulationNormalStrength("Simulation Normal Strength", Float) = 1
        _NormalMapStrength("Water Normal Map Strength", Float) = 1
        _NormalMapSize("Water Normal Map Size", Float) = 1
        [NoScaleOffset] _WaterFlowMap("Water Flow Map (RG, A noise)", 2D) = "black" {}
        _Tiling ("Tiling", Float) = 1
        _Speed ("Speed", Float) = 1
        _FlowStrength ("Flow Strength", Float) = 1
        _FlowOffset ("Flow Offset", Float) = 0
        _UJump ("Flow Map U jump per phase", Range(-0.25, 0.25)) = 0.25
        _VJump ("Flow Map V jump per phase", Range(-0.25, 0.25)) = 0.25


        [Header(Water Surface)] [Space]
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Specular ("Specular", Range(0,1)) = 0.0
        _Smoothness("Smoothness", Range(0,1)) = 0.5
        _WaterShallowColor ("Water Shallow Color", Color) = (0, 0, 0, 0)
        _WaterDeepColor ("Water Deep Color", Color) = (0, 0, 0, 0)
        _WaterDensity ("Water Fog Density", Range(0, 2)) = 0.1

        [Header(Tessellation)] [Space] [NoScaleOffset] _HeightMap("Tessellation Height Map", 2D) = "black" {}
        _HeightMapAltitude("Height Map Altitude", Range(0, 100)) = 1
        // This keyword enum allows us to choose between partitioning modes. It's best to try them out for yourself
        [KeywordEnum(INTEGER, FRAC_EVEN, FRAC_ODD, POW2)] _PARTITIONING("Partition algoritm", Float) = 0
        // This allows us to choose between tessellation factor methods
        [KeywordEnum(CONSTANT, WORLD, SCREEN, WORLD_WITH_DEPTH)] _TESSELLATION_FACTOR("Tessellation mode", Float) = 0
        // This factor is applied differently per factor mode
        //  Constant: not used
        //  World: this is the ideal edge length in world units. The algorithm will try to keep all edges at this value
        //  Screen: this is the ideal edge length in screen pixels. The algorithm will try to keep all edges at this value
        //  World with depth: similar to world, except the edge length is decreased quadratically as the camera gets closer 
        _TessellationFactor("Tessellation factor", Range(0, 100)) = 1
        // This value is added to the tessellation factor. Use if your model should be more or less tessellated by default
        _TessellationBias("Tessellation bias", Range(0, 100)) = 0
        // A tolerance to frustum culling. Increase if triangles disappear when on screen
        _FrustumCullTolerance("Frustum cull tolerance", Range(0, 100)) = 0.01
        // A tolerance to back face culling. Increase if holes appear on your mesh
        _BackFaceCullTolerance("Back face cull tolerance", Range(0, 100)) = 0.01
        // This keyword selects a tessellation smoothing method
        //  Flat: no smoothing
        //  Phong: use Phong tessellation, as described here: http://www.klayge.org/material/4_0/PhongTess/PhongTessellation.pdf'
        [KeywordEnum(FLAT, PHONG, BEZIER_QUAD_NORMALS)] _TESSELLATION_SMOOTHING("Smoothing mode", Float) = 0
        // A factor to interpolate between flat and the selected smoothing method
        _TessellationSmoothing("Smoothing factor", Range(0, 1)) = 0.75


        [Header(Screen Space Relfections)] [Space]
        _MaxDistance("Max Trace Distance", Range(0, 100)) = 15.0
        _Resolution("Resolution", Range(0, 1)) = 0.1
        [IntRange] _Steps("Steps", Range(0, 100)) = 10
        _Thickness("Thickness", Range(0, 10)) = 0.5


        [Header(Wireframe)] [Space] [Toggle(ENABLE_WIREFRAME)] _Wireframe("Enable Wireframe", Float) = 0
        _WireframeColor ("Wireframe Color", Color) = (0, 0, 0, 1)
        _WireframeSmoothing ("Wireframe Smoothing", Range(0, 10)) = 1
        _WireframeThickness ("Wireframe Thickness", Range(0, 10)) = 1

        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
            "PreviewType" = "Plane"
            "ShaderModel" = "5.0"
        }

        Pass
        {
            Name "Tessellation"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
            Cull Back
            ZWrite On
            ZTest LEqual
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma target 5.0 // 5.0 required for tessellation.
            #pragma exclude_renderers d3d11_9x
            #pragma exclude_renderers d3d9

            #pragma vertex Vertex
            #pragma fragment Fragment

            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF

            // Material keywords - Lighting
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            // #pragma multi_compile_fragment _ _SHADOWS_SOFT
            // #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            #pragma multi_compile_fragment _ _LIGHT_COOKIES
            #pragma multi_compile _ _FORWARD_PLUS

            // Material keywords - Tessellation
            #pragma shader_feature_local _PARTITIONING_INTEGER _PARTITIONING_FRAC_EVEN _PARTITIONING_FRAC_ODD _PARTITIONING_POW2
            #pragma shader_feature_local _TESSELLATION_SMOOTHING_FLAT _TESSELLATION_SMOOTHING_PHONG _TESSELLATION_SMOOTHING_BEZIER_QUAD_NORMALS
            #pragma shader_feature_local _TESSELLATION_FACTOR_CONSTANT _TESSELLATION_FACTOR_WORLD _TESSELLATION_FACTOR_SCREEN _TESSELLATION_FACTOR_WORLD_WITH_DEPTH

            // Material keywords - Wireframe Rendering
            #pragma shader_feature_local ENABLE_WIREFRAME

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"

            Texture2D _MainTex;
            SamplerState sampler_MainTex;

            Texture2D _WaterNormal1;
            SamplerState sampler_WaterNormal1;
            Texture2D _WaterNormal2;
            SamplerState sampler_WaterNormal2;
            Texture2D _WaterFlowMap;
            SamplerState sampler_WaterFlowMap;

            Texture2D _HeightMap;
            SamplerState sampler_HeightMap;
            TextureCube _FogCube;
            SamplerState sampler_FogCube; // Skybox cube texture

            // CBUFFER section needed for SRP batching.
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _BaseColor;
                float _Smoothness;
                float _Specular;
                float _Metallic;

                float4 _WaterShallowColor;
                float4 _WaterDeepColor;
                float _WaterDensity;

                float _UJump;
                float _VJump;
                float _Tiling;
                float _Speed;
                float _FlowStrength;
                float _FlowOffset;

                float _NormalMapStrength;
                float _NormalMapSize;
                float _SimulationNormalStrength;
                float4 _HeightMap_TexelSize;
                float _HeightMapAltitude;

                float3 _FactorEdge1;
                float _FactorInside;
                float _TessellationFactor;
                float _TessellationBias;
                float _TessellationSmoothing;
                float _FrustumCullTolerance;
                float _BackFaceCullTolerance;

                float _MaxDistance;
                float _Resolution;
                int _Steps;
                float _Thickness;

                float4 _WireframeColor;
                float _WireframeSmoothing;
                float _WireframeThickness;

                float _HeightMap_Width;
                float _HeightMap_Height;

                // Extract scale from object to world matrix.
                static float3 scale = float3(
                    length(unity_ObjectToWorld._m00_m10_m20),
                    length(unity_ObjectToWorld._m01_m11_m21),
                    length(unity_ObjectToWorld._m02_m12_m22)
                );
            CBUFFER_END

            #include "Assets/Materials/Shaders/Common.hlsl"
            #include "Tessellation.hlsl"

            struct Interpolators
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : INTERNALTESSPOS;
                float3 normalWS : NORMAL;
                float2 uv : TEXCOORD0;
                DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 1);
                float2 uv_MainTex : TEXCOORD2;
                float3 normalOS : TEXCOORD4;
                float4 tangentWS : TEXCOORD5;
                float3 viewDir : TEXCOORD6;
                float fogCoords : TEXCOORD7;
                float2 barycentricCoordinates : TEXCOORD8;
                float4 screenPos : TEXCOORD9;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            // #include "Wireframe.hlsl"

            TessellationControlPoint Vertex(Attributes input)
            {
                TessellationControlPoint output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);

                VertexPositionInputs positionInputs = GetVertexPositionInputs(input.vertexOS);
                VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

                output.normalOS = input.normalOS.xyz;

                output.positionCS = positionInputs.positionCS;
                output.positionWS = positionInputs.positionWS;
                output.normalWS = normalInputs.normalWS;
                output.tangentWS = float4(normalInputs.tangentWS, input.tangentOS.w); // tangent.w contains bitangent multiplier;
                output.uv = input.uv;
                output.uv_MainTex = TRANSFORM_TEX(input.uv, _MainTex);
                output.lightMap = input.lightMap;

                return output;
            }

            // The domain function runs once per vertex in the final, tessellated mesh.
            // Use it to reposition vertices and prepare for the fragment stage.
            [domain(INPUT_TYPE)] // Signal we're inputting triangles.
            Interpolators Domain(TessellationFactors factors, OutputPatch<TessellationControlPoint, CONTROL_POINTS> patch,
     float3 barycentricCoordinates : SV_DomainLocation)
            {
                // Setup instancing and stereo support (for VR)
                UNITY_SETUP_INSTANCE_ID(patch[0]);
                UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                Interpolators output = (Interpolators)0;
                // Calculate smoothed position, normal, and tangent
                // This rounds a triangle to smooth model silhouettes and improve normal interpolation
                // It can use either flat (no smoothing), Phong, or bezier-based smoothing, depending on material settings
                #if defined(_TESSELLATION_SMOOTHING_PHONG)
                    output.positionWS = CalculatePhongPosition(barycentricCoordinates, _TessellationSmoothing,
                        patch[0].positionWS, patch[0].normalWS, patch[1].positionWS, patch[1].normalWS, patch[2].positionWS, patch[2].normalWS);
                #elif defined(_TESSELLATION_SMOOTHING_BEZIER_LINEAR_NORMALS) || defined(_TESSELLATION_SMOOTHING_BEZIER_QUAD_NORMALS)
                    output.positionWS = CalculateBezierPosition(barycentricCoordinates, _TessellationSmoothing, factors.bezierPoints, patch[0].positionWS, patch[1].positionWS, patch[2].positionWS);
                #else
                BARYCENTRIC_INTERPOLATE(positionWS);
                #endif

                #if defined(_TESSELLATION_SMOOTHING_BEZIER_QUAD_NORMALS)
                    float3 normalWS, tangentWS;
                    CalculateBezierNormalAndTangent(barycentricCoordinates, _TessellationSmoothing, factors.bezierPoints,
                        patch[0].normalWS, patch[0].tangentWS.xyz, patch[1].normalWS, patch[1].tangentWS.xyz, patch[2].normalWS, patch[2].tangentWS.xyz,
                        normalWS, tangentWS);
                #else
                BARYCENTRIC_INTERPOLATE(normalWS);
                    BARYCENTRIC_INTERPOLATE(tangentWS.xyz);
                float3 normalWS = normalize(output.normalWS);
                float3 tangentWS = normalize(output.tangentWS.xyz);
                #endif

                BARYCENTRIC_INTERPOLATE(normalOS);
                BARYCENTRIC_INTERPOLATE(uv);
                BARYCENTRIC_INTERPOLATE(uv_MainTex);

                // Apply height map.
                const float height = SAMPLE_TEXTURE2D_LOD(_HeightMap, sampler_HeightMap, output.uv, 0).r * _HeightMapAltitude;
                output.positionWS += output.normalWS * height;

                output.normalWS = normalWS;
                output.tangentWS = float4(tangentWS, patch[0].tangentWS.w);
                output.positionCS = TransformWorldToHClip(output.positionWS);
                output.screenPos = ComputeScreenPos(output.positionCS);
                output.fogCoords = ComputeFogFactor(output.positionCS.z);
                output.viewDir = GetWorldSpaceViewDir(output.positionWS);

                float4 lightMap;
                    BARYCENTRIC_INTERPOLATE_NOOUT(lightMap);
                OUTPUT_LIGHTMAP_UV(lightMap, unity_LightmapST, output.lightmapUV);
                OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

                return output;
            }

            half3 GetViewPositionFromDepth(float2 uv)
            {
                #if UNITY_REVERSED_Z
                    real depth = SampleSceneDepth(uv);
                #else
                    // Adjust z to match NDC for OpenGL
                    real depth = lerp(UNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(uv));
                #endif

                // depth = LinearEyeDepth(depth, _ZBufferParams);
                float3 worldPos = ComputeWorldSpacePosition(uv, depth, UNITY_MATRIX_I_VP);
                return TransformWorldToView(worldPos);
            }

            float4 Fragment(Interpolators input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv_MainTex);
                color *= _BaseColor;

                // Fog Reference: https://github.com/keijiro/KinoFog/blob/master/Assets/Kino/Fog/Shader/Fog.shader
                // https://www.reddit.com/r/Unity3D/comments/7wm02n/perfect_fog_for_your_game_trouble_matching_fog/
                // float4 fogCube = SAMPLE_TEXTURECUBE(_FogCube, sampler_FogCube, -viewDir);

                // color = float4(MixFogColor(color.rgb, fogCube.rgb, input.fogCoords), color.a);

                #if defined(ENABLE_WIREFRAME)
                // For Wireframe.
                float3 barys;
                barys.xy = input.barycentricCoordinates;
                barys.z = 1 - barys.x - barys.y;
                float3 deltas = fwidth(barys); // Keep the line width constant in screen space using screen-space derivative.
                float3 smoothing = deltas * _WireframeSmoothing;
                float3 thickness = deltas * _WireframeThickness;

                barys = smoothstep(thickness, thickness + smoothing, barys);
                float minBary = min(barys.x, min(barys.y, barys.z));
                color = lerp(_WireframeColor, color, minBary);
                #endif

                float3 flow = SAMPLE_TEXTURE2D(_WaterFlowMap, sampler_WaterFlowMap, input.uv * _Tiling).rgb;
                flow.xy = flow.xy * 2 - 1; // Map is in 0-1 range. We have to map it back to -1-1 range using *2-1.
                flow *= _FlowStrength;
                float noise = SAMPLE_TEXTURE2D(_WaterFlowMap, sampler_WaterFlowMap, input.uv * _Tiling).a; // Greyscale noise in alpha channel.
                float time = _Time.x * _Speed + noise;
                float2 jump = float2(_UJump, _VJump);
                float3 flowUVW_A = FlowUVW(input.uv, flow.xy, jump, _FlowOffset, _Tiling, time, false);
                float3 flowUVW_B = FlowUVW(input.uv, flow.xy, jump, _FlowOffset, _Tiling, time, true);

                float3 waterNormal1_A = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal1, sampler_WaterNormal1, flowUVW_A.xy * _NormalMapSize), _NormalMapStrength) * flowUVW_A.z;
                float3 waterNormal2_A = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal2, sampler_WaterNormal2, flowUVW_A.xy * _NormalMapSize - time * 2), _NormalMapStrength) * flowUVW_A.z;

                float3 waterNormal1_B = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal1, sampler_WaterNormal1, flowUVW_B.xy * _NormalMapSize), _NormalMapStrength) * flowUVW_B.z;
                float3 waterNormal2_B = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal2, sampler_WaterNormal2, flowUVW_B.xy * _NormalMapSize - time * 2), _NormalMapStrength) * flowUVW_B.z;
                // float3 normalTS = BlendNormal(BlendNormal(waterNormal1_A, waterNormal1_B), BlendNormal(waterNormal2_A, waterNormal2_B)) * flow.z;
                float3 normalTS = ((waterNormal1_A + waterNormal1_B) + (waterNormal2_A + waterNormal2_B)) * flow.z;
                normalTS = BlendNormal(normalTS, GenerateNormalFromHeightMap(input.uv));

                float3x3 tangentToWorld = CreateTangentToWorld(input.normalWS, input.tangentWS.xyz, input.tangentWS.w);
                float3 normalWS = TransformTangentToWorld(normalTS, tangentToWorld);

                half3 normViewDir = normalize(input.viewDir);
                InputData lightingInput = (InputData)0; // Info about position and orientation of mesh at current fragment.
                lightingInput.positionWS = input.positionWS;
                lightingInput.normalWS = normalWS;
                lightingInput.viewDirectionWS = normViewDir;
                lightingInput.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
                lightingInput.bakedGI = SAMPLE_GI(input.lightmapUV, input.vertexSH, lightingInput.normalWS);

                half4 ssrUV = half4(1,1,1,1);
                #if defined(_ENVIRONMENTREFLECTIONS)
                half3 normalVS = TransformWorldToViewNormal(normalWS);
                half3 reflectDirVS = reflect(-normViewDir, normalVS);
                half4 positionTo = half4(input.viewDir, 1.0);

                half4 startView = half4(input.viewDir + (reflectDirVS * 0), 1.0);
                half4 endView = half4(input.viewDir + (reflectDirVS * _MaxDistance), 1.0);

                half4 startFrag = startView;
                startFrag = TransformWViewToHClip(startFrag);
                startFrag.xyz /= startFrag.w;
                startFrag.xy = startFrag.xy * 0.5 + 0.5;
                startFrag.xy *= _ScreenParams.xy;

                half4 endFrag = endView;
                endFrag = TransformWViewToHClip(endFrag);
                endFrag.xyz /= endFrag.w;
                endFrag.xy = endFrag.xy * 0.5 + 0.5;
                endFrag.xy *= _ScreenParams.xy;

                // First Pass (Rough Pass)
                half2 curFrag = startFrag.xy;
                ssrUV.xy = curFrag / _ScreenParams.xy;

                // How many pixels the current step takes.
                float deltaX = endFrag.x - startFrag.x;
                float deltaY = endFrag.y - startFrag.y;

                // Decide which delta to use for current step.
                float useX = abs(deltaX) >= abs(deltaY) ? 1 : 0;
                float delta = lerp(abs(deltaY), abs(deltaX), useX) * clamp(_Resolution, 0, 1);

                // How much to increment the X and Y positions by. Uses bigger of the two deltas.
                half2 increment = half2(deltaX, deltaY) / max(delta, 0.001);

                float search0 = 0; // Remembers the last position of the line where the ray missed/didn't intersect with geometry.
                float search1 = 0; // Ranges from 0-1. 0: Start fragment. 1: End fragment.

                int hit0 = 0; // Indicates intersection during first pass.
                int hit1 = 0; // Indicates intersection during second pass.

                float viewDistance = startView.z; // How far away from the camera the current point on the ray is.
                float depth = _Thickness; // View distance difference between current ray and scene position.

                [unroll(10)]
                for (int i = 0; i < int(delta); ++i)
                {
                    curFrag += increment;
                    ssrUV.xy = curFrag / _ScreenParams.xy;
                    positionTo.xyz = GetViewPositionFromDepth(ssrUV.xy);

                    search1 = lerp((curFrag.y - startFrag.y) / deltaY, (curFrag.x - startFrag.x) / deltaX, useX);
                    viewDistance = (startView.y * endView.y) / lerp(endView.y, startView.y, search1);
                    depth = viewDistance - positionTo.y;

                    if (depth > 0 && depth < _Thickness)
                    {
                        hit0 = 1;
                        break;
                    } else
                    {
                        search0 = search1;
                    }

                }

                search1 = search0 + ((search1 - search0) / 2);

                float steps = _Steps;
                steps *= hit0;

                // Second Pass (Refinement Pass)
                [unroll(10)]
                for (int i = 0; i < steps; ++i)
                {
                    curFrag = lerp(startFrag.xy, endFrag.xy, search1);
                    ssrUV.xy = curFrag / _ScreenParams.xy;
                    positionTo.xyz = GetViewPositionFromDepth(ssrUV.xy);

                    viewDistance = (startView.y * endView.y) / lerp(endView.y, startView.y, search1);
                    depth = viewDistance - positionTo.y;

                    if (depth > 0 && depth < _Thickness)
                    {
                        hit1 = 1;
                        search1 = search0 + ((search1 - search0) / 2);
                    } else
                    {
                        float temp = search1;
                        search1 = search1 + ((search1 - search0) / 2);
                        search0 = temp;
                    }
                }

                float visibility = hit1
                                 * positionTo.w
                                 * (1 - max(dot(-normViewDir, reflectDirVS), 0))
                                 * (1 - clamp(depth / _Thickness, 0, 1))
                                 * (1 - clamp(length(positionTo - input.viewDir) / _MaxDistance, 0, 1))
                                 * (ssrUV.x < 0 || ssrUV.x > 1 ? 0 : 1)
                                 * (ssrUV.y < 0 || ssrUV.y > 1 ? 0 : 1);

                visibility = clamp(visibility, 0, 1);
                ssrUV.ba = visibility;

                // half4 reflectData = SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, reflectDir, _Smoothness);
                // half3 reflectionProbeColor = DecodeHDREnvironment(reflectData, unity_SpecCube0_HDR);
                // color *= reflectionProbeColor;
                #endif

                #if defined(_SPECULARHIGHLIGHTS)
                    half3 specular = _Specular;
                #else
                half3 specular = 0;
                #endif

                float4 colorBelowWater = ColorBelowWater(input.screenPos);
                SurfaceData surfaceInput = (SurfaceData)0; // Holds info about the surface material's physical properties (e.g. color).
                surfaceInput.albedo = color;
                surfaceInput.alpha = colorBelowWater.a;
                surfaceInput.emission = colorBelowWater * (1 - color.a);
                surfaceInput.specular = specular;
                surfaceInput.metallic = _Metallic;
                surfaceInput.smoothness = _Smoothness;
                surfaceInput.normalTS = normalTS;
                surfaceInput.occlusion = 1;

                float4 finalColor = UniversalFragmentPBR(lightingInput, surfaceInput);
                // finalColor.a = 1;
                return finalColor;
            }
            ENDHLSL
        }

        // Deferred Rendering Pass
        Pass
        {
            Name "Water G-Buffer"
            Tags
            {
                "LightMode" = "UniversalGBuffer"
            }
            Cull Back
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 5.0 // 5.0 required for tessellation.
            #pragma exclude_renderers d3d11_9x
            #pragma exclude_renderers d3d9

            #pragma vertex Vertex
            #pragma fragment Fragment

            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF

            // Material keywords - Lighting
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            // #pragma multi_compile_fragment _ _SHADOWS_SOFT
            // #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            #pragma multi_compile_fragment _ _LIGHT_COOKIES
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
            #pragma multi_compile_fragment _ _DEFERRED_MIXED_LIGHTING

            // Material keywords - Tessellation
            #pragma shader_feature_local _PARTITIONING_INTEGER _PARTITIONING_FRAC_EVEN _PARTITIONING_FRAC_ODD _PARTITIONING_POW2
            #pragma shader_feature_local _TESSELLATION_SMOOTHING_FLAT _TESSELLATION_SMOOTHING_PHONG _TESSELLATION_SMOOTHING_BEZIER_QUAD_NORMALS
            #pragma shader_feature_local _TESSELLATION_FACTOR_CONSTANT _TESSELLATION_FACTOR_WORLD _TESSELLATION_FACTOR_SCREEN _TESSELLATION_FACTOR_WORLD_WITH_DEPTH

            // Material keywords - Wireframe Rendering
            #pragma shader_feature_local ENABLE_WIREFRAME

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"

            Texture2D _MainTex;
            SamplerState sampler_MainTex;

            Texture2D _WaterNormal1;
            SamplerState sampler_WaterNormal1;
            Texture2D _WaterNormal2;
            SamplerState sampler_WaterNormal2;
            Texture2D _WaterFlowMap;
            SamplerState sampler_WaterFlowMap;

            Texture2D _HeightMap;
            SamplerState sampler_HeightMap;
            TextureCube _FogCube;
            SamplerState sampler_FogCube; // Skybox cube texture

            // CBUFFER section needed for SRP batching.
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _BaseColor;
                float _Smoothness;
                // float _Specular;
                float _Metallic;

                float4 _WaterShallowColor;
                float4 _WaterDeepColor;
                float _WaterDensity;

                float _UJump;
                float _VJump;
                float _Tiling;
                float _Speed;
                float _FlowStrength;
                float _FlowOffset;

                float _NormalMapStrength;
                float _NormalMapSize;
                float _SimulationNormalStrength;
                float4 _HeightMap_TexelSize;
                float _HeightMapAltitude;

                float3 _FactorEdge1;
                float _FactorInside;
                float _TessellationFactor;
                float _TessellationBias;
                float _TessellationSmoothing;
                float _FrustumCullTolerance;
                float _BackFaceCullTolerance;

                float _MaxDistance;
                float _Resolution;
                int _Steps;
                float _Thickness;

                float4 _WireframeColor;
                float _WireframeSmoothing;
                float _WireframeThickness;

                float _HeightMap_Width;
                float _HeightMap_Height;

                // Extract scale from object to world matrix.
                static float3 scale = float3(
                    length(unity_ObjectToWorld._m00_m10_m20),
                    length(unity_ObjectToWorld._m01_m11_m21),
                    length(unity_ObjectToWorld._m02_m12_m22)
                );
            CBUFFER_END

            #include "Assets/Materials/Shaders/Common.hlsl"
            #include "Tessellation.hlsl"

            struct Interpolators
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : INTERNALTESSPOS;
                float3 normalWS : NORMAL;
                float2 uv : TEXCOORD0;
                DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 1);
                float2 uv_MainTex : TEXCOORD2;
                float3 normalOS : TEXCOORD4;
                float4 tangentWS : TEXCOORD5;
                float3 viewDir : TEXCOORD6;
                float fogCoords : TEXCOORD7;
                float2 barycentricCoordinates : TEXCOORD8;
                float4 screenPos : TEXCOORD9;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            // #include "Wireframe.hlsl"

            TessellationControlPoint Vertex(Attributes input)
            {
                TessellationControlPoint output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);

                VertexPositionInputs positionInputs = GetVertexPositionInputs(input.vertexOS);
                VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS, input.tangentOS);

                output.normalOS = input.normalOS.xyz;

                output.positionCS = positionInputs.positionCS;
                output.positionWS = positionInputs.positionWS;
                output.normalWS = normalInputs.normalWS;
                output.tangentWS = float4(normalInputs.tangentWS, input.tangentOS.w); // tangent.w contains bitangent multiplier;
                output.uv = input.uv;
                output.uv_MainTex = TRANSFORM_TEX(input.uv, _MainTex);
                output.lightMap = input.lightMap;

                return output;
            }

            // The domain function runs once per vertex in the final, tessellated mesh.
            // Use it to reposition vertices and prepare for the fragment stage.
            [domain(INPUT_TYPE)] // Signal we're inputting triangles.
            Interpolators Domain(TessellationFactors factors, OutputPatch<TessellationControlPoint, CONTROL_POINTS> patch,
     float3 barycentricCoordinates : SV_DomainLocation)
            {
                // Setup instancing and stereo support (for VR)
                UNITY_SETUP_INSTANCE_ID(patch[0]);
                UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                Interpolators output = (Interpolators)0;
                // Calculate smoothed position, normal, and tangent
                // This rounds a triangle to smooth model silhouettes and improve normal interpolation
                // It can use either flat (no smoothing), Phong, or bezier-based smoothing, depending on material settings
                #if defined(_TESSELLATION_SMOOTHING_PHONG)
                    output.positionWS = CalculatePhongPosition(barycentricCoordinates, _TessellationSmoothing,
                        patch[0].positionWS, patch[0].normalWS, patch[1].positionWS, patch[1].normalWS, patch[2].positionWS, patch[2].normalWS);
                #elif defined(_TESSELLATION_SMOOTHING_BEZIER_LINEAR_NORMALS) || defined(_TESSELLATION_SMOOTHING_BEZIER_QUAD_NORMALS)
                    output.positionWS = CalculateBezierPosition(barycentricCoordinates, _TessellationSmoothing, factors.bezierPoints, patch[0].positionWS, patch[1].positionWS, patch[2].positionWS);
                #else
                BARYCENTRIC_INTERPOLATE(positionWS);
                #endif

                #if defined(_TESSELLATION_SMOOTHING_BEZIER_QUAD_NORMALS)
                    float3 normalWS, tangentWS;
                    CalculateBezierNormalAndTangent(barycentricCoordinates, _TessellationSmoothing, factors.bezierPoints,
                        patch[0].normalWS, patch[0].tangentWS.xyz, patch[1].normalWS, patch[1].tangentWS.xyz, patch[2].normalWS, patch[2].tangentWS.xyz,
                        normalWS, tangentWS);
                #else
                BARYCENTRIC_INTERPOLATE(normalWS);
                    BARYCENTRIC_INTERPOLATE(tangentWS.xyz);
                float3 normalWS = normalize(output.normalWS);
                float3 tangentWS = normalize(output.tangentWS.xyz);
                #endif

                BARYCENTRIC_INTERPOLATE(normalOS);
                BARYCENTRIC_INTERPOLATE(uv);
                BARYCENTRIC_INTERPOLATE(uv_MainTex);

                // Apply height map.
                const float height = SAMPLE_TEXTURE2D_LOD(_HeightMap, sampler_HeightMap, output.uv, 0).r * _HeightMapAltitude;
                output.positionWS += output.normalWS * height;

                output.normalWS = normalWS;
                output.tangentWS = float4(tangentWS, patch[0].tangentWS.w);
                output.positionCS = TransformWorldToHClip(output.positionWS);
                output.screenPos = ComputeScreenPos(output.positionCS);
                output.fogCoords = ComputeFogFactor(output.positionCS.z);
                output.viewDir = GetWorldSpaceViewDir(output.positionWS);

                float4 lightMap;
                    BARYCENTRIC_INTERPOLATE_NOOUT(lightMap);
                OUTPUT_LIGHTMAP_UV(lightMap, unity_LightmapST, output.lightmapUV);
                OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

                return output;
            }

            half3 GetViewPositionFromDepth(float2 uv)
            {
                #if UNITY_REVERSED_Z
                    real depth = SampleSceneDepth(uv);
                #else
                    // Adjust z to match NDC for OpenGL
                    real depth = lerp(UNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(uv));
                #endif

                // depth = LinearEyeDepth(depth, _ZBufferParams);
                float3 worldPos = ComputeWorldSpacePosition(uv, depth, UNITY_MATRIX_I_VP);
                return TransformWorldToView(worldPos);
            }

            FragmentOutput Fragment(Interpolators input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv_MainTex);
                color *= _BaseColor;

                // Fog Reference: https://github.com/keijiro/KinoFog/blob/master/Assets/Kino/Fog/Shader/Fog.shader
                // https://www.reddit.com/r/Unity3D/comments/7wm02n/perfect_fog_for_your_game_trouble_matching_fog/
                // float4 fogCube = SAMPLE_TEXTURECUBE(_FogCube, sampler_FogCube, -viewDir);

                // color = float4(MixFogColor(color.rgb, fogCube.rgb, input.fogCoords), color.a);

                #if defined(ENABLE_WIREFRAME)
                // For Wireframe.
                float3 barys;
                barys.xy = input.barycentricCoordinates;
                barys.z = 1 - barys.x - barys.y;
                float3 deltas = fwidth(barys); // Keep the line width constant in screen space using screen-space derivative.
                float3 smoothing = deltas * _WireframeSmoothing;
                float3 thickness = deltas * _WireframeThickness;

                barys = smoothstep(thickness, thickness + smoothing, barys);
                float minBary = min(barys.x, min(barys.y, barys.z));
                color = lerp(_WireframeColor, color, minBary);
                #endif

                float3 flow = SAMPLE_TEXTURE2D(_WaterFlowMap, sampler_WaterFlowMap, input.uv * _Tiling).rgb;
                flow.xy = flow.xy * 2 - 1; // Map is in 0-1 range. We have to map it back to -1-1 range using *2-1.
                flow *= _FlowStrength;
                float noise = SAMPLE_TEXTURE2D(_WaterFlowMap, sampler_WaterFlowMap, input.uv * _Tiling).a; // Greyscale noise in alpha channel.
                float time = _Time.x * _Speed + noise;
                float2 jump = float2(_UJump, _VJump);
                float3 flowUVW_A = FlowUVW(input.uv, flow.xy, jump, _FlowOffset, _Tiling, time, false);
                float3 flowUVW_B = FlowUVW(input.uv, flow.xy, jump, _FlowOffset, _Tiling, time, true);

                float3 waterNormal1_A = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal1, sampler_WaterNormal1, flowUVW_A.xy * _NormalMapSize), _NormalMapStrength) * flowUVW_A.z;
                float3 waterNormal2_A = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal2, sampler_WaterNormal2, flowUVW_A.xy * _NormalMapSize - time * 2), _NormalMapStrength) * flowUVW_A.z;

                float3 waterNormal1_B = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal1, sampler_WaterNormal1, flowUVW_B.xy * _NormalMapSize), _NormalMapStrength) * flowUVW_B.z;
                float3 waterNormal2_B = UnpackNormalScale(
                    SAMPLE_TEXTURE2D(_WaterNormal2, sampler_WaterNormal2, flowUVW_B.xy * _NormalMapSize - time * 2), _NormalMapStrength) * flowUVW_B.z;
                // float3 normalTS = BlendNormal(BlendNormal(waterNormal1_A, waterNormal1_B), BlendNormal(waterNormal2_A, waterNormal2_B)) * flow.z;
                float3 normalTS = ((waterNormal1_A + waterNormal1_B) + (waterNormal2_A + waterNormal2_B)) * flow.z;
                normalTS = BlendNormal(normalTS, GenerateNormalFromHeightMap(input.uv));

                float3x3 tangentToWorld = CreateTangentToWorld(input.normalWS, input.tangentWS.xyz, input.tangentWS.w);
                float3 normalWS = TransformTangentToWorld(normalTS, tangentToWorld);

                half3 normViewDir = normalize(input.viewDir);
                InputData lightingInput = (InputData)0; // Info about position and orientation of mesh at current fragment.
                lightingInput.positionWS = input.positionWS;
                lightingInput.normalWS = normalWS;
                lightingInput.viewDirectionWS = normViewDir;
                lightingInput.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
                lightingInput.bakedGI = SAMPLE_GI(input.lightmapUV, input.vertexSH, lightingInput.normalWS);

                half4 ssrUV = half4(1,1,1,1);
                #if defined(_ENVIRONMENTREFLECTIONS)
                half3 normalVS = TransformWorldToViewNormal(normalWS);
                half3 reflectDirVS = reflect(-normViewDir, normalVS);
                half4 positionTo = half4(input.viewDir, 1.0);

                half4 startView = half4(input.viewDir + (reflectDirVS * 0), 1.0);
                half4 endView = half4(input.viewDir + (reflectDirVS * _MaxDistance), 1.0);

                half4 startFrag = startView;
                startFrag = TransformWViewToHClip(startFrag);
                startFrag.xyz /= startFrag.w;
                startFrag.xy = startFrag.xy * 0.5 + 0.5;
                startFrag.xy *= _ScreenParams.xy;

                half4 endFrag = endView;
                endFrag = TransformWViewToHClip(endFrag);
                endFrag.xyz /= endFrag.w;
                endFrag.xy = endFrag.xy * 0.5 + 0.5;
                endFrag.xy *= _ScreenParams.xy;

                // First Pass (Rough Pass)
                half2 curFrag = startFrag.xy;
                ssrUV.xy = curFrag / _ScreenParams.xy;

                // How many pixels the current step takes.
                float deltaX = endFrag.x - startFrag.x;
                float deltaY = endFrag.y - startFrag.y;

                // Decide which delta to use for current step.
                float useX = abs(deltaX) >= abs(deltaY) ? 1 : 0;
                float delta = lerp(abs(deltaY), abs(deltaX), useX) * clamp(_Resolution, 0, 1);

                // How much to increment the X and Y positions by. Uses bigger of the two deltas.
                half2 increment = half2(deltaX, deltaY) / max(delta, 0.001);

                float search0 = 0; // Remembers the last position of the line where the ray missed/didn't intersect with geometry.
                float search1 = 0; // Ranges from 0-1. 0: Start fragment. 1: End fragment.

                int hit0 = 0; // Indicates intersection during first pass.
                int hit1 = 0; // Indicates intersection during second pass.

                float viewDistance = startView.z; // How far away from the camera the current point on the ray is.
                float depth = _Thickness; // View distance difference between current ray and scene position.

                [unroll(10)]
                for (int i = 0; i < int(delta); ++i)
                {
                    curFrag += increment;
                    ssrUV.xy = curFrag / _ScreenParams.xy;
                    positionTo.xyz = GetViewPositionFromDepth(ssrUV.xy);

                    search1 = lerp((curFrag.y - startFrag.y) / deltaY, (curFrag.x - startFrag.x) / deltaX, useX);
                    viewDistance = (startView.y * endView.y) / lerp(endView.y, startView.y, search1);
                    depth = viewDistance - positionTo.y;

                    if (depth > 0 && depth < _Thickness)
                    {
                        hit0 = 1;
                        break;
                    } else
                    {
                        search0 = search1;
                    }

                }

                search1 = search0 + ((search1 - search0) / 2);

                float steps = _Steps;
                steps *= hit0;

                // Second Pass (Refinement Pass)
                [unroll(10)]
                for (int i = 0; i < steps; ++i)
                {
                    curFrag = lerp(startFrag.xy, endFrag.xy, search1);
                    ssrUV.xy = curFrag / _ScreenParams.xy;
                    positionTo.xyz = GetViewPositionFromDepth(ssrUV.xy);

                    viewDistance = (startView.y * endView.y) / lerp(endView.y, startView.y, search1);
                    depth = viewDistance - positionTo.y;

                    if (depth > 0 && depth < _Thickness)
                    {
                        hit1 = 1;
                        search1 = search0 + ((search1 - search0) / 2);
                    } else
                    {
                        float temp = search1;
                        search1 = search1 + ((search1 - search0) / 2);
                        search0 = temp;
                    }
                }

                float visibility = hit1
                                 * positionTo.w
                                 * (1 - max(dot(-normViewDir, reflectDirVS), 0))
                                 * (1 - clamp(depth / _Thickness, 0, 1))
                                 * (1 - clamp(length(positionTo - input.viewDir) / _MaxDistance, 0, 1))
                                 * (ssrUV.x < 0 || ssrUV.x > 1 ? 0 : 1)
                                 * (ssrUV.y < 0 || ssrUV.y > 1 ? 0 : 1);

                visibility = clamp(visibility, 0, 1);
                ssrUV.ba = visibility;

                // half4 reflectData = SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, reflectDir, _Smoothness);
                // half3 reflectionProbeColor = DecodeHDREnvironment(reflectData, unity_SpecCube0_HDR);
                // color *= reflectionProbeColor;
                #endif

                #if defined(_SPECULARHIGHLIGHTS)
                    half3 specular = _Specular;
                #else
                half3 specular = 0;
                #endif

                float4 colorBelowWater = ColorBelowWater(input.screenPos);
                SurfaceData surfaceInput = (SurfaceData)0; // Holds info about the surface material's physical properties (e.g. color).
                surfaceInput.albedo = color;
                surfaceInput.alpha = colorBelowWater.a;
                surfaceInput.emission = colorBelowWater * (1 - color.a);
                surfaceInput.specular = specular;
                surfaceInput.metallic = _Metallic;
                surfaceInput.smoothness = _Smoothness;
                surfaceInput.normalTS = normalTS;
                surfaceInput.occlusion = 1;

                // float4 finalColor = UniversalFragmentPBR(lightingInput, surfaceInput);
                // finalColor.a = 1;
                // return finalColor;

                BRDFData brdfData;
                InitializeBRDFData(surfaceInput.albedo, surfaceInput.metallic, surfaceInput.specular, surfaceInput.smoothness, surfaceInput.alpha, brdfData);

                Light mainLight = GetMainLight(lightingInput.shadowCoord, lightingInput.positionWS, lightingInput.shadowMask);
                MixRealtimeAndBakedGI(mainLight, lightingInput.normalWS, lightingInput.bakedGI, lightingInput.shadowMask);
                half3 finalColor = GlobalIllumination(brdfData, lightingInput.bakedGI, surfaceInput.occlusion, lightingInput.positionWS, lightingInput.normalWS, lightingInput.viewDirectionWS);

                return BRDFDataToGbuffer(brdfData, lightingInput, surfaceInput.smoothness, surfaceInput.emission + finalColor, surfaceInput.occlusion);
            }
            ENDHLSL
        }
    }
}