using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private GameManager _gameManager;

    public AudioClip bgm;
    public AudioClip gameOver;

    public float delay = 2;
    public float timer;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Start()
    {
       PlayBGM(); 
    }

    void Update()
    {
        if(!_gameManager.isPlaying)
        {
            DeathBGM();
        }
    }

    void PlayBGM()
    {
        _audioSource.clip = bgm;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void DeathBGM()
    {
        _audioSource.Stop();

        timer += Time.deltaTime;

        if(timer >= delay)
        {
            _audioSource.PlayOneShot(gameOver);
        }
    }
}
