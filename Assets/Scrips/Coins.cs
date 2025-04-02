using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _renderer;
    
    private AudioSource _audioSource;
    public AudioClip _coinSFX;

    void Update()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
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
        _boxCollider.enabled = false;
        _renderer.enabled = false;
        _audioSource.PlayOneShot(_coinSFX);
        Destroy(gameObject, _coinSFX.length);
    }
        
}
