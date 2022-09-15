using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AbstractSelectScreen : ButtonInterface
{
    [Header("Character Card Settings")]
    [SerializeField] protected GameObject characterCardPrefab;
    [SerializeField] protected GameObject characterSelectGrid;
    [SerializeField] protected int characterCardAmount = 3;

    [Header("Card Color")]
    [SerializeField] protected Color defaultColor;
    [SerializeField] protected Color hoverColor;
    [SerializeField] protected Color selectedColor;

    protected bool playerOrEnemy;
    CharacterObject[] characters;
    CharacterObject character;
    GameObject characterGO;
    


    protected abstract void Init();

    private void Setup()
    {
        if(playerOrEnemy)
        {
            characters = MainMenuController.Instance.CharacterDatabase.PlayerCharacters;
            character = MainMenuController.Instance.DataHolder.Player;
            characterGO = MainMenuController.Instance.DataHolder.PlayerGO;
        }
        else
        {
            characters = MainMenuController.Instance.CharacterDatabase.EnemyCharacters;
            character = MainMenuController.Instance.DataHolder.Enemy;
            characterGO = MainMenuController.Instance.DataHolder.EnemyGO;
        }
    }

    protected override void Initialize()
    {
        Init();
        Setup();

        int counter = 0;

        foreach(CharacterObject c in characters)
        {
            counter++;

            if(counter > characterCardAmount)
            {
                return;
            }

            GameObject obj = Instantiate(characterCardPrefab, Vector3.zero, Quaternion.identity, characterSelectGrid.transform);

            obj.GetComponent<CharacterSelectionCardHolder>().Setup(c, playerOrEnemy);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnCharacterCardEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnCharacterCardExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, (data) => { OnCharacterClick(obj, (PointerEventData)data); });
        }
    }

    public void OnCharacterCardEnter(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        obj.transform.GetChild(2).GetComponent<Image>().overrideSprite = null;
        obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = obj.GetComponent<CharacterSelectionCardHolder>().CharacterObject.AnimatorController;
        obj.transform.GetChild(2).GetComponent<Animator>().SetTrigger("Menu");

        if(obj != characterGO)
        {
            obj.transform.GetChild(0).GetComponent<Image>().color = hoverColor;
        }
    }

    public void OnCharacterCardExit(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1f, 1f, 1f);

        obj.transform.GetChild(2).GetComponent<Image>().overrideSprite = obj.GetComponent<CharacterSelectionCardHolder>().CharacterObject.Sprite;
        obj.transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = null;

        if(obj != characterGO)
        {
            obj.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        }
    }    

    public void OnCharacterClick(GameObject obj, PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            SetCharacter(obj);
        }
    }

    private void SetCharacter(GameObject obj)
    {
        if(character != null)
        {
            if(characterGO != null)
            {
                characterGO.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
            }
        }

        if(playerOrEnemy)
        {
            MainMenuController.Instance.DataHolder.SetPlayer(obj);
            character = MainMenuController.Instance.DataHolder.Player;
            characterGO = MainMenuController.Instance.DataHolder.PlayerGO;
        }
        else
        {
            MainMenuController.Instance.DataHolder.SetEnemy(obj);
            character = MainMenuController.Instance.DataHolder.Enemy;
            characterGO = MainMenuController.Instance.DataHolder.EnemyGO;
        }
        

        obj.transform.GetChild(0).GetComponent<Image>().color = selectedColor;
    }

    public void UnsetCharacter()
    {
        if(character != null)
        {
            if(characterGO != null)
            {
               characterGO.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
            }

            if(playerOrEnemy)
            {
                MainMenuController.Instance.DataHolder.UnSetPlayer();
            }
            else
            {
                MainMenuController.Instance.DataHolder.UnSetEnemy();
            }
            
        }
    }
}
