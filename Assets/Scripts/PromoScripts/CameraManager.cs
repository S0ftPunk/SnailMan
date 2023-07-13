using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public MouseController mouseController;
    public int time,angleView;

    public GameObject snailHead, snailMan,snailMan2;
    public Image shotImage;
    public Text snaiText;
    public Collider firstCollider, secondCollider;

    private float startAng;

    private bool toLeft, toRight, wasShoted, visible;

    private Tweener tween;

    public AudioSource monsterSound, ambientSound,heartSound;
    public AudioClip screemerClip;

    private void OnTriggerEnter(Collider other)
    {
        if(other == secondCollider)
        {
            snailHead.SetActive(true);
            monsterSound.DOFade(0, 2);
        }
    }

    private void SnailDetector(GameObject obj)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, obj.transform.position - transform.position);
        Physics.Raycast(ray, out hit);
        Debug.DrawLine(ray.origin, hit.point, Color.white);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == obj.transform.gameObject)
            {
                var playerPos = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
                var snailPos = new Vector3(transform.position.x, 0, transform.position.z);
                var toPlayer = (playerPos - snailPos).normalized;
                var res = Vector3.Dot(transform.forward, toPlayer);
                visible = true;
            }
            else
            {
                visible = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 4);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, angleView / 2, 0) * transform.forward) * 5);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, -angleView / 2, 0) * transform.forward) * 5);
    }
    private void Update()
    {
        SnailDetector(snailMan);
        if (visible)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(SoundStart(1));
            }
        }
        if (Input.GetKeyDown(KeyCode.I) )
        {
            StartCoroutine(ShotCor());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SoundStart(2));
        }
    }
    IEnumerator SoundStart(int step)
    {
        yield return new WaitForSeconds(0.2f);
        if (step == 1)
        {
            ambientSound.PlayOneShot(screemerClip, 0.3f);
            GetComponent<PlayerMovement>().runSpeed *= 1.2f;
            yield return new WaitForSeconds(0.5f);
            monsterSound.DOFade(0.6f, 5);
        }
        else if (step == 2)
        {
            ambientSound.PlayOneShot(screemerClip, 0.3f);
        }
    }
    IEnumerator ShotCor()
    {
        heartSound.DOFade(1, 3);
        tween = shotImage.DOFade(1, 0.6f);
        monsterSound.DOFade(0, 2);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(0.4f);
        snailMan2.SetActive(true);
        snailHead.SetActive(false);
        ambientSound.PlayOneShot(screemerClip, 0.7f);
        tween = shotImage.DOFade(0, 0.6f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(0.4f);
        tween = shotImage.DOFade(1, 0.6f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        ambientSound.PlayOneShot(screemerClip, 1);
        snaiText.DOFade(1, 0.4f);
    }
    //IEnumerator Writer()
    //{

    //}

}
