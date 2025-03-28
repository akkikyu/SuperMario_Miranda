using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisteryBox : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    public AudioClip _misteryBoxSFX;
    public AudioClip _misteryBoxSFX2;
    private bool _isOpen = false;
    public Transform poweupSpawner;
    public GameObject powerupPrefab;

    void Awake ()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void ActivateBox()
    {
        if(!_isOpen)
        {
           _animator.SetTrigger("OpenBox");
          
           _audioSource.clip = _misteryBoxSFX;

           _isOpen = true;

           Spawn();
        }

        else
        {
        _audioSource.volume = 1f;
        _audioSource.clip = _misteryBoxSFX2;
        }
        _audioSource.Play();
    }
    
    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
          ActivateBox();
        }           
    }

    void Spawn()
    {
        Instantiate(powerupPrefab, poweupSpawner.position, poweupSpawner.rotation);
    }

}
