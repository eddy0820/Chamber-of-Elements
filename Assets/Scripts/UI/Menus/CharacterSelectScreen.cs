using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterSelectScreen : AbstractSelectScreen
{
    [Header("Other")]
    [SerializeField] Sprite affinityNoneSprite;
    [SerializeField] GameObject backArrow;
    [SerializeField] GameObject nextArrow;

    protected override void OnAwake()
    {
        int counter = 0;

        foreach(PlayerObject player in characterDatabase.PlayerCharacters)
        {
            counter++;

            if(counter > 3)
            {
                return;
            }

            GameObject obj = Instantiate(characterCardPrefab, Vector3.zero, Quaternion.identity, characterSelectGrid.transform);

            obj.GetComponent<CharacterSelectionCardHolder>().characterObject = player;

            obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = player.Name;

            if(player.AffinityType == AffinityTypes.None)
            {
                obj.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = affinityNoneSprite;
            }
            else
            {
                obj.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = affinityDatabase.GetAffinity[player.AffinityType].Sprite;
            }

            obj.transform.GetChild(2).GetComponent<Image>().sprite = player.Sprite;
            obj.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = player.CardPosition;
            obj.transform.GetChild(2).GetComponent<RectTransform>().localScale = player.CardScale;
            obj.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health: " + player.BaseStats.Stats.Find(x => x.StatType == maxHealthStatType).Value;
            obj.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Basic Attack: " + player.BaseStats.Stats.Find(x => x.StatType == basicAttackStatType).Value;

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnCharacterCardEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnCharacterCardExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, (data) => { OnCharacterClick(obj, (PointerEventData)data); });
        }
    }

    protected override void Initialize() {}

    protected override void UpdateInterface() {}

    protected override void UpdateMouseObjectTransform() {}

    public void OnCharacterCardEnter(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = obj.GetComponent<CharacterSelectionCardHolder>().characterObject.AnimatorController;
        obj.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Menu");

        if(obj != MainMenuScreenSwitch.Instance.DataHolder.PlayerGO)
        {
            obj.transform.GetChild(0).GetComponent<Image>().color = hoverColor;
        }
    }

    public void OnCharacterCardExit(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = null;

        if(obj != MainMenuScreenSwitch.Instance.DataHolder.PlayerGO)
        {
            obj.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        }
    }    

    public void OnCharacterClick(GameObject obj, PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            SetPlayer(obj);
        }
    }

    public void BackButtonSelected()
    {
        backArrow.SetActive(true);
    }

    public void BackButtonDeselected()
    {
        backArrow.SetActive(false);
    }

    public void NextButtonSelected()
    {
        nextArrow.SetActive(true);
    }

    public void NextButtonDeselected()
    {
        nextArrow.SetActive(false);
    }

    private void SetPlayer(GameObject obj)
    {
        if(MainMenuScreenSwitch.Instance.DataHolder.Player != null)
        {
            if(MainMenuScreenSwitch.Instance.DataHolder.PlayerGO != null)
            {
                MainMenuScreenSwitch.Instance.DataHolder.PlayerGO.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
            }
        }

        MainMenuScreenSwitch.Instance.DataHolder.SetPlayer(obj);

        obj.transform.GetChild(0).GetComponent<Image>().color = selectedColor;
    }

    public void UnsetPlayer()
    {
        if(MainMenuScreenSwitch.Instance.DataHolder.Player != null)
        {
            if(MainMenuScreenSwitch.Instance.DataHolder.PlayerGO != null)
            {
                MainMenuScreenSwitch.Instance.DataHolder.PlayerGO.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
            }
           
            MainMenuScreenSwitch.Instance.DataHolder.UnSetPlayer();
        }
    }
}
