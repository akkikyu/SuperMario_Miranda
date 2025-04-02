using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    public AudioClip _deathSFX;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;

    public int direction = 1;
    public float speed = 2.5f;
    
    void Awake ()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(direction * speed, _rigidBody.velocity.y);
    }

    public void Death()
    {
        direction = 0;
        _rigidBody.gravityScale = 0;
        _animator.SetTrigger("isDead");
        _audioSource.clip = _deathSFX;
        _audioSource.Play();
        _boxCollider.enabled = false;
        Destroy(gameObject, 0.3f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tuberia") || collision.gameObject.layer == 6 || collision.gameObject.CompareTag("Tope Goomba"))
        {
            direction *= -1;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            PlayerControl playerScript = collision.gameObject.GetComponent<PlayerControl>();
            playerScript.Death();
        }
    }

    void OnBecameVisible()
    {
        speed = 2.5f;
    }

    void OnbecameInvisible()
    {
        speed = 0;
    }
}
