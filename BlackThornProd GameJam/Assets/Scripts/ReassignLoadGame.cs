using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReassignLoadGame : MonoBehaviour {
    public GlobalManager globalMng;
    public Button loadButton;

    // Start is called before the first frame update
    void Start()
    {
        loadButton = GetComponent<Button>();
        globalMng = FindObjectOfType<GlobalManager>();
        loadButton.onClick.AddListener(LoadGameAndScene);
    }

    public void LoadGameAndScene() {
        globalMng.LoadFile();
        RetryLevel();
    }

    public void RetryLevel() {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Intro_Scene");
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}
