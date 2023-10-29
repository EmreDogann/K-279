using Capabilities.Movement;
using DG.Tweening;
using UnityEngine;

public class DirectionBiasFollower : MonoBehaviour
{
    [SerializeField] private Ease easing = Ease.InOutSine;
    [SerializeField] private float followTime;

    private IMover _moverFollowTarget;
    private Vector3 _biasAmount;

    private void Awake()
    {
        _moverFollowTarget = transform.parent.GetComponent<IMover>();
        _biasAmount = transform.localPosition;
    }

    private void OnEnable()
    {
        if (_moverFollowTarget != null)
        {
            _moverFollowTarget.OnSwitchingDirection += OnSwitchDirection;
        }
    }

    private void OnDisable()
    {
        if (_moverFollowTarget != null)
        {
            _moverFollowTarget.OnSwitchingDirection -= OnSwitchDirection;
        }
    }

    private void OnSwitchDirection(bool isFacingRight)
    {
        transform.DOKill();
        transform.DOLocalMove(_biasAmount * (isFacingRight ? 1 : -1), followTime).SetEase(easing);
    }
}