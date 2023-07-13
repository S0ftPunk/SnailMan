using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public AudioClip slimeSound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            FindObjectOfType<AmbientSounds>().audio.PlayOneShot(slimeSound,0.3f);
            collision.gameObject.GetComponent<PlayerMovement>().SlimeStep(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<PlayerMovement>().SlimeStep(false);
        }
    }
}
