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

    [SerializeField] GameObject interfaceCanvas;
    public GameObject InterfaceCanvas => interfaceCanvas;
    [SerializeField] GameObject infoCanvas;
    public GameObject InfoCanvas => infoCanvas;
    [SerializeField] GameObject winCanvas;
    public GameObject WinCanvas => winCanvas;
    [SerializeField] GameObject loseCanvas;
    public GameObject LoseCanvas => loseCanvas;
    [SerializeField] GameObject flashCanvas;
    public GameObject FlashCanvas => flashCanvas;
    [SerializeField] GameObject pauseCanvas;
    public GameObject PauseCanvas => pauseCanvas;
    
    [Space(15)]

    public MouseElement mouseElement = new MouseElement(); 

    [Header("Debug")]
    public bool debug;
    [SerializeField] ElementObject debugElement1;
    [SerializeField] ElementObject debugElement2;
    [SerializeField] ElementObject debugElement3;
    [SerializeField] ElementObject debugElement4;
    [SerializeField] ElementObject debugElement5;

    DataHolder dataHolder;
    public DataHolder DataHolder => dataHolder;

    private void Awake()
    {
        Instance = this;
    
        try
        {
            dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
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
    }

    private void Start()
    {
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
        }
    }

    private void OnApplicationQuit()
    {
        elementSlotsInv.ClearElements();
    }

    // Find another place for this
    public Character ConvertCharacterEntry(CharacterEntry characterEntry)
    {
        switch(characterEntry)
        {
            case CharacterEntry.Player:
                return Player.Instance;
            
            case CharacterEntry.Enemy:
                return GameManager.Instance.Enemy;
            default:
                return null;
        }
    }

    public void SetTurnCounter(int amount)
    {
        turnCounter = amount;
        turnCounterText.GetComponent<TextMeshProUGUI>().text = "Turn " + turnCounter;
    }
}
