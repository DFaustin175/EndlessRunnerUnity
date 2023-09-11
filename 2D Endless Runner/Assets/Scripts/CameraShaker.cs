using DG.Tweening;
using System;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Vector3 positionShake;
    [SerializeField] private Vector3 rotationShake;

    private static event Action Shake;
    public static void Invoke()
    { Shake?.Invoke(); }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake += CameraShake;

    void CameraShake()
    {
        _cam.DOComplete();
        _cam.DOShakePosition(.3f, positionShake);
        _cam.DOShakeRotation(.3f, rotationShake);
    }
}
