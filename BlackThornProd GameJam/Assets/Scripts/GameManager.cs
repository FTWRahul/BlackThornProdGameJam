using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Game Manager
    public GameManager gameMng;

    // Player game object
    public RotatePlayer player;

    // Arrays for planets and spawners
    //public List<Planet> objPlanet; // Planets from the Planet class
    public List<Planet> objPlanet; // Planets from the Planet class
    private RiftEnemySpawnner[] arrEnemiesRemaining;
    
    // Indices for the planets
    public int intCurrentPlanetIndex;
    public int intTargetPlanetIndex;
    public int intPlayerScore;

    // Counter of enemies remaining
    public int intEnemiesRemaining;

    // Animation times
    public float fltAnimaDestroyEnemy;
    public float fltAnimaDestroyBullet;

    // Comfort distance between player and surface of the planet
    public float fltPlayerDistPlanet;

    // Time to destroy the bullets
    public float fltDestroyBullet;

    //UI refrences
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject[] loseText;
    public GameObject[] winText;
    public TextMeshProUGUI textPlayerScore;

    //Scene management and level strings
    public string Main;
    public string Master;
    public string level1;
    public string level2;
    public string level3;
    public string level4;
    public string level5;
    public string currentLevel;

    public bool blnPaused;
    //private bool isPaused;
    //public bool otherPanelOpen;

    // Use this for initialization
    void Start()
    {
        // Assign initial values
        intPlayerScore = 0;

        // Assign objects to the game manager
        objPlanet = FindObjectsOfType<Planet>().ToList(); // transform the array of planets into a list
        objPlanet[intCurrentPlanetIndex].blnCurrent = true;
        arrEnemiesRemaining = FindObjectsOfType<RiftEnemySpawnner>();
        player = FindObjectOfType<RotatePlayer>();

        // Assign the win and lose texts
        gameOverPanel.SetActive(true);
        winText = GameObject. FindGameObjectsWithTag("Win");
        loseText = GameObject.FindGameObjectsWithTag("Lose");
        gameOverPanel.SetActive(false);

        //currentLevel = SceneManager.GetActiveScene().ToString();

        // Assign a Game Manager if there is not one
        if (gameMng == null)
        {
            gameMng = this.gameObject.GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            blnPaused = !blnPaused;

            if (blnPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
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
    }

    //Updates the score text on UI canvas
    public void IncreaseScore()
    {
        //Debug.Log("Starting");
        intPlayerScore++;
        textPlayerScore.text = intPlayerScore.ToString();
        //Debug.Log(intPlayerScore);

    }

    // Check if the player won the level
    public void CheckForWin() {
        if (intEnemiesRemaining < 1) {
            // Good ending for the level
            Debug.Log("YOU WIN!!");
            gameMng.EndLevel();
        }
    }

    // Activate/Deactivate loss text and deactivate/activate win text
    public void FlipWinLossTexts(GameObject[] inDeactivate, GameObject[] inActivate) {
        
        for (int i = 0; i < loseText.Length; i++) {
            inDeactivate[i].SetActive(false);
            inActivate[i].SetActive(true);
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        FlipWinLossTexts(winText, loseText);
        player.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
    }
    public void EndLevel()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        FlipWinLossTexts(loseText, winText);
        player.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
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
        Debug.Log("Quitting Game");
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
        Time.timeScale = 1;
        SceneManager.LoadScene(level5);
    }
}
