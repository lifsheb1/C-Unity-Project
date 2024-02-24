using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelCloseScript : MonoBehaviour
{
    public GameObject metaPanel;
    public Toggle metaDoNotShow;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnCloseClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCloseClicked()
    {
        // check if don't show is ticked
        if (metaDoNotShow.isOn)
            GameManager.Instance.infoPanelHideState.HideCurrent();
        
        // hide the current panel
        metaPanel.SetActive(false);
        
        // unfreeze
        GameManager.Instance.Unfreeze();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
