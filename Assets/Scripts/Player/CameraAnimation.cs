using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraAnimation : MonoBehaviour
{
    private Vector3 startPosition; // ��������� ������� ������
    private Quaternion startRotation; // ��������� ������� ������

    public float duration, srenght, random;
    public int vibrato;
    private void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }

    public void Shake()
    {
        //float duration = 1f; // ������������ ��������
        //float strength = 0.05f; // ���� �����������
        //int vibrato = 10; // ���������� �����������
        //float randomness = 90f; // ����������� �����������

        transform.DOShakePosition(duration, srenght, vibrato, random, false); // ����������� ������
    }

    public void ResetPosition()
    {
        transform.localPosition = startPosition;
        transform.DOLocalMove(startPosition,1);
        transform.localRotation = startRotation;
    }

}
