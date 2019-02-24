using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    //public GameObject pausePanel;

    public List<Planet> objPlanet;

    //public GameObject _Player;

    public GameManager gameMng;

    // Indices for the planets
    public int intCurrentPlanetIndex;
    public int intTargetPlanetIndex;

    // Animation times
    public float fltAnimaDestroyEnemy;
    public float fltAnimaDestroyBullet;

    // public string Truck;
    //public string Main;
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
        for(int i = 0; i < objPlanet.Count; i++)
        {
            if(objPlanet[i].blnTarget)
            {
                intTargetPlanetIndex = i;
                break;
            }
        }
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
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(Truck);
    }
    public void MainMenu()
    {
       // SceneManager.LoadScene(Main);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
