using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorController : MonoBehaviour
{
    public static DamageIndicatorController Instance {get; private set; }
    [SerializeField] GameObject damageIndicatorPrefab;

    [Space(15)]
    [SerializeField] float defaultDissapearTimer = 0.8f;
    [SerializeField] float defaultFadeOutSpeed = 5f;
    [SerializeField] float defaultMoveYSpeed = 1.5f;
    [SerializeField] float defaultScaleFactor = 0.04f;
    float _defaultScaleFactor;

    [Space(15)]
    [SerializeField] string missIndicatorText;
    [SerializeField] string immuneIndicatorText;

    [Space(15)]
    [SerializeField] float damageIndicatorDelay = 1.0f;
    public float DamageIndicatorDelay => damageIndicatorDelay;

    private void Awake()
    {
        Instance = this;
        _defaultScaleFactor = defaultScaleFactor / 1000;
    }

    public void DoDamageIndicator(float value, Vector3 position)
    {
        GameObject gameObject = Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
        gameObject.GetComponent<DamageIndicatorBehavior>().InitializeValue(value, defaultDissapearTimer, defaultFadeOutSpeed, defaultMoveYSpeed, _defaultScaleFactor);
    }

    public void DoDamageIndicator(float value, Vector3 position, float dissapearTimer, float fadeOutSpeed, float moveYSpeed, float scaleFactor)
    {
        GameObject gameObject = Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
        gameObject.GetComponent<DamageIndicatorBehavior>().InitializeValue(value, dissapearTimer, fadeOutSpeed, moveYSpeed, scaleFactor);
    }

    public void DoMissIndicator(Vector3 position)
    {
        GameObject gameObject = Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
        gameObject.GetComponent<DamageIndicatorBehavior>().InitializeString(missIndicatorText, defaultDissapearTimer, defaultFadeOutSpeed, defaultMoveYSpeed, _defaultScaleFactor);
    }

    public void DoMissIndicator(Vector3 position, float dissapearTimer, float fadeOutSpeed, float moveYSpeed, float scaleFactor)
    {
        GameObject gameObject = Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
        gameObject.GetComponent<DamageIndicatorBehavior>().InitializeString(missIndicatorText, dissapearTimer, fadeOutSpeed, moveYSpeed, scaleFactor);
    }

    public void DoImmuneIndicator(Vector3 position)
    {
        GameObject gameObject = Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
        gameObject.GetComponent<DamageIndicatorBehavior>().InitializeString(immuneIndicatorText, defaultDissapearTimer, defaultFadeOutSpeed, defaultMoveYSpeed, _defaultScaleFactor);
    }

    public void DoImmuneIndicator(Vector3 position, float dissapearTimer, float fadeOutSpeed, float moveYSpeed, float scaleFactor)
    {
        GameObject gameObject = Instantiate(damageIndicatorPrefab, position, Quaternion.identity);
        gameObject.GetComponent<DamageIndicatorBehavior>().InitializeString(immuneIndicatorText, dissapearTimer, fadeOutSpeed, moveYSpeed, scaleFactor);
    }
}
