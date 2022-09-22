using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameModeInterface : ButtonInterface
{
    [SerializeField] GameObject adventureModeButton;
    [SerializeField] GameObject battleModeButton;

    [Header("Card Color")]
    [SerializeField] protected Color defaultColor;
    [SerializeField] protected Color hoverColor;

    public void MainMenuScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.ChooseGameModeScreen.SetActive(false);
        MainMenuController.Instance.MainMenuScreen.SetActive(true);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        AddEvent(adventureModeButton, EventTriggerType.PointerEnter, delegate { OnButtonEnter(adventureModeButton); });
        AddEvent(adventureModeButton, EventTriggerType.PointerExit, delegate { OnButtonExit(adventureModeButton); });
        AddEvent(adventureModeButton, EventTriggerType.PointerClick, (data) => { OnAdventureButtonClick(adventureModeButton, (PointerEventData)data); });

        AddEvent(battleModeButton, EventTriggerType.PointerEnter, delegate { OnButtonEnter(battleModeButton); });
        AddEvent(battleModeButton, EventTriggerType.PointerExit, delegate { OnButtonExit(battleModeButton); });
        AddEvent(battleModeButton, EventTriggerType.PointerClick, (data) => { OnBattleButtonClick(battleModeButton, (PointerEventData)data); });
    }

    private void OnButtonEnter(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        obj.transform.GetChild(0).GetComponent<Image>().color = hoverColor;
    }

    private void OnButtonExit(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
    }

    private void OnAdventureButtonClick(GameObject obj, PointerEventData data)
    {
        MainMenuController.Instance.DataHolder.SetGameMode(GameModes.Adventure);

        MainMenuController.Instance.ChooseGameModeScreen.SetActive(false);
        MainMenuController.Instance.CharacterSelectScreen.SetActive(true);

        MainMenuController.Instance.CharacterSelectNext.SetActive(false);
        MainMenuController.Instance.CharacterSelectStart.SetActive(true);

        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
    }

    private void OnBattleButtonClick(GameObject obj, PointerEventData data)
    {
        MainMenuController.Instance.DataHolder.SetGameMode(GameModes.Battle);

        MainMenuController.Instance.ChooseGameModeScreen.SetActive(false);
        MainMenuController.Instance.CharacterSelectScreen.SetActive(true);

        MainMenuController.Instance.CharacterSelectNext.SetActive(true);
        MainMenuController.Instance.CharacterSelectStart.SetActive(false);

        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
    }
}
