using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandera : MonoBehaviour
{
    public BoxCollider2D _touchFlag;
    private AudioSource _audioSource;
    public AudioClip _flagPoleSFX;

    private SoundManager _soundManager;


    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            FlagPole();
            _touchFlag.enabled = false;
            _soundManager.LevelFinished();
        }
        //.tag ** "Player" es otra alternativa, pero la que he puesto es más optimizad   
    }
    
    public void FlagPole()
    {
        _audioSource.clip = _flagPoleSFX;
        _audioSource.Play();
    }
}
