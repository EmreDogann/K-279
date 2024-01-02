using System;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

namespace Utils.Editor
{
    internal class DitherLitShader : BaseShaderGUI
    {
        private static readonly string[] workflowModeNames = Enum.GetNames(typeof(LitGUI.WorkflowMode));

        private LitGUI.LitProperties litProperties;
        private MaterialProperty _noiseTypeProperty;
        private MaterialProperty _noiseMapProperty;
        private MaterialProperty _colorRampMapProperty;

        private MaterialProperty _useRampTexProperty;
        private MaterialProperty _bgColorProperty;
        private MaterialProperty _fgColorProperty;

        private MaterialProperty _tilingProperty;

        // collect properties from the material properties
        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties);
            litProperties = new LitGUI.LitProperties(properties);
            _noiseTypeProperty = ShaderGUI.FindProperty("_NoiseType", properties);
            _noiseMapProperty = ShaderGUI.FindProperty("_NoiseMap", properties);
            _colorRampMapProperty = ShaderGUI.FindProperty("_ColorRampMap", properties);

            _useRampTexProperty = ShaderGUI.FindProperty("_UseRampTex", properties);
            _bgColorProperty = ShaderGUI.FindProperty("_BG", properties);
            _fgColorProperty = ShaderGUI.FindProperty("_FG", properties);

            _tilingProperty = ShaderGUI.FindProperty("_Tiling", properties);
        }

        // material changed check
        public override void ValidateMaterial(Material material)
        {
            SetMaterialKeywords(material, LitGUI.SetMaterialKeywords);
        }

        // material main surface options
        public override void DrawSurfaceOptions(Material material)
        {
            // Use default labelWidth
            EditorGUIUtility.labelWidth = 0f;

            if (litProperties.workflowMode != null)
            {
                DoPopup(LitGUI.Styles.workflowModeText, litProperties.workflowMode, workflowModeNames);
            }

            base.DrawSurfaceOptions(material);
        }

        // material main surface inputs
        public override void DrawSurfaceInputs(Material material)
        {
            materialEditor.IntSliderShaderProperty(_noiseTypeProperty, 1, 4,
                new GUIContent("Noise Type",
                    "Noise type to use in post processing shader.\n 1 = Blue noise,\n 2 = White Noise,\n 3 = Interleaved-Gradient Noise,\n 4 = Bayer Noise"));
            materialEditor.TexturePropertySingleLine(new GUIContent("Dither Map", "Pattern to use for dithering."),
                _noiseMapProperty);
            materialEditor.ShaderProperty(_useRampTexProperty, _useRampTexProperty.displayName);

            if (_useRampTexProperty.floatValue == 1)
            {
                material.EnableKeyword("USE_RAMP_TEX");
                materialEditor.TexturePropertySingleLine(
                    new GUIContent("Color Ramp Map", "Color Ramp to sample when thresholding colors."),
                    _colorRampMapProperty);
            }
            else
            {
                material.DisableKeyword("USE_RAMP_TEX");
                materialEditor.ColorProperty(_bgColorProperty, _bgColorProperty.displayName);
                materialEditor.ColorProperty(_fgColorProperty, _fgColorProperty.displayName);
            }

            materialEditor.FloatProperty(_tilingProperty, "Dither Tiling");

            base.DrawSurfaceInputs(material);
            LitGUI.Inputs(litProperties, materialEditor, material);
            DrawEmissionProperties(material, true);
            DrawTileOffset(materialEditor, baseMapProp);
        }

        // material main advanced options
        public override void DrawAdvancedOptions(Material material)
        {
            if (litProperties.reflections != null && litProperties.highlights != null)
            {
                materialEditor.ShaderProperty(litProperties.highlights, LitGUI.Styles.highlightsText);
                materialEditor.ShaderProperty(litProperties.reflections, LitGUI.Styles.reflectionsText);
            }

            base.DrawAdvancedOptions(material);
        }

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            if (material == null)
            {
                throw new ArgumentNullException("material");
            }

            // _Emission property is lost after assigning Standard shader to the material
            // thus transfer it before assigning the new shader
            if (material.HasProperty("_Emission"))
            {
                material.SetColor("_EmissionColor", material.GetColor("_Emission"));
            }

            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            if (oldShader == null || !oldShader.name.Contains("Legacy Shaders/"))
            {
                SetupMaterialBlendMode(material);
                return;
            }

            SurfaceType surfaceType = SurfaceType.Opaque;
            BlendMode blendMode = BlendMode.Alpha;
            if (oldShader.name.Contains("/Transparent/Cutout/"))
            {
                surfaceType = SurfaceType.Opaque;
                material.SetFloat("_AlphaClip", 1);
            }
            else if (oldShader.name.Contains("/Transparent/"))
            {
                // NOTE: legacy shaders did not provide physically based transparency
                // therefore Fade mode
                surfaceType = SurfaceType.Transparent;
                blendMode = BlendMode.Alpha;
            }

            material.SetFloat("_Blend", (float)blendMode);

            material.SetFloat("_Surface", (float)surfaceType);
            if (surfaceType == SurfaceType.Opaque)
            {
                material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            }
            else
            {
                material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            }

            if (oldShader.name.Equals("Standard (Specular setup)"))
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Specular);
                Texture texture = material.GetTexture("_SpecGlossMap");
                if (texture != null)
                {
                    material.SetTexture("_MetallicSpecGlossMap", texture);
                }
            }
            else
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Metallic);
                Texture texture = material.GetTexture("_MetallicGlossMap");
                if (texture != null)
                {
                    material.SetTexture("_MetallicSpecGlossMap", texture);
                }
            }
        }
    }
}