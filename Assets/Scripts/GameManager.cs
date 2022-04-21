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
    [SerializeField] InventoryObject elementSlotsInv;
    public InventoryObject ElementSlotsInv => elementSlotsInv;
    GameStateManager gameStateManager;
    public GameStateManager GameStateManager => gameStateManager;
    Player player;
    public Player Player => player;
    Enemy enemy;
    public Enemy Enemy => enemy;
    GameObject interfaceCanvas;
    public GameObject InterfaceCanvas => interfaceCanvas;
    GameObject infoCanvas;
    public GameObject InfoCanvas => infoCanvas;
    public MouseElement mouseElement = new MouseElement();

    private void Awake()
    {
        Instance = this;
        gameStateManager = GetComponent<GameStateManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        interfaceCanvas = GameObject.FindObjectOfType<ElementSlotsInterface>().gameObject;
        infoCanvas = GameObject.FindObjectOfType<ElementTextDisplay>().transform.parent.gameObject;
        elementDatabase.InitElements();
        affinityDatabase.InitAffinities();
    }

    private void Start()
    {
        elementSlotsInv.ReRollElements();
    }

    private void OnApplicationQuit()
    {
        elementSlotsInv.Container.elementSlots = new Element[GameObject.Find("Game Interface Canvas").transform.GetChild(0).childCount];
    }
}
