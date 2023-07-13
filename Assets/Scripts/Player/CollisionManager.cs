using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Animator exploseAnimation;
    public GameObject player;
    public AudioClip exploseAudio;
    public AudioSource audio;
    public bool isShoted;
    private void OnCollisionEnter(Collision collision)
    {
        if (isShoted)
        {
            if (collision.gameObject.name == "SnailMan")
            {
                collision.gameObject.GetComponentInParent<EnemyScript>().Staned();
            }

            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log(collision.gameObject.name);
            Transform target = player.transform;
            exploseAnimation.transform.rotation = Quaternion.LookRotation(target.position - exploseAnimation.transform.position, Vector3.up);
            exploseAnimation.gameObject.SetActive(true);
            audio.PlayOneShot(exploseAudio);
            StartCoroutine(DestroyObjects());
        }
    }

    IEnumerator DestroyObjects()
    {
        yield return new WaitForSeconds(0.15f);
            Destroy(this.gameObject);
    }

}
