using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunScript : MonoBehaviour
{
    public bool isPicked;
    public bool isReloded;

    [SerializeField] Image shotButton;
    [SerializeField] Image gunImage;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;

    [SerializeField] EnemyScript enemyScript;

    public AudioClip reloadAuido;
    private AudioSource audio;

    [SerializeField] Text gunHelpText;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void GunPickUp()
    {
        isPicked = true;
        gunImage.enabled = true;
        StartCoroutine(HideImage());
    }
    public void BulletPicUp()
    {
        if (!isReloded & isPicked)
        {
            gun.SetActive(true);
            isReloded = true;
            bullet.SetActive(true);
            shotButton.enabled = true;
            audio.PlayOneShot(reloadAuido);
        }
    }
    public void Shot()
    {
        if (isReloded)
        {
            GameObject newBullet = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            newBullet.GetComponent<CollisionManager>().isShoted = true;
            newBullet.GetComponent<BoxCollider>().enabled = true;
            rb.velocity = newBullet.transform.forward * -20;
            bullet.SetActive(false);
            gun.SetActive(false);
            enemyScript.HeardThePlayer();
            isReloded = false;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }
    IEnumerator HideImage()
    {
        yield return new WaitForSeconds(3);
        gunImage.enabled = false;
    }
    public IEnumerator TextShow(string text)
    {
        gunHelpText.enabled = true;
        gunHelpText.text = text;
        yield return new WaitForSeconds(3);
        gunHelpText.enabled = false;
    }
}
