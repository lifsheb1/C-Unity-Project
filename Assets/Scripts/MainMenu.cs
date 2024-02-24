using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject trashSelectionScreen;
    public void PlayGame()
    {
        // bring to screen to pick variable amounts of trash
        
        //SceneManager.LoadScene("Scene2");
        this.gameObject.SetActive(false);
        trashSelectionScreen.GetComponent<TrashSelectionScreen>().Load();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
