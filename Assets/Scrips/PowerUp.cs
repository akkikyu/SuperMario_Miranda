using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;

    public int direction = 1;
    public float speed = 2.5f;

    private AudioSource _audioSource;
    public AudioClip _powerupSFX;
    private SpriteRenderer _renderer;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(direction * speed, _rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tuberia") || collision.gameObject.layer == 6)
        {
            direction *= -1;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            PlayerControl playerScript = collision.gameObject.GetComponent<PlayerControl>();
            playerScript.canShoot = true;
            playerScript.powerUpTimer = 0;
            Death();
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

    public void Death()
    {
        direction = 0;
        _rigidBody.gravityScale = 0;
        _boxCollider.enabled = false;
        _renderer.enabled = false;
        _audioSource.PlayOneShot(_powerupSFX);
        Destroy(gameObject, _powerupSFX.length);
    }
}
