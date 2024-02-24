using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public InfoPanelHideState infoPanelHideState;
    public GamePointState gamePointState;

    public PauseMenu pauseMenu;
    private bool isFrozen = false;
    
    public Camera mainCamera, uiCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Debug.Log("Scene reloaded");
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    
    // Update is called once per frame

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // load in hidden panels
        if (infoPanelHideState == null)
            infoPanelHideState = new();

        if (gamePointState == null)
            gamePointState = new();

        // bug: enable UI camera on start since maincamera won't be detected
        uiCamera.gameObject.SetActive(true);
    }
    
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        if(!pauseMenu.pauseMenuUI.activeSelf) // if pause menu not already open
        {
            Cursor.visible = true;
            pauseMenu.Load();
            Freeze();
        }
        else // in both screens
        {
            pauseMenu.Close(); // can make this an unpause
            Cursor.visible = false;
            Unfreeze();
        }
    }

    public static bool GetIsFrozen()
    {
        return Instance.isFrozen; 
    }

    public void Unfreeze()
    {
        Time.timeScale = 1f;
        isFrozen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Freeze()
    {
        Time.timeScale = 0f;
        isFrozen = true;
        Cursor.lockState = CursorLockMode.None;
    }
}