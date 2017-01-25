using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour
{
    private Transform camTransform;

    private float shakeAmount = 0;
    private float shakeLenght = 0;
    private float lerpTimer = 0;
    private float currentShakeAmount;
    public AnimationCurve shakeEasing;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
    }

    public void Shake(float shakeAmount, float shakeLenght)
    {
        Debug.Log("Jump");
        this.enabled = true;
        originalPos = camTransform.localPosition;

        if (currentShakeAmount > 0)
        {
            this.shakeAmount += currentShakeAmount + shakeAmount;
            this.shakeLenght += shakeLenght + lerpTimer * shakeLenght;
            lerpTimer = 0;
        } else
        {
            this.shakeAmount = shakeAmount;
            this.shakeLenght = shakeLenght;
        }
    }

    void Update()
    {
        if (lerpTimer <= 1)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            lerpTimer += Time.deltaTime / shakeLenght;
            shakeAmount = Mathf.Lerp(shakeAmount, 0, shakeEasing.Evaluate(lerpTimer));
        }
        else
        {
            StopShaking();
        }
    }

    void StopShaking()
    {
        lerpTimer = 0f;
        shakeAmount = 0;
        shakeLenght = 0;
        camTransform.localPosition = originalPos;
        this.enabled = false;
    }
}
