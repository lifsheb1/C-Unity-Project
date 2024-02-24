using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashSelectionScreen : MonoBehaviour
{
    public GameObject trashSelectionScreen;
    public GameObject generateTrash;
    public int amountToGenerate = 330;
    
    // Start is called before the first frame update
    void Awake()
    {
        trashSelectionScreen = GameObject.Find("TrashSelectionScreen");
        generateTrash = GameObject.Find("GenerateTrash");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        generateTrash.GetComponent<GenerateTrash>().spawnObjects(amountToGenerate);
        
        GameObject.FindWithTag("Player").GetComponent<QuestController>()
            .RequestedObjectCount = amountToGenerate;
    }

    private void StartLoad(int amount)
    {
        Close();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Scene2");
        amountToGenerate = amount;
    }
    
    public void SmallAmount()
    {
        StartLoad(330);        
    }

    public void MediumAmount()
    {
        StartLoad(660);
    }

    public void LargeAmount()
    {
        StartLoad(1000);
    }

    public void Close()
    {
        trashSelectionScreen.SetActive(false);
    }

    public void Load()
    {
        trashSelectionScreen.SetActive(true);
    }
}
