using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Singleton - Jeden konrektny element w ca³ym projekcie

    public static GameManager settings; // Tworzymy jeden konrektny // obiekt dla ustawieñ
    public GameObject ResetButton; // Zmienna do przycisku do restartu
    public bool inGame; // Sprwadzamy czy aktualnie gramy
    // Ile mamy punktów
    private float score;
    public Text scoreText; //Wynik wyœwietlany tekstowo

    // Zmienna na monetki
    public int coins;
    // Zmienna na text w UI
    public Text coinScoreText;

    public int HighScore;

    // Odwo³anie do powerupa Immortality z okna Project
    public Immortality immortality;

    // Odwo³anie do powerupa Immortality z okna Project
    public Magnet magnet;


    void UpdateOnScreenScore()
    {
        score += worldSpeed;
        scoreText.text = score.ToString("0");
        coinScoreText.text = coins.ToString();
    }

    private void FixedUpdate()
    {
        if (inGame == false) { return; }
        UpdateOnScreenScore();
    }

    public float worldSpeed = 0.2f;
    private void Start()
    {
        InitGame();
    }

    public void GameOver()
    {
        inGame = false;
        ResetButton.SetActive(true);
        if ((int)score > HighScore)
        {
            HighScore = (int)score;
        }

    }
    void InitGame()
    {
        ResetButton.SetActive(false);
        // Zawsze na scenie/grze bêdzie tylko jeden GameManager
        if (settings == null)
        {
            settings = this;
        }
        inGame = true; // Na starcie gry chcemy graæ

        // Je¿eli mamy zapisane monetki na komputerze
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }

        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            HighScore = 0;
            PlayerPrefs.SetInt("HighScore", 0);
        }
        UpdateOnScreenScore();

        immortality.isActive = false;
        magnet.isActive = false;

    }

    public void MagnetCollected()
    {
        if (magnet.isActive)
        {
            CancelInvoke("Magnet");
        }
        magnet.isActive = true;
        Invoke("Magnet", magnet.getDuration());       
    }
    private void Magnet()
    {
        // Moja postaæ wraca do normy
        worldSpeed -= immortality.GetSpeed();
        immortality.isActive = false;
    }






    public void ImmortalityCollected()
    {
        // Jeœli gracz ju¿ jest nieœmiertelny to musimy przestaæ 
        // odwo³ywaæ siê do tego timera z wczeœniej
        if (immortality.isActive == true)
        {
            CancelInvoke("Immortality");
        }
        // I zacz¹æ braæ pod uwagê nowy timer
        immortality.isActive = true;
        // Moja postaæ przyspiesza
       worldSpeed += immortality.GetSpeed();
       Invoke("Immortality", immortality.getDuration());

    }

    private void Immortality()
    {
        // Moja postaæ wraca do normy
        worldSpeed -= immortality.GetSpeed();
        immortality.isActive = false;
    }





    public void CollectCoins(int value = 1)
    {
        GameManager.settings.coins += value;
        PlayerPrefs.SetInt("Coins", GameManager.settings.coins);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
    }










}
