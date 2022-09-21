using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharactersInterface : AbstractGameInterface
{   
    public static CharactersInterface Instance {get; private set; }

    [SerializeField] RectTransformPosition playerBox;
    [SerializeField] RectTransformPosition enemyBox;
    [SerializeField] RectTransformPosition weatherBox;

    [Space(15)]
    [SerializeField] RectTransformPosition weatherOutlineBox;

    [Space(15)]
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
        playerInteract.sprite.material.SetColor("_OutlineColor", playerInteract.interactColor);

        if(playerInteract.outlineInteract)
        {
            playerInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(Player.Instance.CharacterObject.OutlineInteractSize.Left, Player.Instance.CharacterObject.OutlineInteractSize.Bottom);
            playerInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-Player.Instance.CharacterObject.OutlineInteractSize.Right, -Player.Instance.CharacterObject.OutlineInteractSize.Top);
        }
        else
        {
            playerInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(playerBox.Left, playerBox.Bottom);
            playerInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-playerBox.Right, -playerBox.Top);
        }
        
        AddEvent(enemyInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterEnemy(enemyInteract.interactObject); });
        AddEvent(enemyInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitEnemy(enemyInteract.interactObject); });
        AddEvent(enemyInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnEnemyClick(enemyInteract.interactObject, (PointerEventData)data); });
        enemyInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        enemyInteract.sprite.material.SetColor("_OutlineColor", enemyInteract.interactColor);

        if(enemyInteract.outlineInteract)
        {
            enemyInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(GameManager.Instance.Enemy.CharacterObject.OutlineInteractSize.Left, GameManager.Instance.Enemy.CharacterObject.OutlineInteractSize.Bottom);
            enemyInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-GameManager.Instance.Enemy.CharacterObject.OutlineInteractSize.Right, -GameManager.Instance.Enemy.CharacterObject.OutlineInteractSize.Top);
        }
        else
        {
            enemyInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(enemyBox.Left, enemyBox.Bottom);
            enemyInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-enemyBox.Right, -enemyBox.Top);
        }

        AddEvent(weatherInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterWeather(weatherInteract.interactObject); });
        AddEvent(weatherInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitWeather(weatherInteract.interactObject); });
        AddEvent(weatherInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnWeatherClick(weatherInteract.interactObject, (PointerEventData)data); });
        weatherInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        weatherInteract.sprite.material.SetColor("_OutlineColor", weatherInteract.interactColor);

        if(weatherInteract.outlineInteract)
        {
            weatherInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(weatherOutlineBox.Left, weatherOutlineBox.Bottom);
            weatherInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-weatherOutlineBox.Right, -weatherOutlineBox.Top);
        }
        else
        {
            weatherInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(weatherBox.Left, weatherBox.Bottom);
            weatherInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-weatherBox.Right, -weatherBox.Top);
        }

        AddEvent(minionInteract.interactObject, EventTriggerType.PointerEnter, delegate { OnEnterMinion(minionInteract.interactObject); });
        AddEvent(minionInteract.interactObject, EventTriggerType.PointerExit, delegate { OnExitMinion(minionInteract.interactObject); });
        AddEvent(minionInteract.interactObject, EventTriggerType.PointerClick, (data) => { OnMinionClick(minionInteract.interactObject, (PointerEventData)data); });
        minionInteract.interactObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        minionInteract.sprite.material.SetColor("_OutlineColor", minionInteract.interactColor);
    }

    [ContextMenu("Reset Character Boxes")]
    public void ResetCharacterBoxes()
    {
        playerInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(playerBox.Left, playerBox.Bottom);
        playerInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-playerBox.Right, -playerBox.Top);

        enemyInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(enemyBox.Left, enemyBox.Bottom);
        enemyInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-enemyBox.Right, -enemyBox.Top);

        weatherInteract.interactObject.GetComponent<RectTransform>().offsetMin = new Vector2(weatherBox.Left, weatherBox.Bottom);
        weatherInteract.interactObject.GetComponent<RectTransform>().offsetMax = new Vector2(-weatherBox.Right, -weatherBox.Top);
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
        if(playerInteract.outlineInteract)
        {
            playerInteract.sprite.material.SetFloat("_OutlineThickness", playerInteract.outlineThickness); 
        }
        else
        {
            obj.GetComponent<Image>().color = playerInteract.interactColor;
        }
        

        CreateCursorTextObj(cursorUse);
    }

    private void OnExitPlayer(GameObject obj)
    {
        if(playerInteract.outlineInteract)
        {
            playerInteract.sprite.material.SetFloat("_OutlineThickness", 0);
        }
        else
        {
            obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        
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
        if(enemyInteract.outlineInteract)
        {
            enemyInteract.sprite.material.SetFloat("_OutlineThickness", playerInteract.outlineThickness);
        }
        else
        {
            obj.GetComponent<Image>().color = enemyInteract.interactColor;
        }

        CreateCursorTextObj(cursorAttack);
    }

    private void OnExitEnemy(GameObject obj)
    {
        if(enemyInteract.outlineInteract)
        {
            enemyInteract.sprite.material.SetFloat("_OutlineThickness", 0);
        }
        else
        {
            obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

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
        if(weatherInteract.outlineInteract)
        {
            weatherInteract.sprite.material.SetFloat("_OutlineThickness", weatherInteract.outlineThickness);
        }
        else
        {
            obj.GetComponent<Image>().color = weatherInteract.interactColor;
        }

        CreateCursorTextObj(cursorUse);
    }

    private void OnExitWeather(GameObject obj)
    {
        if(weatherInteract.outlineInteract)
        {
            weatherInteract.sprite.material.SetFloat("_OutlineThickness", 0);
        }
        else
        {
            obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }

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
            if(minionInteract.outlineInteract)
            {
                minionInteract.sprite.material.SetFloat("_OutlineThickness", minionInteract.outlineThickness);
            }

            minionText.text = ((MinionObject) Player.Instance.Minion.CharacterObject).Description;
        }
    }

    private void OnExitMinion(GameObject obj)
    {
        if(Player.Instance.MinionExists)
        {
            if(minionInteract.outlineInteract)
            {
                minionInteract.sprite.material.SetFloat("_OutlineThickness", 0);
            }

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
        public bool outlineInteract;
        public SpriteRenderer sprite;
        public float outlineThickness;
    }

    [System.Serializable]
    public class RectTransformPosition
    {
        [SerializeField] float left;
        public float Left => left;
        [SerializeField] float right;
        public float Right => right;
        [SerializeField] float top;
        public float Top => top;
        [SerializeField] float bottom;
        public float Bottom => bottom;
    }
}
