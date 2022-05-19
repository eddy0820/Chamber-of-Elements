using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharactersInterface : AbstractGameInterface
{
    [SerializeField] Color playerInteractColor = new Color(0, 191, 191, 35);
    [SerializeField] Color enemyInteractColor = new Color(191, 0, 0, 35);
    [SerializeField] Color weatherInteractColor = new Color(198, 32, 229, 35);
    [SerializeField] Sprite cursorUse;
    [SerializeField] Sprite cursorAttack;
   
    GameObject playerInteract;
    GameObject enemyInteract;
    GameObject weatherInteract;

    protected override void OnAwake()
    {
        playerInteract = transform.GetChild(0).gameObject;
        enemyInteract = transform.GetChild(1).gameObject;
        weatherInteract = transform.GetChild(2).gameObject;
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

        AddEvent(weatherInteract, EventTriggerType.PointerEnter, delegate { OnEnterWeather(weatherInteract); });
        AddEvent(weatherInteract, EventTriggerType.PointerExit, delegate { OnExitWeather(weatherInteract); });
        AddEvent(weatherInteract, EventTriggerType.PointerClick, (data) => { OnWeatherClick(weatherInteract, (PointerEventData)data); });
        weatherInteract.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    protected override void UpdateInterface() {}

    protected override void UpdateMouseObjectTransform()
    {
        if(GameManager.Instance.mouseElement.cursorTextObj != null)
        {
            GameManager.Instance.mouseElement.cursorTextObj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    private void OnEnterPlayer(GameObject obj)
    {
        obj.GetComponent<Image>().color = playerInteractColor;

        CreateCursorTextObj(cursorUse);
    }

    private void OnExitPlayer(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        GameManager.Instance.mouseElement.RemoveMouseCursorText(Destroy);
    }

    private void OnPlayerClick(GameObject obj, PointerEventData eventData)
    {
        if(GameStateManager.Instance.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(GameManager.Instance.mouseElement.obj != null)
                {
                    ElementObject element = GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID];

                    if(element.Type == ElementTypes.Utility)
                    {
                        if(GameManager.Instance.IsImmuneElementAndAffinity(Player.Instance, GameManager.Instance.mouseElement.element) == false)
                        {
                            if(!(((UtilityElementObject) element).DoHealInBehavior) && ((UtilityElementObject) element).HealAmount >= 0)
                            {
                                Player.Instance.Stats.Heal(((UtilityElementObject) element).HealAmount, Player.Instance);
                            }

                            if(element.Behavior.DoBehavior(element))
                            {
                                GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                            }
                        } 
                    }
                }
            }
            else if(eventData.button == PointerEventData.InputButton.Right) {}
        }
    }

    private void OnEnterEnemy(GameObject obj)
    {
        obj.GetComponent<Image>().color = enemyInteractColor;

        CreateCursorTextObj(cursorAttack);
    }

    private void OnExitEnemy(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        GameManager.Instance.mouseElement.RemoveMouseCursorText(Destroy);
    }

    private void OnEnemyClick(GameObject obj, PointerEventData eventData)
    {
        if(GameStateManager.Instance.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                ElementObject element = null;

                if(UnityEngine.Random.Range(0, 101) > Player.Instance.Stats.Stats["HitChance"].value)
                {
                    Debug.Log("Miss");

                    GameStateManager.Instance.playerTurnGameState.GoToNextState();
                    GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                }
                else if(GameManager.Instance.mouseElement.obj != null)
                {
                    element = GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID];

                    if(element.Type != ElementTypes.Arena || element.Type != ElementTypes.Utility)
                    {
                        if(element.Damage >= 0)
                        {
                            if(GameManager.Instance.IsImmuneElementAndAffinity(GameManager.Instance.Enemy, GameManager.Instance.mouseElement.element) == false)
                            {
                                if(!element.DoAttackInBehavior)
                                {
                                    GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID].Damage);
                                }

                                if(element.Behavior.DoBehavior(element))
                                {
                                    GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                                }  
                            }
                            else
                            {
                                GameStateManager.Instance.playerTurnGameState.GoToNextState();
                                GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                            }
                        }
                    }
                }
                else
                {
                    GameStateManager.Instance.playerTurnGameState.Attack(Player.Instance.AffinityType, Player.Instance.Stats.Stats["BasicAttack"].value);
                }
                
            }
            else if(eventData.button == PointerEventData.InputButton.Right) {}
        }
    }

    private void OnEnterWeather(GameObject obj)
    {
        obj.GetComponent<Image>().color = weatherInteractColor;

        CreateCursorTextObj(cursorUse);
    }

    private void OnExitWeather(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        GameManager.Instance.mouseElement.RemoveMouseCursorText(Destroy);
    }

    private void OnWeatherClick(GameObject obj, PointerEventData eventData)
    {
        if(GameStateManager.Instance.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(GameManager.Instance.mouseElement.obj != null)
                {
                    ElementObject element = GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID];

                    if(element.Type == ElementTypes.Arena)
                    {
                        if(!element.DoAttackInBehavior)
                        {
                            if(GameManager.Instance.IsImmuneElementAndAffinity(GameManager.Instance.Enemy, GameManager.Instance.mouseElement.element) == false)
                            {
                                GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID].Damage);
                            }
                            else
                            {
                                GameStateManager.Instance.playerTurnGameState.GoToNextState();
                                GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                            }
                        }
                        
                        if(element.Behavior.DoBehavior(element)) /* IF DOING ATTACK IN BEHAVIOR MAKE SURE YOU CHECK FOR IMMUNITIES */
                        {
                            GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                        }
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
