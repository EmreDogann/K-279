using Capabilities;
using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionBiasFollower : MonoBehaviour
{
    [SerializeField] private Transform transformToFollow;
    [SerializeField] private Ease easing = Ease.InOutSine;

    [SerializeField] private float followTime;

    private void OnEnable()
    {
        Move.OnFlipPlayer += OnFlipPlayer;
    }

    private void OnDisable()
    {
        Move.OnFlipPlayer -= OnFlipPlayer;
    }

    private void Update()
    {
        transform.position = transformToFollow.position;
    }

    private void OnFlipPlayer(bool isFacingRight)
    {
        Vector3 targetRotation = isFacingRight
            ? new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z)
            : new Vector3(transform.eulerAngles.x, 180.0f, transform.eulerAngles.z);

        transform.DOKill();
        transform.DORotate(targetRotation, followTime).SetEase(easing);
    }
}