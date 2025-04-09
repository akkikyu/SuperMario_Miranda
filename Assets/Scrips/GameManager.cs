using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isPlaying = true;
    public bool isPaused = false;
    private SoundManager _soundManager;
    public GameObject pauseCanvas;
    private int coins = 0;
    public Text coinsText;

    private int x = 0;
    public Text goombasText;


    void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    void Start()
    {
        coinsText.text = "Coins: " + coins.ToString();
        goombasText.text = "X " + x.ToString();
    }


    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Pause()
    {
       
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            _soundManager.PauseBGM();
            pauseCanvas.SetActive(false);
        }
        else
        {
            //Para pausar el tiempo, valor entre 0 y 1. 0(se pausa), 0.5 (mitad de velocidad), 1 (velocidad normal)
            Time.timeScale = 0;
            isPaused = true;
            _soundManager.PauseBGM();
            pauseCanvas.SetActive(true);
        }
    }

    public void AddCoins()
    {
        coins++;
        coinsText.text = "Coins: " + coins.ToString();
    }

    public void AddGoombas()
    {
        x++;
        goombasText.text = "X " + x.ToString();
    }
}

