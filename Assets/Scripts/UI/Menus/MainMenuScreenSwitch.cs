using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreenSwitch : MonoBehaviour
{
    public static MainMenuScreenSwitch Instance {get; private set; }
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] GameObject howToPlayMenuScreen;
    [SerializeField] GameObject characterSelectScreen;
    [SerializeField] GameObject enemySelectScreen;

    [Space(15)]

    [Scene]
    [SerializeField] string battleScene;
    [SerializeField] GameObject dataHolderPrefab;

    [Space(15)]

    [SerializeField] AffinityDatabase affinityDatabase;

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

    public void HowToPlayScreen()
    {
        mainMenuScreen.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(false);
        mainMenuScreen.SetActive(false);
        howToPlayMenuScreen.SetActive(true);
    }

    public void MainMenuScreen()
    {
        howToPlayMenuScreen.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        characterSelectScreen.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        characterSelectScreen.GetComponent<CharacterSelectScreen>().UnsetPlayer();
        howToPlayMenuScreen.SetActive(false);
        characterSelectScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void CharacterSelectScreen()
    {
        mainMenuScreen.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(false);
        enemySelectScreen.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        enemySelectScreen.GetComponent<EnemySelectScreen>().UnsetEnemy();
        enemySelectScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        characterSelectScreen.SetActive(true);
    }

    public void EnemySelectScreen()
    {
        characterSelectScreen.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
        characterSelectScreen.SetActive(false);
        enemySelectScreen.SetActive(true);
    }

    public void GoToBattle()
    {
        SceneManager.LoadScene(battleScene);
    }
}
