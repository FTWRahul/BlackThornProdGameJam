using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Planet> objPlanet;

    //public GameObject _Player;

    public GameManager gameMng;

    // Indices for the planets
    public int intCurrentPlanetIndex;
    public int intTargetPlanetIndex;
    public int intPlayerScore;

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
    public TextMeshProUGUI textPlayerScore;

    public string Main;
    public string Master;
    public bool blnPaused;
    //private bool isPaused;
    //public bool otherPanelOpen;

    // Use this for initialization
    void Start()
    {
        // Initialize planets
        Planet newPlanet = new Planet(100, false, true, false);
        //objPlanet.Add(newPlanet);

        objPlanet[intCurrentPlanetIndex].blnCurrent = true;

        intPlayerScore = 0;

        //_Player = GameObject.FindGameObjectWithTag("Player");

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
        Debug.Log("Starting");
        intPlayerScore++;
        textPlayerScore.text = intPlayerScore.ToString();
        Debug.Log(intPlayerScore);

    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
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
        SceneManager.LoadScene(Master);
    }
    public void MainMenu()
    {
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
}
