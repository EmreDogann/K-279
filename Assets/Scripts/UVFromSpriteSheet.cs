using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class UVFromSpriteSheet : MonoBehaviour
{
    [SerializeField] private Vector2 spriteFixedSize = Vector2.one;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private int _prevAnimFrame;
    private readonly int _spriteUVRange = Shader.PropertyToID("_SpriteUVRange");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        if (_spriteRenderer)
        {
            _spriteRenderer.sharedMaterial.SetVector(_spriteUVRange, CalcUvRange(_spriteRenderer.sprite));
        }
    }

    private void LateUpdate()
    {
        // float minU = 1;
        // float maxU = 0;
        // float minV = 1;
        // float maxV = 0;
        // foreach (Vector2 uv in spriteRenderer.sprite.uv)
        // {
        //     minU = Mathf.Min(uv.x, minU);
        //     maxU = Mathf.Max(uv.x, maxU);
        //     minV = Mathf.Min(uv.y, minV);
        //     maxV = Mathf.Max(uv.y, maxV);
        // }

        // spriteRenderer.sharedMaterial.SetVector(_spriteUVRange, new Vector4(minU, maxU, minV, maxV));

        if (!_animator && _animator.GetCurrentAnimatorClipInfo(0).Length == 0)
        {
            return;
        }

        var animationClip = _animator.GetCurrentAnimatorClipInfo(0);
        int currentFrame = (int)(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime *
                                 (animationClip[0].clip.length * animationClip[0].clip.frameRate));
        if (currentFrame != _prevAnimFrame)
        {
            _spriteRenderer.sharedMaterial.SetVector(_spriteUVRange, CalcUvRange(_spriteRenderer.sprite));
        }

        _prevAnimFrame = currentFrame;
    }

    // From: https://forum.unity.com/threads/shader-graph-getting-local-sprite-uv-from-sprite-sheet.865834/#post-9037684
    private Vector4 CalcUvRange(Sprite sprite)
    {
        Vector2 textureSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 fixedSize = spriteFixedSize * sprite.pixelsPerUnit / textureSize;

        Vector2 spriteUvPos = CalcSpriteUvPos(sprite);
        spriteUvPos += sprite.pivot / textureSize;
        spriteUvPos += 1 * fixedSize;

        return new Vector4(
            spriteUvPos.x, spriteUvPos.x + fixedSize.x,
            spriteUvPos.y, spriteUvPos.y + fixedSize.y
        );
    }

    // From: https://forum.unity.com/threads/shader-graph-getting-local-sprite-uv-from-sprite-sheet.865834/#post-9037684
    private static Vector2 CalcSpriteUvPos(Sprite sprite)
    {
        Vector2 uvPos = Vector2.one;
        foreach (Vector2 uv in sprite.uv)
        {
            uvPos.x = Mathf.Min(uv.x, uvPos.x);
            uvPos.y = Mathf.Min(uv.y, uvPos.y);
        }

        return uvPos;
    }
}