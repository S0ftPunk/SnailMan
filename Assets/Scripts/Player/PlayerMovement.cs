using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    //public float speed = 10f;
    //private Rigidbody rb;
    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    //void Update()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    Vector3 move1 = transform.right * x + transform.forward * z;
    //    //Vector3 move = new Vector3(move1.x, rb.velocity.y, move1.z);
    //    rb.velocity = new Vector3(move1.x * speed, rb.velocity.y, move1.z * speed);
    //}

    private float speed;
    public float runSpeed;
    public float walkSpeed;
    public CharacterController controller;
    public int health = 100;
    public Rigidbody rb;
    public Joystick joystick;
    public float stamina;
    public bool isRunning = false;
    public bool isMoving = false;
    public Slider sliderStamina;
    public EnemyScript enemyScript;
    public CameraBobbing cameraBobbing;
    private bool wasUpped;

    public GameObject mobileUI;
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        PlayerPrefs.SetString("first?", "yes");
        if (UserAgentManager.Instance.isPC)
        {
            mobileUI.SetActive(false);
        }
    }
    public void Down()
    {
        sliderStamina.gameObject.SetActive(true);
        isRunning = true;
        wasUpped = false;
    }
    public void Up()
    {
        isRunning = false;
        wasUpped = true;
    }
    void Update()
    {
        float x;
        float z = 0;
        if (!UserAgentManager.Instance.isPC)
            x = joystick.Horizontal;
        else
        {
            x = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.LeftShift))
                Down();
            else
                Up();
        }
        if (isRunning)
        {
            enemyScript.HeardThePlayer();
            if (stamina > 0)
            {
                stamina -= Time.deltaTime * 1.5f;
                speed = runSpeed;
                z = 1;
                sliderStamina.value = stamina;
            }
            else
            {
                speed = 0;
                isRunning = false;
            }

        }
        else
        {
            if (stamina < 10)
            {
                stamina += Time.deltaTime;
                sliderStamina.value = stamina;
            }
            else if(stamina >= 10)
            {
                StartCoroutine(SliderOFF());
            }    
            if (!UserAgentManager.Instance.isPC)
                z = joystick.Vertical;
            else
                z = Input.GetAxis("Vertical"); 
            speed = walkSpeed;
        }
        cameraBobbing.CAmeraAnimation(x, z,speed/1.2f); //speed

        Vector3 move = transform.right * x + transform.forward * z;
        rb.velocity = move * speed;
    }
    public void SlimeStep(bool isInStep)
    {
        if (isInStep)
        {
            enemyScript.HeardThePlayer();
        }
        else
        {
            StartCoroutine(SlimeOff(1));
        }
        walkSpeed = 0.5f;
        runSpeed = 1f;

    }
    IEnumerator SliderOFF()
    {
        yield return new WaitForSeconds(2f);
        if(wasUpped)
            sliderStamina.gameObject.SetActive(false);
    }
    IEnumerator SlimeOff(float speed)
    {
        yield return new WaitForSeconds(7f);
        walkSpeed = speed;
        runSpeed = speed*2;
    }
}
