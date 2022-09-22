using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance {get; private set; }
    [SerializeField] GameObject mainMenuScreen;
    public GameObject MainMenuScreen => mainMenuScreen;
    [SerializeField] GameObject howToPlayMenuScreen;
    public GameObject HowToPlayMenuScreen => howToPlayMenuScreen;
    [SerializeField] GameObject characterSelectScreen;
    public GameObject CharacterSelectScreen => characterSelectScreen;
    [SerializeField] GameObject enemySelectScreen;
    public GameObject EnemySelectScreen => enemySelectScreen;
    [SerializeField] GameObject chooseGameModeScreen;
    public GameObject ChooseGameModeScreen => chooseGameModeScreen;

    [Space(15)]

    [Scene]
    [SerializeField] string battleScene;
    public string BattleScene => battleScene;
    [SerializeField] GameObject dataHolderPrefab;

    [Space(15)]

    [SerializeField] GameObject characterSelectNext;
    public GameObject CharacterSelectNext => characterSelectNext;
    [SerializeField] GameObject characterSelectStart;
    public GameObject CharacterSelectStart => characterSelectStart;

    [Space(15)]

    [SerializeField] AffinityDatabase affinityDatabase;
    public AffinityDatabase AffinityDatabase => affinityDatabase;
    [SerializeField] protected CharacterDatabase characterDatabase;
    public CharacterDatabase CharacterDatabase => characterDatabase;

    DataHolder dataHolder;
    public DataHolder DataHolder => dataHolder;


    private void Awake()
    {
        Instance = this;
             
        affinityDatabase.InitAffinities();

        try
        {
            dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
        }
        catch {}

        if(dataHolder == null)
        {
            GameObject dataHolderObject = Instantiate(dataHolderPrefab, dataHolderPrefab.transform.position, dataHolderPrefab.transform.rotation);
            dataHolder = dataHolderObject.GetComponent<DataHolder>();
        }
    }
}
