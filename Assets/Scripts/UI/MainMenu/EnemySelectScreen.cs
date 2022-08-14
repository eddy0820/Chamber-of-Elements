using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EnemySelectScreen : AbstractSelectScreen
{
    [Header("Other")]
    [SerializeField] Sprite affinityNoneSprite;
    [SerializeField] GameObject backArrow;
    [SerializeField] GameObject battleArrow;

    protected override void OnAwake()
    {
        int counter = 0;

        foreach(EnemyObject enemy in characterDatabase.EnemyCharacters)
        {
            counter++;

            if(counter > 3)
            {
                return;
            }

            GameObject obj = Instantiate(characterCardPrefab, Vector3.zero, Quaternion.identity, characterSelectGrid.transform);

            obj.GetComponent<CharacterSelectionCardHolder>().characterObject = enemy;

            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = enemy.Name;

            if(enemy.AffinityType == AffinityTypes.None)
            {
                obj.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = affinityNoneSprite;
            }
            else
            {
                obj.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = affinityDatabase.GetAffinity[enemy.AffinityType].Sprite;
            }

            obj.transform.GetChild(2).GetComponent<Image>().sprite = enemy.Sprite;
            obj.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = enemy.CardPosition;
            obj.transform.GetChild(2).GetComponent<RectTransform>().localScale = enemy.CardScale;
            obj.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health: " + enemy.BaseStats.Stats.Find(x => x.StatType == maxHealthStatType).Value;
            obj.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Basic Attack: " + enemy.BaseStats.Stats.Find(x => x.StatType == basicAttackStatType).Value;

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnCharacterCardEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnCharacterCardExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, (data) => { OnCharacterClick(obj, (PointerEventData)data); });
        }
    }

    public void OnCharacterCardEnter(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = obj.GetComponent<CharacterSelectionCardHolder>().characterObject.AnimatorController;
        obj.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Menu");

        if(obj != dataHolder.EnemyGO)
        {
            obj.transform.GetChild(0).GetComponent<Image>().color = hoverColor;
        }
    }

    public void OnCharacterCardExit(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = null;

        if(obj != dataHolder.EnemyGO)
        {
            obj.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        }
    }    

    public void OnCharacterClick(GameObject obj, PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            SetEnemy(obj);
        }
    }

    protected override void Initialize() {}

    protected override void UpdateInterface() {}

    protected override void UpdateMouseObjectTransform() {}

    public void BackButtonSelected()
    {
        backArrow.SetActive(true);
    }

    public void BackButtonDeselected()
    {
        backArrow.SetActive(false);
    }

    public void BattleButtonSelected()
    {
        battleArrow.SetActive(true);
    }

    public void BattleButtonDeselected()
    {
        battleArrow.SetActive(false);
    }

    private void SetEnemy(GameObject obj)
    {
        if(dataHolder.Enemy != null)
        {
            dataHolder.EnemyGO.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        }

        dataHolder.SetEnemy(obj);

        obj.transform.GetChild(0).GetComponent<Image>().color = selectedColor;
    }

    public void UnsetEnemy()
    {
        if(dataHolder.Enemy != null)
        {
            dataHolder.EnemyGO.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
            dataHolder.UnSetEnemy();
        }
    }
}
