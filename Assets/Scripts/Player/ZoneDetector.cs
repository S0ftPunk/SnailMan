using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetector : MonoBehaviour
{
    [SerializeField] Collider defaultCollider, secondCollider, lastCollider, winTrigger, gunCollider;
    [SerializeField] List<Collider> bullets;
    [SerializeField] GameObject patrol1, patrol2, patrol3;
    [SerializeField] GameObject winText;

    private EnemyScript enemy;
    private GunScript gunScript;
    private SessionManager sessionManager;

    private bool isWinner = false;
    private void Start()
    {
        enemy = FindObjectOfType<EnemyScript>();
        enemy.ChangeRoute(patrol1);

        gunScript = FindObjectOfType<GunScript>();
        sessionManager = FindObjectOfType<SessionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == winTrigger)
        {
            if (!isWinner)
            {
                sessionManager.Win();
                isWinner = true;
            }
        }
        if (!enemy.isAgred | !enemy.isFollowing)
        {
            if (other == defaultCollider)
                enemy.ChangeRoute(patrol1);
            else if (other == secondCollider)
                enemy.ChangeRoute(patrol2);
            else if (other == lastCollider)
                enemy.ChangeRoute(patrol3);
        }
        if(other == gunCollider)
        {
            StartCoroutine(gunScript.TextShow("You found the weapon! Now you need to find ammo"));
            gunScript.GunPickUp();
            gunCollider.gameObject.SetActive(false);
        }
        foreach (Collider col in bullets)
        {
            if (other == col)
            {
                if (gunScript.isPicked & !gunScript.isReloded)
                {
                    gunScript.BulletPicUp();
                    col.gameObject.SetActive(false);
                }
                if(!gunScript.isPicked)
                {
                    StartCoroutine(gunScript.TextShow("You haven't found the weapon yet"));
                }
            }
        }
    }
}
