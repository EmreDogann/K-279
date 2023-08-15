using Controllers;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDirectionFlipper : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    private SpriteRenderer _spriteRenderer;
    private bool _flipX;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _flipX = _spriteRenderer.flipX;
    }

    private void Update()
    {
        float direction = controller.RetrieveMoveInput(gameObject);

        if (direction > 0)
        {
            _spriteRenderer.flipX = _flipX;
        }
        else if (direction < 0)
        {
            _spriteRenderer.flipX = !_flipX;
        }
    }
}