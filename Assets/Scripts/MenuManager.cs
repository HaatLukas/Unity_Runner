using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_Text highScoreValue;
    public TMP_Text coinsValue;
    public TMP_Text soundBtnText;


    public Powerup magnet; // 2 powerupy 
    public Powerup immortality;

    public Text MagnetLevelText; // 4 text do UI
    public Text ImmortalityLevelText;
    public Text MagnetButtonText;
    public Text ImmortalityButtonText;

    int hs = 0;
    int coins = 0;

    public GameObject mainMenuPanel; // Do podpiêcia guzików
    public GameObject upgradeStorePanel; // Do podpiêcia guzików

    public void OpenStore()
    {
        mainMenuPanel.SetActive(false);
        upgradeStorePanel.SetActive(true);
    }

    public void CloseStore()
    {
        mainMenuPanel.SetActive(true); // ??
        upgradeStorePanel.SetActive(false); //? 
    }

    private void Start()
    {

        Screen.SetResolution(1600, 1000, false);


        if (PlayerPrefs.HasKey("HighScoreValue"))
        {
            hs = PlayerPrefs.GetInt("HighScoreValue");
        }

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        mainMenuPanel.SetActive(true);
        upgradeStorePanel.SetActive(false);


        UpdateUI();
    }

    private void UpdateUI()
    {
        highScoreValue.text = hs.ToString();
        coinsValue.text = coins.ToString();

        if (SoundManager.instance.GetMuted())
        {
            soundBtnText.text = "TURN ON SOUND";
        }
        else
        {
            soundBtnText.text = "TURN OFF SOUND";
        }

        MagnetLevelText.text = magnet.ToString();
        ImmortalityLevelText.text = immortality.ToString();
        MagnetButtonText.text = magnet.UpgradeCostString();
        ImmortalityButtonText.text = immortality.UpgradeCostString();
}

    public void UpgradeImmortalityBtn()
    {
        UpgradePowerup(immortality);
    }
    public void UpgradeMagnetBtn()
    {
        UpgradePowerup(magnet);
    }
    private void UpgradePowerup(Powerup ulepszenie)
    { 
        //  AND 
        if (coins >= ulepszenie.GetNextUpgradeCost() && !ulepszenie.IsMaxedOut())
        {
            ReduceCoinsAmount(ulepszenie.GetNextUpgradeCost()); // tracimy pieni¹dze
            ulepszenie.Upgrade(); // Ulepszamy 
            UpdateUI();
        }
    }
    private void ReduceCoinsAmount(int amount)
    {
        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
    }


    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void SoundButton()
    {
        SoundManager.instance.ToggleMuted();

        UpdateUI();
    }
}
