using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public List<Planet> objPlanet;

    //public GameObject _Player;

    public GameManager gameMng;

    // Indices for the planets
    public int intCurrentPlanetIndex;
    public int intTargetPlanetIndex;

    // Animation times
    public float fltAnimaDestroyEnemy;
    public float fltAnimaDestroyBullet;

    // Comfort distance between player and surface of the planet
    public float fltPlayerDistPlanet;

    public string Main;
    public string Master;
    //private bool isPaused;
    //public bool otherPanelOpen;

    // Use this for initialization
    void Start()
    {
        // Initialize planets
        Planet newPlanet = new Planet(100, false, true, false);
        //objPlanet.Add(newPlanet);

        objPlanet[intCurrentPlanetIndex].blnCurrent = true;



        //_Player = GameObject.FindGameObjectWithTag("Player");

        // Assign a Game Manager if there is not one
        if (gameMng == null)
        {
            gameMng = this.gameObject.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for(int i = 0; i < objPlanet.Count; i++)
        //{
        //    if(objPlanet[i].blnTarget)
        //    {
        //        intTargetPlanetIndex = i;
        //        break;
        //    }
        //}
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
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(Truck);
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
