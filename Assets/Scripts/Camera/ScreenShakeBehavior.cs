using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeBehavior : MonoBehaviour
{   
    static ScreenShakeBehavior instance; 
    public static ScreenShakeBehavior Instance => instance;
    float shakeTimeRemaining;
    float shakeMagnitude;
    float shakeFadeTime;
    float shakeRotation;
    float rotationMultiplier;
    Vector3 startingPos;

    [SerializeField] ShakeSettings shakeSettings;


    private void Awake()
    {
        instance = this;
        startingPos = transform.position;
    }

    private void LateUpdate()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakeMagnitude;
            float yAmount = Random.Range(-1f, 1f) * shakeMagnitude;
        
            transform.position = new Vector3(xAmount, yAmount, startingPos.z);

            shakeMagnitude = Mathf.MoveTowards(shakeMagnitude, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        else
        {
            float moveX = Mathf.MoveTowards(transform.position.x, startingPos.x, shakeFadeTime * 3 * Time.deltaTime);
            float moveY = Mathf.MoveTowards(transform.position.y, startingPos.y, shakeFadeTime * 3 * Time.deltaTime);

            transform.position = new Vector3(moveX, moveY, startingPos.z);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float length, float magnitude, float _rotationMultiplier)
    {
        shakeTimeRemaining = length;
        shakeMagnitude = magnitude;

        shakeFadeTime = magnitude / length;

        rotationMultiplier = _rotationMultiplier;
        shakeRotation = magnitude * rotationMultiplier;
    }

    public void StartShake(ShakePresets preset)
    {
        switch(preset)
        {
            case ShakePresets.Small:
                StartShake(shakeSettings.SPSmallLength, shakeSettings.SPSmallMagnitude, shakeSettings.SPSmallRotationMultiplier);
                break;

            case ShakePresets.Medium:
                StartShake(shakeSettings.SPMediumLength, shakeSettings.SPMediumMagnitude, shakeSettings.SPMediumRotationMultiplier);
                break;

            case ShakePresets.Large:
                StartShake(shakeSettings.SPLargeLength, shakeSettings.SPLargeMagnitude, shakeSettings.SPLargeRotationMultiplier);
                break;
        }
    }

    public enum ShakePresets
    {
        Small,
        Medium,
        Large
    }

    [System.Serializable]
    class ShakeSettings
    {
        public float SPSmallLength = 0.25f;
        public float SPSmallMagnitude = 0.1f;
        public float SPSmallRotationMultiplier = 7.5f;

        [HorizontalLine(color: EColor.Gray, height: 2)]

        public float SPMediumLength = 0.7f;
        public float SPMediumMagnitude = 0.3f;
        public float SPMediumRotationMultiplier = 7.5f;

        [HorizontalLine(color: EColor.Gray, height: 2)]

        public float SPLargeLength = 1.5f;
        public float SPLargeMagnitude = 0.8f;
        public float SPLargeRotationMultiplier = 7.5f;

    }
}
