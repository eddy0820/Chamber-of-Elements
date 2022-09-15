using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharactersInterface : AbstractGameInterface
{   
    public static CharactersInterface Instance {get; private set; }
    [SerializeField] CharacterInteractEntry playerInteract;
    [SerializeField] CharacterInteractEntry enemyInteract;
    [SerializeField] CharacterInteractEntry weatherInteract;
    [SerializeField] CharacterInteractEntry minionInteract;

    [SerializeField] Sprite cursorUse;
    [SerializeField] Sprite cursorAttack;

    [Space(15)]

    public TextMeshProUGUI minionText;

    protected override void OnAwake() 
    {
        Instance = this;
        
        minionText.text = "";
    }

    protected override void Initialize()
    {
        AddEvent(playerInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterPlayer(playerInteract.interactObject); });
        AddEvent(playerInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitPlayer(playerInteract.interactObject); });
        AddEvent(playerInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnPlayerClick(playerInteract.interactObject, (PointerEventData)data); });
        playerInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        
        AddEvent(enemyInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterEnemy(enemyInteract.interactObject); });
        AddEvent(enemyInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitEnemy(enemyInteract.interactObject); });
        AddEvent(enemyInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnEnemyClick(enemyInteract.interactObject, (PointerEventData)data); });
        enemyInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        AddEvent(weatherInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterWeather(weatherInteract.interactObject); });
        AddEvent(weatherInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitWeather(weatherInteract.interactObject); });
        AddEvent(weatherInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnWeatherClick(weatherInteract.interactObject, (PointerEventData)data); });
        weatherInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        AddEvent(minionInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterMinion(minionInteract.interactObject); });
        AddEvent(minionInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitMinion(minionInteract.interactObject); });
        AddEvent(minionInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnMinionClick(minionInteract.interactObject, (PointerEventData)data); });
        minionInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    protected override void UpdateInterface() 
    {
        if(GameManager.Instance.mouseElement.cursorTextObj != null)
        {
            GameManager.Instance.mouseElement.cursorTextObj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    private void OnEnterPlayer(GameObject obj)
    {
        obj.GetComponent<Image>().color = playerInteract.interactColor;

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
                        if(Player.Instance.IsImmuneElementAndAffinity(GameManager.Instance.mouseElement.element) == false)
                        {
                            if(!(((UtilityElementObject) element).DoHealInBehavior) && ((UtilityElementObject) element).HealAmount >= 0)
                            {
                                Player.Instance.Stats.Heal(((UtilityElementObject) element).HealAmount);
                            }

                            if(element.Behavior.DoBehavior(element, Player.Instance))
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
        obj.GetComponent<Image>().color = enemyInteract.interactColor;

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
                    DamageIndicatorController.Instance.DoMissIndicator(GameManager.Instance.Enemy.transform.position);

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
                            if(GameManager.Instance.Enemy.IsImmuneElementAndAffinity(GameManager.Instance.mouseElement.element) == false)
                            {
                                if(element.HitParticle != null)
                                {
                                    GameObject particle = Instantiate(element.HitParticle, GameManager.Instance.Enemy.transform.position, Quaternion.identity);
                                    ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
                                    particleSystem.textureSheetAnimation.SetSprite(0, element.HitParticleTexture);
                                    particleSystem.Play();
                                }
                                
                                if(!element.DoAttackInBehavior)
                                {
                                    GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID].Damage);
                                }

                                if(element.Behavior.DoBehavior(element, GameManager.Instance.Enemy))
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
        obj.GetComponent<Image>().color = weatherInteract.interactColor;

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
                            if(GameManager.Instance.Enemy.IsImmuneElementAndAffinity(GameManager.Instance.mouseElement.element) == false)
                            {
                                GameStateManager.Instance.playerTurnGameState.Attack(GameManager.Instance.mouseElement.element.AffinityType, GameManager.Instance.ElementDatabase.GetElement[GameManager.Instance.mouseElement.element.ID].Damage);
                            }
                            else
                            {
                                GameStateManager.Instance.playerTurnGameState.GoToNextState();
                                GameManager.Instance.mouseElement.RemoveMouseElement(Destroy);
                            }
                        }
                        
                        if(GameManager.Instance.Enemy.IsImmuneElementAndAffinity(GameManager.Instance.mouseElement.element) == false)
                        {
                            if(element.Behavior.DoBehavior(element, GameManager.Instance.Enemy))
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

    private void OnEnterMinion(GameObject obj)
    {
        if(Player.Instance.MinionExists)
        {
            minionText.text = ((MinionObject) Player.Instance.Minion.CharacterObject).Description;
        }
    }

    private void OnExitMinion(GameObject obj)
    {
        if(Player.Instance.MinionExists)
        {
            minionText.text = "";
        }  
    }

    private void OnMinionClick(GameObject obj, PointerEventData eventData)
    {
        if(Player.Instance.MinionExists)
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
                            if(Player.Instance.Minion.IsImmuneElementAndAffinity(GameManager.Instance.mouseElement.element) == false)
                            {
                                if(!(((UtilityElementObject) element).DoHealInBehavior) && ((UtilityElementObject) element).HealAmount >= 0)
                                {
                                    Player.Instance.Minion.Stats.Heal(((UtilityElementObject) element).HealAmount);
                                }

                                if(element.Behavior.DoBehavior(element, Player.Instance.Minion))
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

    [System.Serializable]
    struct CharacterInteractEntry
    {
        public GameObject interactObject;
        public Color interactColor;
    }
}
