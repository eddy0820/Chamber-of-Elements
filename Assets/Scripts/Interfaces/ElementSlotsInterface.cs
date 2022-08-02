using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ElementSlotsInterface : AbstractGameInterface
{
    [SerializeField] InventoryObject elementSlots;
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] RuntimeAnimatorController mouseElementController;
    [SerializeField] GameObject tooltip;

    GameObject[] slots;
    Dictionary<GameObject, Element> elementsDisplayed = new Dictionary<GameObject, Element>();

    protected override void OnAwake()
    {
        slots = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
    }

    protected override void Initialize()
    {
        elementsDisplayed = new Dictionary<GameObject, Element>();

        for(int i = 0; i < elementSlots.Container.elementSlots.Length; i++)
        {
            var obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(); });
            AddEvent(obj, EventTriggerType.PointerClick, (data) => { OnClick(obj, (PointerEventData)data); });

            elementsDisplayed.Add(obj, elementSlots.Container.elementSlots[i]);
        }
    }

    protected override void UpdateInterface()
    {
        foreach(KeyValuePair<GameObject, Element> _slot in elementsDisplayed)
        {
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponent<Image>().sprite = elementSlots.Database.GetElement[_slot.Value.ID].ElementTexture;
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponent<Image>().sprite = emptySlotSprite;
            }
        }
    }

    protected override void UpdateMouseObjectTransform()
    {
        if(GameManager.Instance.mouseElement.obj != null)
        {
            GameManager.Instance.mouseElement.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnEnter(GameObject obj)
    {
        GameManager.Instance.mouseElement.hoverObj = obj;

        if(elementsDisplayed.ContainsKey(obj))
        {
            GameManager.Instance.mouseElement.hoverElement.UpdateSlot(elementsDisplayed[obj]);

            UpdateToolTip(GameManager.Instance.mouseElement.hoverElement); 
        }
    }

    public void OnExit()
    {
        GameManager.Instance.mouseElement.hoverObj = null;
        GameManager.Instance.mouseElement.hoverElement = new Element();
        tooltip.SetActive(false);
    }

    public void OnClick(GameObject obj, PointerEventData eventData)
    {
        if(GameStateManager.Instance.currentState is PlayerTurnGameState)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(GameManager.Instance.mouseElement.obj == null && elementsDisplayed[obj].ID >= 0)
                {
                    OnDragStart(obj);
                }
                else
                {
                    OnDragEnd(obj);
                }
            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
        }
    }

    public void OnDragStart(GameObject obj)
    {
        var mouseObject =  new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(32, 32);
        rectTransform.localScale = new Vector3(4, 4, 1);

        mouseObject.transform.SetParent(transform.parent);

        if(elementsDisplayed[obj].ID >= 0)
        {
            var image = mouseObject.AddComponent<Image>();
            image.sprite = elementSlots.Database.GetElement[elementsDisplayed[obj].ID].ElementTexture;
            image.raycastTarget = false;

            var animator = mouseObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = mouseElementController;
        }

        GameManager.Instance.mouseElement.obj = mouseObject;
        GameManager.Instance.mouseElement.element.UpdateSlot(elementsDisplayed[obj]);

        GameManager.Instance.mouseElement.hoverElement.UpdateSlot(new Element());
        elementsDisplayed[obj].UpdateSlot(new Element()); 

        tooltip.SetActive(false);
    }

    public void OnDragEnd(GameObject obj)
    {
        var elementOnMouse = GameManager.Instance.mouseElement;
        var mouseHoverObj = elementOnMouse.hoverObj;
        var mouseHoverElement = elementOnMouse.hoverElement;
        var mouseElement = elementOnMouse.element;
       
       // Over a slot
        if(mouseHoverObj)
        {
            // There is an element in the slot
            if(mouseHoverElement.ID >= 0)
            {
                int result = elementSlots.CanCombine(elementsDisplayed[obj], mouseElement);

                // Recipe Exists
                if(result > -1)
                {
                    Element e = new Element(elementSlots.Database.GetElement[result]);

                    elementsDisplayed[mouseHoverObj].UpdateSlot(e);

                    mouseHoverObj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewElement");

                    elementOnMouse.RemoveMouseElement(Destroy);

                    mouseHoverElement.UpdateSlot(e);
                    UpdateToolTip(e);
                }
                // Affinity Recipe
                else if(result == -1)
                {
                    elementsDisplayed[mouseHoverObj].UpdateSlot(new Element());

                    Player.Instance.SwitchAffinity(mouseElement.AffinityType);

                    elementOnMouse.RemoveMouseElement(Destroy);

                    tooltip.SetActive(false);
                }
                else
                {
                    MinionObject minion = elementSlots.CanCombineMinion(elementsDisplayed[obj], mouseElement);

                    // Minion Recipe
                    if(minion != null)
                    {
                        elementsDisplayed[mouseHoverObj].UpdateSlot(new Element());
                        elementOnMouse.RemoveMouseElement(Destroy);

                        Player.Instance.Minion.CreateMinion(minion);

                        tooltip.SetActive(false);
                    }
                    // Relic Recipe
                    else
                    {
                        RelicObject relic = elementSlots.CanCombineRelic(elementsDisplayed[obj], mouseElement);

                        if(relic != null)
                        {
                            elementsDisplayed[mouseHoverObj].UpdateSlot(new Element());
                            elementOnMouse.RemoveMouseElement(Destroy);

                            Player.Instance.Relic.CreateRelic(relic);

                            tooltip.SetActive(false);
                        }
                    }
                }
            }
            // There is no element in the slot
            else
            {
                elementsDisplayed[mouseHoverObj].UpdateSlot(mouseElement);

                elementOnMouse.hoverElement.UpdateSlot(mouseElement);

                UpdateToolTip(mouseElement);

                elementOnMouse.RemoveMouseElement(Destroy);  
            }
        }
    }

    public void OnRightClick()
    {
        var elementOnMouse = GameManager.Instance.mouseElement;

        if(elementOnMouse.element.ID < 0 && Player.Instance.AffinityType != AffinityTypes.None && elementOnMouse.hoverElement.ID >= 0)
        {
            int result = elementSlots.CanCombine(elementOnMouse.hoverElement, new Element(GameManager.Instance.AffinityDatabase.GetAffinity[Player.Instance.AffinityType].RecipeElement));

            if(result > -1)
            {
                elementsDisplayed[elementOnMouse.hoverObj].UpdateSlot(new Element(elementSlots.Database.GetElement[result]));
                elementOnMouse.hoverObj.transform.GetChild(0).GetComponent<Animator>().SetTrigger("NewElement");
            }
            else if(result == -1)
            {
                elementsDisplayed[elementOnMouse.hoverObj].UpdateSlot(new Element());
            } 
        }
    }

    private void UpdateToolTip(Element element)
    {
        if(element.ID > -1)
        {
            tooltip.SetActive(true);
            tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = element.Name;
            if(element.AffinityType is AffinityTypes.None)
            {
                tooltip.transform.GetChild(1).GetComponent<Image>().sprite = Player.Instance.AffinityNoneSprite;
            }
            else
            {
                tooltip.transform.GetChild(1).GetComponent<Image>().sprite = GameManager.Instance.AffinityDatabase.GetAffinity[element.AffinityType].Sprite;
            }
            tooltip.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.ElementDatabase.GetElement[element.ID].Type.ToString();
            tooltip.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.ElementDatabase.GetElement[element.ID].Description;
        } 
    }

    public void DoEnlightenment()
    {
        int num = UnityEngine.Random.Range(0, 2);
        int midSlot = Mathf.CeilToInt(elementSlots.Container.elementSlots.Length / 2);

        switch(num)
        {
            case 0:
                elementSlots.Container.elementSlots[midSlot].UpdateSlot(new Element(elementSlots.Database.GetElement[4]));
            break;
            case 1:
                elementSlots.Container.elementSlots[midSlot].UpdateSlot(new Element(elementSlots.Database.GetElement[5]));
            break;
        }
    }
}
