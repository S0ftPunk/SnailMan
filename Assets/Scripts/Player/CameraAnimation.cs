using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraAnimation : MonoBehaviour
{
    private Vector3 startPosition; // начальная позиция камеры
    private Quaternion startRotation; // начальный поворот камеры

    public float duration, srenght, random;
    public int vibrato;
    private void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    public void Shake()
    {
        //float duration = 1f; // длительность анимации
        //float strength = 0.05f; // сила покачивания
        //int vibrato = 10; // количество покачиваний
        //float randomness = 90f; // случайность покачивания

        transform.DOShakePosition(duration, srenght, vibrato, random, false); // покачивание камеры
    }

    public void ResetPosition()
    {
        transform.localPosition = startPosition;
        transform.DOLocalMove(startPosition,1);
        transform.localRotation = startRotation;
    }

}
