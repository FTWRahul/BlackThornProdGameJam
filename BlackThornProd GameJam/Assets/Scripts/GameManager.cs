﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject gameOverPanel;
    //public GameObject pausePanel;
    public GameObject _Player;
    public GameManager gameMng;

    public GameObject[] targetPlanet;
    public int intPlanetIndex;

   // public string Truck;
    //public string Main;
    //private bool isPaused;
    //public bool otherPanelOpen;

    // Use this for initialization
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        if (gameMng == null)
        {
            gameMng = this.gameObject.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        //gameOverPanel.SetActive(true);
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
