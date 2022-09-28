using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    [SerializeField] ElementDatabase elementDatabase;
    public ElementDatabase ElementDatabase => elementDatabase;
    [SerializeField] AffinityDatabase affinityDatabase;
    public AffinityDatabase AffinityDatabase => affinityDatabase;
    [SerializeField] WeatherDatabase weatherDatabase;
    public WeatherDatabase WeatherDatabase => weatherDatabase;
    [SerializeField] InventoryObject elementSlotsInv;
    public InventoryObject ElementSlotsInv => elementSlotsInv;
    [SerializeField] GameObject dataHolderPrefab;

    [Space(15)]

    [ReadOnly, SerializeField] int turnCounter;
    public int TurnCounter => turnCounter;
    [SerializeField] GameObject turnCounterText;
 
    Enemy enemy;
    public Enemy Enemy => enemy;

    [Space(15)]

    [SerializeField] GameObject winCanvasBattle;
    [SerializeField] GameObject loseCanvasBattle;
    [SerializeField] GameObject pauseCanvasBattle;

    [Space(10)]

    [SerializeField] GameObject winCanvasAdventure;
    [SerializeField] GameObject loseCanvasAdventure;
    [SerializeField] GameObject pauseCanvasAdventure;

    GameObject winCanvas;
    public GameObject WinCanvas => winCanvas;
    GameObject loseCanvas;
    public GameObject LoseCanvas => loseCanvas;
    GameObject pauseCanvas;
    public GameObject PauseCanvas => pauseCanvas;

    [Space(10)]

    [SerializeField] GameObject flashCanvas;
    public GameObject FlashCanvas => flashCanvas;
    
    [Space(15)]

    public MouseElement mouseElement = new MouseElement(); 

    [Header("Debug")]
    public bool debug;
    [SerializeField] ElementObject debugElement1;
    [SerializeField] ElementObject debugElement2;
    [SerializeField] ElementObject debugElement3;
    [SerializeField] ElementObject debugElement4;
    [SerializeField] ElementObject debugElement5;
    [SerializeField] ElementObject debugElement6;
    [SerializeField] ElementObject debugElement7;
    [SerializeField] ElementObject debugElement8;
    [SerializeField] ElementObject debugElement9;
    [SerializeField] ElementObject debugElement0;

    DataHolder dataHolder;
    public DataHolder DataHolder => dataHolder;
    RunTracker runTracker;
    public RunTracker RunTracker => runTracker;

    private void Awake()
    {
        Instance = this;
    
        try
        {
            dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            runTracker = GameObject.FindWithTag("RunTracker").GetComponent<RunTracker>();
        }
        catch {}
        
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

        elementDatabase.InitElements();
        affinityDatabase.InitAffinities();
        weatherDatabase.InitWeathers();

        turnCounter = 1;

        if(dataHolder != null)
        {
            GameObject.Find("Player").GetComponent<Player>().DoAwake(dataHolder.Player);
            enemy.DoAwake(dataHolder.Enemy);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().DoAwake(null);
            enemy.DoAwake(null);

            GameObject dataHolderObject = Instantiate(dataHolderPrefab, dataHolderPrefab.transform.position, dataHolderPrefab.transform.rotation);
            dataHolder = dataHolderObject.GetComponent<DataHolder>();
            dataHolder.SetPlayer(Player.Instance.CharacterObject as PlayerObject);
            dataHolder.SetEnemy(enemy.CharacterObject as EnemyObject);
        }  

        if(dataHolder.Mode == GameModes.Adventure)
        {
            winCanvas = winCanvasAdventure;
            loseCanvas = loseCanvasAdventure;
            pauseCanvas = pauseCanvasAdventure;

            if(runTracker.lastBattle != null)
            {
                Player.Instance.Stats.SetHealth(runTracker.PlayerHealth);
                Player.Instance.Relic.SetRelicObject(runTracker.PlayerRelic);
                Player.Instance.SetUnlockablesAdventureMode(runTracker.UnlockedAffinities, runTracker.UnlockedElementRecipes, runTracker.UnlockedMinionRecipes, runTracker.UnlockedRelicRecipes, runTracker.ReRollElements);
            } 
        }
        else
        {
            winCanvas = winCanvasBattle;
            loseCanvas = loseCanvasBattle;
            pauseCanvas = pauseCanvasBattle;
        }

        Player.Instance.Relic.DoAwake();
    }

    private void Start()
    {
        elementSlotsInv.ClearElements();
        elementSlotsInv.ReRollElements();
    }

    private void Update()
    {
        if(PauseMenu.Instance.IsGamePaused == false && debug)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                elementSlotsInv.Container.elementSlots[0].UpdateSlot(new Element(debugElement1));
            }

            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                elementSlotsInv.Container.elementSlots[1].UpdateSlot(new Element(debugElement2));
            }

            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                elementSlotsInv.Container.elementSlots[2].UpdateSlot(new Element(debugElement3));
            }

            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                elementSlotsInv.Container.elementSlots[3].UpdateSlot(new Element(debugElement4));
            }

            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                elementSlotsInv.Container.elementSlots[4].UpdateSlot(new Element(debugElement5));
            }

            if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                elementSlotsInv.Container.elementSlots[0].UpdateSlot(new Element(debugElement6));
            }

            if(Input.GetKeyDown(KeyCode.Alpha7))
            {
                elementSlotsInv.Container.elementSlots[1].UpdateSlot(new Element(debugElement7));
            }

            if(Input.GetKeyDown(KeyCode.Alpha8))
            {
                elementSlotsInv.Container.elementSlots[2].UpdateSlot(new Element(debugElement8));
            }

            if(Input.GetKeyDown(KeyCode.Alpha9))
            {
                elementSlotsInv.Container.elementSlots[3].UpdateSlot(new Element(debugElement9));
            }

            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                elementSlotsInv.Container.elementSlots[4].UpdateSlot(new Element(debugElement0));
            }
        }
    }

    private void OnApplicationQuit()
    {
        elementSlotsInv.ClearElements();
    }

    public void SetTurnCounter(int amount)
    {
        turnCounter = amount;
        turnCounterText.GetComponent<TextMeshProUGUI>().text = "Turn " + turnCounter;
    }
}

[System.Serializable]
public class MouseElement
{
    [ReadOnly] public GameObject obj;
    [ReadOnly] public Element element;
    [ReadOnly] public GameObject sourceSlot;
    [ReadOnly] public GameObject hoverObj;
    [ReadOnly] public Element hoverElement;
    [ReadOnly] public GameObject cursorTextObj;

    public MouseElement()
    {
        obj = null;
        element = new Element();
        sourceSlot = null;
        hoverObj = null;
        hoverElement = new Element();
        cursorTextObj = null;
    }

    public void RemoveMouseElement(Action<GameObject> Destroy)
    {
        Destroy(obj);
        element.UpdateSlot(new Element());
        sourceSlot = null;
    }

    public void RemoveMouseCursorText(Action<GameObject> Destroy)
    {
        Destroy(cursorTextObj);
        cursorTextObj = null;
    }
}
