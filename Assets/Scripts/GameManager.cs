using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    [SerializeField] ElementDatabase elementDatabase;
    public ElementDatabase ElementDatabase => elementDatabase;
    [SerializeField] ElementRecipeDatabase elementRecipeDatabase;
    [SerializeField] AffinityDatabase affinityDatabase;
    public AffinityDatabase AffinityDatabase => affinityDatabase;
    [SerializeField] WeatherDatabase weatherDatabase;
    public WeatherDatabase WeatherDatabase => weatherDatabase;
    [SerializeField] InventoryObject elementSlotsInv;
    public InventoryObject ElementSlotsInv => elementSlotsInv;
    GameStateManager gameStateManager;
    public GameStateManager GameStateManager => gameStateManager;
    WeatherStateManager weatherStateManager;
    public WeatherStateManager WeatherStateManager => weatherStateManager;
    Player player;
    public Player Player => player;
    Enemy enemy;
    public Enemy Enemy => enemy;
    GameObject interfaceCanvas;
    public GameObject InterfaceCanvas => interfaceCanvas;
    GameObject infoCanvas;
    public GameObject InfoCanvas => infoCanvas;
    public MouseElement mouseElement = new MouseElement();

    [Header("Debug")]
    public bool debug;
    [SerializeField] ElementObject debugElement1;
    [SerializeField] ElementObject debugElement2;
    [SerializeField] ElementObject debugElement3;
    [SerializeField] ElementObject debugElement4;
    [SerializeField] ElementObject debugElement5;

    private void Awake()
    {
        Instance = this;
        gameStateManager = GetComponent<GameStateManager>();
        weatherStateManager = GameObject.FindObjectOfType<WeatherStateManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        interfaceCanvas = GameObject.FindObjectOfType<ElementSlotsInterface>().gameObject;
        infoCanvas = GameObject.FindObjectOfType<ElementTextDisplay>().transform.parent.gameObject;
        elementDatabase.InitElements();
        affinityDatabase.InitAffinities();
        weatherDatabase.InitWeathers();
    }

    private void Start()
    {
        elementSlotsInv.ReRollElements();
    }

    private void Update()
    {
        if(debug)
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
}
