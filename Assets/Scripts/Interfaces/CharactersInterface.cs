using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharactersInterface : AbstractGameInterface
{
    [SerializeField] Color playerInteractColor = new Color(0, 255, 255, 35);
    [SerializeField] Color enemyInteractColor = new Color(255, 0, 0, 35);
    [SerializeField] Sprite cursorUse;
    [SerializeField] Sprite cursorAttack;
   
    GameObject playerInteract;
    GameObject enemyInteract;

    protected override void OnAwake()
    {
        playerInteract = transform.GetChild(1).GetChild(0).gameObject;
        enemyInteract = transform.GetChild(1).GetChild(1).gameObject;
    }

    protected override void Initialize()
    {
        AddEvent(playerInteract, EventTriggerType.PointerEnter, delegate { OnEnterPlayer(playerInteract); });
        AddEvent(playerInteract, EventTriggerType.PointerExit, delegate { OnExitPlayer(playerInteract); });
        AddEvent(playerInteract, EventTriggerType.PointerClick, (data) => { OnPlayerClick(playerInteract, (PointerEventData)data); });
        playerInteract.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        
        AddEvent(enemyInteract, EventTriggerType.PointerEnter, delegate { OnEnterEnemy(enemyInteract); });
        AddEvent(enemyInteract, EventTriggerType.PointerExit, delegate { OnExitEnemy(enemyInteract); });
        AddEvent(enemyInteract, EventTriggerType.PointerClick, (data) => { OnEnemyClick(enemyInteract, (PointerEventData)data); });
        enemyInteract.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    protected override void UpdateInterface() {}

    protected override void UpdateMouseObjectTransform()
    {
        if(GameManager.Instance.mouseElement.cursorTextObj != null)
        {
            GameManager.Instance.mouseElement.cursorTextObj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnEnterPlayer(GameObject obj)
    {
        obj.GetComponent<Image>().color = playerInteractColor;

        CreateCursorTextObj(cursorUse);
    }

    public void OnExitPlayer(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        GameManager.Instance.mouseElement.RemoveMouseCursorText(Destroy);
    }

    public void OnPlayerClick(GameObject obj, PointerEventData eventData)
    {
        if(GameManager.Instance.GameStateManager.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(GameManager.Instance.mouseElement.obj != null)
                {
                    ElementObject element = GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID];

                    if(element.Type == ElementTypes.Utility)
                    {
                        if(!(((UtilityElementObject) element).DoHealInBehavior))
                        {
                            GameManager.Instance.Player.Stats.Heal(((UtilityElementObject) element).HealAmount, GameManager.Instance.Player);
                        }

                        if(element.Behavior.DoBehavior(element))
                        {
                            GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                        }
                    }
                }
            }
            else if(eventData.button == PointerEventData.InputButton.Right) {}
        }
    }

    public void OnEnterEnemy(GameObject obj)
    {
        obj.GetComponent<Image>().color = enemyInteractColor;

        CreateCursorTextObj(cursorAttack);
    }

    public void OnExitEnemy(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        GameManager.Instance.mouseElement.RemoveMouseCursorText(Destroy);
    }

    public void OnEnemyClick(GameObject obj, PointerEventData eventData)
    {
        if(GameManager.Instance.GameStateManager.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                ElementObject element = null;

                if(GameManager.Instance.mouseElement.obj != null)
                {
                    element = GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID];
                }
                
                if(GameManager.Instance.mouseElement.obj == null)
                {
                    GameManager.Instance.GameStateManager.playerTurnGameState.Attack(AffinityTypes.None, GameManager.Instance.Player.Stats.Stats["BasicAttack"].value);
                }
                else if(element.Damage >= 0)
                {
                    if(!element.DoAttackInBehavior)
                    {
                        GameManager.Instance.GameStateManager.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID].Damage);
                    }

                    if(element.Behavior.DoBehavior(element))
                    {
                        GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                    }
                }
            }
            else if(eventData.button == PointerEventData.InputButton.Right) {}
        }
    }

    private void CreateCursorTextObj(Sprite sprite)
    {
        var cursorObject =  new GameObject();
        var rectTransform = cursorObject.AddComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(256, 16);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.localScale = new Vector3(2.5f, 2.5f, 1);

        cursorObject.transform.SetParent(transform);

        var image = cursorObject.AddComponent<Image>();
        image.sprite = sprite;
        image.raycastTarget = false;

        GameManager.Instance.mouseElement.cursorTextObj = cursorObject; 
    }
}
