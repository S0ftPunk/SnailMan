using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VisibleManager : MonoBehaviour
{
    public AudioSource monsterSound, ambientSound;
    public AudioClip screemerClip;

    public static int count = 0;
    private void OnBecameVisible()
    {
        if (count == 0)
        {
            monsterSound.DOFade(1, 4);
            ambientSound.PlayOneShot(screemerClip, 0.5f);
        }
        else if(count == 1)
        {
            ambientSound.PlayOneShot(screemerClip, 0.7f);
        }
        else if(count == 2)
        {
            ambientSound.PlayOneShot(screemerClip, 1);
        }
        count++;
    }
}
