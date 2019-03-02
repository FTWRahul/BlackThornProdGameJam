using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Game and Global Managers
    public GameManager gameMng;
    public GlobalManager globalMng;

    // Player game object
    public RotatePlayer player;
    public int intScoreMultiplier = 1;
    public int intMultiplierCounter;
    public int intMultiplierCap;
    public int intMaxMultiplier;

    // Arrays for planets and spawners
    public List<Planet> objPlanet; // Planets from the Planet class
    private RiftEnemySpawnner[] arrSpawners;

    // Indices for the planets
    public int intCurrentPlanetIndex;
    public int intTargetPlanetIndex;
    public int intPlayerScore;

    // Counter of enemies and waves remaining
    public int intEnemiesRemaining;
    public int intWavesRemaining;

    //Speed of player depending on the planet he is on
    public float speedPlanetSmall;
    public float speedPlanetMed;
    public float speedPlanetLarge;

    // Animation times
    public float fltAnimaDestroyEnemy;
    public float fltAnimaDestroyBullet;

    // Comfort distance between player and surface of the planet
    public float fltPlayerDistPlanet;
    public float planetRotation;


    // Time to destroy the bullets
    public float fltDestroyBullet;

    //UI refrences
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject inGamePanel;
    //public TextMeshProUGUI multiplierText;
    public GameObject multiplierValue;
    public Image imgCheckMark;
    public GameObject[] loseText;
    public GameObject[] winText;
    public TextMeshProUGUI textPlayerScore;
    public TextMeshProUGUI textEndGameScore;
    public TextMeshProUGUI textEndMessage1;
    public TextMeshProUGUI textEndMessage2;
    public TextMeshProUGUI textPlayerNeedsMedals;
    public Image imgEndLevel;
    public Button btnRestartLevel;
    public Button btnMainMenu;
    public Button btnExitGame;

    //Scene management and level strings
    public string Main;
    public string Master;
    public string level1;
    public string level2;
    public string level3;
    public string level4;
    public string level5;
    public string currentLevel;

    // Boolean for planet destruction
    public bool blnAllPlanetsAlive;

    // Booleans for the panels
    public bool blnPaused;
    //public bool otherPanelOpen;

    // Use this for initialization
    void Start()
    {
        planetRotation = Random.Range(0, 360);

        // Assign a Game Manager if there is not one
        if (gameMng == null)
        {
            gameMng = this.gameObject.GetComponent<GameManager>();
        }
        globalMng = FindObjectOfType<GlobalManager>();

        // Assign initial values
        intPlayerScore = 0;
        blnAllPlanetsAlive = true;

        // Assign objects to the game manager
        objPlanet[intCurrentPlanetIndex].blnCurrent = true;
        player = FindObjectOfType<RotatePlayer>();
        arrSpawners = FindObjectsOfType<RiftEnemySpawnner>();

        // Assign the win and lose texts
        gameOverPanel.SetActive(true);
        winText = GameObject. FindGameObjectsWithTag("Win");
        loseText = GameObject.FindGameObjectsWithTag("Lose");
        gameOverPanel.SetActive(false);

        currentLevel = SceneManager.GetActiveScene().name.ToString();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            CheckPauseState();
        }
        if (player.drawRay)
        {
            for (int i = 0; i < arrSpawners.Length; i++)
            {
                for (int j = 0; j < objPlanet.Count; j++)
                {
                    //Debug.DrawRay(objPlanet[j].transform.position, arrSpawners[i].transform.position - objPlanet[j].transform.position, Color.blue);
                    for (int k = 0; k< objPlanet.Count; k++)
                    {
                        Debug.DrawRay(objPlanet[k].transform.position, objPlanet[j].transform.position - objPlanet[k].transform.position, Color.yellow);

                    }

                }
                //Debug.DrawRay(objPlanet[i].transform.position, arrSpawners[i].transform.position - objPlanet[i].transform.position, Color.blue);
            }
            for (int i =0; i<objPlanet.Count;i++)
            {
                //Debug.DrawRay(objPlanet[i].transform.position, player.transform.position - objPlanet[i].transform.position, Color.blue);
            }

        }
    }

    // Check which planet is the target
    public void FindTarget() {
        for (int i = 0; i < objPlanet.Count; i++) {
            if (objPlanet[i].blnTarget) {
                intTargetPlanetIndex = i;
                break;
            }
        }
    }

    // Manage which planet is the target and which one is the current
    public void ManageTargets() {
        objPlanet[intCurrentPlanetIndex].blnCurrent = false;
        objPlanet[intTargetPlanetIndex].blnTarget = false;
        objPlanet[intTargetPlanetIndex].blnCurrent = true;
        intCurrentPlanetIndex = intTargetPlanetIndex;
        objPlanet[intCurrentPlanetIndex].SetPlayerSpeeds();
    }

    //Updates the score text on UI canvas
    public void IncreaseScore()
    {
        //Debug.Log("Starting");
        intPlayerScore = intPlayerScore + intScoreMultiplier;
        textPlayerScore.text = intPlayerScore.ToString();
        //Debug.Log(intPlayerScore);
    }

    public void IncreaseMultiplier()
    {
        multiplierValue.SetActive(true);
        multiplierValue.GetComponent<TextMeshProUGUI>().text = "x" + intScoreMultiplier.ToString();
        if (intMultiplierCap < 10)
        {
            if (intMultiplierCounter < 5)
            {
                intMultiplierCounter++;
            }
            else
            {
                intScoreMultiplier += 2;
                intMultiplierCounter = 0;
            }
        }

    }


    // Check if the player has won the level
    //public void CheckForWin() {
    //    if (intEnemiesRemaining < 1) {
    //        Debug.Log(intWavesRemaining);
    //        if (CheckForSpawners() && intWavesRemaining < 1) {
    //            arrSpawners = FindObjectsOfType<RiftEnemySpawnner>();
    //            for (int i = 0; i < arrSpawners.Length; i++) {
    //                Debug.Log("SPAWN NEXT WAVE");
    //                arrSpawners[i].fltMaxSpawnTime = 10;
    //                arrSpawners[i].Start();
    //            }

    //        } else if (!CheckForSpawners()) {
    //        // Good ending for the level
    //            Debug.Log("YOU WIN!!");
    //            gameMng.EndLevel();
    //        }
    //    }
    //}

    public void CheckForWin() {
        StartCoroutine(CheckBuffer());
    }

    IEnumerator CheckBuffer()
    {
        //yield return new WaitForSeconds(3f);
        yield return new WaitForEndOfFrame();
        if (intEnemiesRemaining < 1) {
            Debug.Log(intWavesRemaining);
            if (CheckForSpawners() && intWavesRemaining < 1) {
                arrSpawners = FindObjectsOfType<RiftEnemySpawnner>();
                for (int i = 0; i < arrSpawners.Length; i++) {
                    Debug.Log("SPAWN NEXT WAVE");
                    arrSpawners[i].fltMaxSpawnTime = 10;
                    arrSpawners[i].Start();
                }

            } else if (!CheckForSpawners()) {
            // Good ending for the level
                Debug.Log("YOU WIN!!");
                gameMng.EndLevel();
            }
        }
    }

    // Check if there are any spawners left in the level
    public bool CheckForSpawners() {
        arrSpawners = FindObjectsOfType<RiftEnemySpawnner>();
        if (arrSpawners.Length < 1) {
            return false;
        } else {
            return true;
        }
    }

    // Activate/Deactivate loss text and deactivate/activate win text
    public void FlipWinLossTexts(GameObject[] inDeactivate, GameObject[] inActivate) {
        
        for (int i = 0; i < loseText.Length; i++) {
            inDeactivate[i].SetActive(false);
            inActivate[i].SetActive(true);
        }
    }

    // Deactivate the health bars of the planets
    public void DeactivateHealthBars() {
        for (int i = 0; i < objPlanet.Count; i++) {
            objPlanet[i].sliderHealth.gameObject.SetActive(false);
        }
    }

    // Check what is the current level and award the proper medal
    public void AwardMedal() {
        for (int i = 0; i < objPlanet.Count; i++) {
            // Check if there are dead planets
            blnAllPlanetsAlive = blnAllPlanetsAlive && !objPlanet[i].blnDead;

            if (blnAllPlanetsAlive) { // Give the proper medal
                Debug.Log("MEDAL HERE!!");
                Debug.Log(currentLevel);
                if (currentLevel == level1) {
                    globalMng.blnMedal1 = true;
                } else if (currentLevel == level2) {
                    globalMng.blnMedal2 = true;
                } else if (currentLevel == level3) {
                    globalMng.blnMedal3 = true;
                } else if (currentLevel == level4) {
                    globalMng.blnMedal4 = true;
                }
                //else if (currentLevel == level5) {
                //    globalMng.blnMedal5 = true;
                //}else if (currentLevel == "Scene_Vinicius") {
                //    globalMng.blnMedal5 = true;
                //}
                globalMng.UnlockLevel5();
            }
        }
    }

    public void CheckPauseState()
    {
       

            blnPaused = !blnPaused;
            if (blnPaused)
            {
                PauseGame();
            }
            else
            {
                settingsPanel.SetActive(false);
                controlsPanel.SetActive(false);
                UnPauseGame();
            }
        
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        DeactivateHealthBars();
        gameOverPanel.SetActive(true);
        FlipWinLossTexts(winText, loseText);
        inGamePanel.SetActive(false);
        player.gameObject.SetActive(false);
        textEndGameScore.text = intPlayerScore.ToString();

        Cursor.lockState = CursorLockMode.None;
    }
    public void EndLevel()
    {
        AwardMedal();
        Time.timeScale = 0f;
        DeactivateHealthBars();
        gameOverPanel.SetActive(true);
        FlipWinLossTexts(loseText, winText);
        inGamePanel.SetActive(false);
        player.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        globalMng.SaveFile();
        textEndGameScore.text = intPlayerScore.ToString();
        //globalMng.SaveState();
        //globalMng.LoadState();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        blnPaused = false;
    }
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Main);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(Master);
    }
    public void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Quitting Game");
    }
    public void Level1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level1);
    }
    public void Level2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level2);
    }
    public void Level3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level3);
    }
    public void Level4()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level4);
    }
    public void Level5()
    {
        if (globalMng.blnUnlock5) {
            Time.timeScale = 1;
            SceneManager.LoadScene(level5);
        }
    }
}
