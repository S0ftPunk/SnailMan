using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbientSounds : MonoBehaviour
{
    public AudioClip verse1, verse2;
    public AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(SoundsManager());
    }
    IEnumerator SoundsManager()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            System.Random rnd = new System.Random();
            int chance = rnd.Next(0, 3);
            if (chance < 2)
            {
                int stereoNum = rnd.Next(0, 6);
                if(stereoNum < 1)
                {
                    audio.panStereo = -0.5f;

                }
                else if(stereoNum < 5)
                {
                    audio.panStereo = 0;
                }
                else
                {
                    audio.panStereo = 0.5f;
                }
                int clipNum = rnd.Next(0, 2);
                if (clipNum == 0)
                {
                    audio.PlayOneShot(verse1);
                }
                else
                {
                    audio.PlayOneShot(verse2);
                }
            }
        }
        
    }
}
