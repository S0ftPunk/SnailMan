using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    public Transform cameraTransform;
    public float bobbingAmount = 0.1f;

    private Vector3 initialPosition;
    private float timer = 0f;
    public float bobbingSpeed = 0.1f;

    void Start()
    {
        initialPosition = cameraTransform.localPosition;
    }

    public void CAmeraAnimation(float horizontal, float vertical,float speed)
    {
        float waveslice = 0.0f;
        bobbingSpeed = speed/10;
        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;

            Vector3 localPosition = cameraTransform.localPosition;
            localPosition.y = initialPosition.y + translateChange;
            cameraTransform.localPosition = localPosition;
        }
        else
        {
            Vector3 localPosition = cameraTransform.localPosition;
            localPosition.y = initialPosition.y;
            cameraTransform.localPosition = localPosition;
        }
    }
}
