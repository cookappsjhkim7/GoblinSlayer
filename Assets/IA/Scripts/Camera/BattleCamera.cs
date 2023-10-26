using DG.Tweening;
using UnityEngine;

public class BattleCamera : SingletonMonoBehaviour<BattleCamera>
{
    private Vector3 _initCameraTransform;

    void Start()
    {
        _initCameraTransform = transform.position;
    }

    public void Shake(float duration, float magnitude)
    {
        transform.DOKill();
        
        transform.DOShakePosition(duration, magnitude).OnComplete(() =>
        {
            transform.position = _initCameraTransform;
        });
    }
}