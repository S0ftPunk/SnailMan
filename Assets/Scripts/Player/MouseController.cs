using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Promo
using DG.Tweening;
//Promo

public class MouseController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 200f;
    public float xRotation = 180f;
    public float yRotation = 0f;

    public float sensitivity = 0.1f; // чувствительность движения мыши/телефона
    private float rotationX = 180f;
    private float rotationY = 0.0f;

    public Transform flashlight;
    public Transform head;
    private void Start()
    {
        if (UserAgentManager.Instance.isPC)
            Cursor.lockState = CursorLockMode.Locked;
        if (PlayerPrefs.HasKey("PlayerSensivity"))
            sensitivity = PlayerPrefs.GetFloat("PlayerSensivity");
        else
        {
            sensitivity = 0.2f;
            PlayerPrefs.SetFloat("PlayerSensivity",sensitivity);
        }
        mouseSensitivity *= sensitivity;
    }
    public void SetSensitivity()
    {
        sensitivity = PlayerPrefs.GetFloat("PlayerSensivity");
        mouseSensitivity = sensitivity * 600f;
        Debug.Log(mouseSensitivity);
    }
    void Update()
    {
        if (!UserAgentManager.Instance.isPC)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width / 3)
                {
                    rotationX += touch.deltaPosition.x * sensitivity * - 1;
                    rotationY -= touch.deltaPosition.y * sensitivity * -1;

                    // Ограничиваем вертикальный поворот камеры
                    rotationY = Mathf.Clamp(rotationY, -90, 90);

                    transform.rotation = Quaternion.Euler(0, rotationX, 0);
                    head.transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
                }
            }
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yRotation -= mouseY;
            xRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(0, xRotation, 0);
            head.transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);

            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    transform.DORotateQuaternion(Quaternion.Euler(0, xRotation - 90, 0), 0.2f);
            //    head.transform.DORotateQuaternion(Quaternion.Euler(yRotation, xRotation-90, 0), 0.2f);
            //    xRotation -= 90;
            //}
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    transform.DORotateQuaternion(Quaternion.Euler(0, xRotation + 90, 0), 0.2f);
            //    head.transform.DORotateQuaternion(Quaternion.Euler(yRotation, xRotation + 90, 0), 0.2f);
            //    xRotation += 90;
            //}
            //if (Input.GetKeyDown(KeyCode.K))
            //{
            //    transform.DORotateQuaternion(Quaternion.Euler(0, xRotation + 180, 0), 0.45f);
            //    head.transform.DORotateQuaternion(Quaternion.Euler(yRotation, xRotation + 180, 0), 0.45f);
            //    xRotation += 180;
            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    transform.DORotateQuaternion(Quaternion.Euler(0, xRotation + 180, 0), 0.2f);
            //    head.transform.DORotateQuaternion(Quaternion.Euler(yRotation, xRotation + 180, 0), 0.2f);
            //    xRotation += 180;
            //}
            //if (Input.GetKeyDown(KeyCode.C))
            //{
            //    transform.DORotateQuaternion(Quaternion.Euler(0, xRotation, 0), 0.2f);
            //    head.transform.DORotateQuaternion(Quaternion.Euler(yRotation, xRotation, 0), 0.2f);
            //}
            //Promo
        }
        flashlight.transform.rotation = Quaternion.Lerp(flashlight.transform.rotation, head.transform.rotation, Time.deltaTime * 25);
        flashlight.transform.position = head.transform.position;
    }
}
