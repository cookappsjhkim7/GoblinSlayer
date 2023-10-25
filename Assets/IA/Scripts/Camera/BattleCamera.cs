using DG.Tweening;
using UnityEngine;

public class BattleCamera : SingletonMonoBehaviour<BattleCamera>
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = transform;
    }

    public void Shake(float duration, float magnitude)
    {
        cameraTransform.DOShakePosition(duration, magnitude);
    }
}