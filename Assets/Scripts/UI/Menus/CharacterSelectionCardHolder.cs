using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionCardHolder : MonoBehaviour
{
    [ReadOnly, SerializeField] CharacterObject characterObject;
    public CharacterObject CharacterObject => characterObject;

    [Header("Other")]
    [SerializeField] Sprite affinityNoneSprite;

    [Header("Used Stats for Card")]
    [SerializeField] protected StatTypeObject maxHealthStatType;
    [SerializeField] protected StatTypeObject basicAttackStatType;

    public void Setup(CharacterObject _characterObject, bool playerOrEnemy)
    {
        characterObject = _characterObject;

        transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = characterObject.Name;

        if(characterObject.AffinityType == AffinityTypes.None)
        {
            transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = affinityNoneSprite;
        }
        else
        {
            transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = MainMenuController.Instance.AffinityDatabase.GetAffinity[characterObject.AffinityType].Sprite;
        }

        transform.GetChild(2).GetComponent<Image>().sprite = characterObject.Sprite;
        transform.GetChild(2).GetComponent<Image>().overrideSprite = characterObject.Sprite;

        
        if(playerOrEnemy)
        {
            transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = ((PlayerObject) characterObject).CardPosition;
            transform.GetChild(2).GetComponent<RectTransform>().localScale = ((PlayerObject) characterObject).CardScale;
        }
        else
        {
            transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = ((EnemyObject) characterObject).CardPosition;
            transform.GetChild(2).GetComponent<RectTransform>().localScale = ((EnemyObject) characterObject).CardScale;
        }
        
        transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health: " + characterObject.BaseStats.Stats.Find(x => x.StatType == maxHealthStatType).Value;
        transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Basic Attack: " + characterObject.BaseStats.Stats.Find(x => x.StatType == basicAttackStatType).Value;
    }
}
