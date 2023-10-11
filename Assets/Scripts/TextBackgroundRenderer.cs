using TMPro;
using UnityEngine;
using Utils;

/*
 * READ BEFORE USING!!!a
 * For this to work properly,
 * the text object muse be a child of the background rect transform.
 *
 *  v Background
 *      Text
 *
 * Text pivot must = middle left.
 * Text vertical alignment must = Left align.
 */

[ExecuteInEditMode]
public class TextBackgroundRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void LateUpdate()
    {
        if (textMeshPro.text != string.Empty)
        {
            textMeshPro.ForceMeshUpdate(true);

            Vector2 textSize = textMeshPro.GetRenderedValues(true);
            backgroundRectTransform.sizeDelta =
                textSize + textMeshPro.margin.xy() * 2.0f + textMeshPro.margin.zw() * 2.0f;
        }
        else
        {
            backgroundRectTransform.sizeDelta = Vector2.zero;
        }
    }
}