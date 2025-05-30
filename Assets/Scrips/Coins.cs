using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _renderer;
    
    private AudioSource _audioSource;
    public AudioClip _coinSFX;
    private GameManager _gameManager;

    void Update()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Coin();
        }    
    }

    void Coin()
    {
        _gameManager.AddCoins();

        _boxCollider.enabled = false;
        _renderer.enabled = false;
        _audioSource.PlayOneShot(_coinSFX);
        Destroy(gameObject, _coinSFX.length);
    }    
}
