using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicatorBehavior : MonoBehaviour
{
    TextMeshPro text;

    float dissapearTimer;
    float fadeOutSpeed;
    float moveYSpeed;
    float scaleFactor;

    public void InitializeValue(float value, float _dissapearTimer, float _fadeOutSpeed, float _moveYSpeed, float _scaleFactor)
    {
        text = GetComponent<TextMeshPro>();
        text.text = value.ToString();

        dissapearTimer = _dissapearTimer;
        fadeOutSpeed = _fadeOutSpeed;
        moveYSpeed = _moveYSpeed;
        scaleFactor = _scaleFactor;
    }

    public void InitializeString(string value, float _dissapearTimer, float _fadeOutSpeed, float _moveYSpeed, float _scaleFactor)
    {
        text = GetComponent<TextMeshPro>();
        text.text = value;

        dissapearTimer = _dissapearTimer;
        fadeOutSpeed = _fadeOutSpeed;
        moveYSpeed = _moveYSpeed;
        scaleFactor = _scaleFactor;
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0f, moveYSpeed * Time.deltaTime, 0f);

        transform.localScale += Vector3.one * scaleFactor;

        dissapearTimer -= Time.deltaTime;

        if(dissapearTimer <= 0f)
        {
            text.alpha -= fadeOutSpeed * Time.deltaTime;

            if(text.alpha <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
