using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    //public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        //GameIsPaused = false;


    }

    public void ResumeButtonClicked()
    {
        Close();
        GameManager.Instance.Unfreeze();
    }
    
    public void Load()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Close();
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadSceneAsync("Scene2");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Menu...");
        Application.Quit();
    }
}
