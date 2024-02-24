using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    public int overallScore;
    public TMP_Text bronzeButtonText;
    public TMP_Text silverButtonText;
    public TMP_Text goldButtonText;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("ScoreOverall")){
            overallScore = PlayerPrefs.GetInt("ScoreOverall");
        }
        else{
            overallScore = 0;
        }

        if((overallScore >= 400) && !(PlayerPrefs.GetString("material") == "bronze")){
            bronzeButtonText.text = "Switch To Bronze";
        }
        else if(PlayerPrefs.GetString("material") == "bronze"){
             bronzeButtonText.text = "Unlocked";
        }
        if((overallScore >= 800) && !(PlayerPrefs.GetString("material") == "silver")){
            silverButtonText.text = "Switch to Silver";
        }
        else if(PlayerPrefs.GetString("material") == "silver"){
             silverButtonText.text = "Unlocked";
        }
        if((overallScore >= 1200) && !(PlayerPrefs.GetString("material") == "gold")){
            goldButtonText.text = "Switch to Gold";
        }
        else if(PlayerPrefs.GetString("material") == "gold"){
             goldButtonText.text = "Unlocked";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBronzeClick(){
        if(overallScore >= 400){
            PlayerPrefs.SetString("material", "bronze");
            PlayerPrefs.SetFloat("suckSpeed", 30f);
            PlayerPrefs.SetFloat("distance", 12f);
        }
    }

    public void onSilverClick(){
         if(overallScore >= 800){
            PlayerPrefs.SetString("material", "silver");
            PlayerPrefs.SetFloat("suckSpeed", 40f);
            PlayerPrefs.SetFloat("distance", 16f);
        }
    }

     public void onGoldClick(){
         if(overallScore >= 800){
            PlayerPrefs.SetString("material", "gold");
            PlayerPrefs.SetFloat("suckSpeed", 50f);
            PlayerPrefs.SetFloat("distance", 20f);
        }
    }
 }

