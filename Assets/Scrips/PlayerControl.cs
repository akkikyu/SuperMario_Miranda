using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float playerSpeed = 4.5f;

    public int direction = 1;

    private float inputHorizontal;

    public Rigidbody2D rigidBody;

    public float jumpForce = 25f;

    public GrowndSensor _groundSensor;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    private AudioSource _audioSource;

    public AudioClip jumpSFX;

    public AudioClip deathSFX;

    private BoxCollider2D _boxCollider;

    private GameManager _gameManager;

    private SoundManager _soundManager;

    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public AudioClip shootSFX;

    public bool canShoot = false;

    public float powerUpDuration = 10f;

    public float powerUpTimer;

    public Image powerUpImage;
    

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _groundSensor = GetComponentInChildren<GrowndSensor>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _soundManager = GameObject.FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    void Update()
    {
        if(!_gameManager.isPlaying)
        {
            return;
        }
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && _groundSensor.isGrounded == true)
        {
            Jump();

        }

        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }

        if(canShoot)
        {
            PowerUp();
        }
        
        Movement();

        _animator.SetBool("IsJumping", !_groundSensor.isGrounded);
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new  Vector2(inputHorizontal * playerSpeed, rigidBody.velocity.y);
        //rigidBody.AddForce(new Vector2(inputHorizontal, 0));
        //rigidBody.MovePosition(new Vector2(100, 0));
    }

    void Movement()
    {
      if(inputHorizontal > 0)
        {
           transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRunning", true); 
        }

      else if(inputHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRunning", true);       
        }
      else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _animator.SetBool("IsJumping", true);
        _audioSource.PlayOneShot(jumpSFX);
    }

    public void Death()
    {
        _animator.SetTrigger("IsDead");
        _audioSource.PlayOneShot(deathSFX);
        _boxCollider.enabled = false;
        
        Destroy(_groundSensor.gameObject);

        inputHorizontal = 0;
        rigidBody.velocity = Vector2.zero;
        
        rigidBody.AddForce(Vector2.up * jumpForce / 2, ForceMode2D.Impulse);

        //_soundManager.Invoke("DeathBGM", 2);
        StartCoroutine(_soundManager.DeathBGM());

        _gameManager.isPlaying = false;

        Destroy(gameObject, 2);

        SceneManager.LoadScene(2);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        _audioSource.PlayOneShot(shootSFX);
    }

    void PowerUp()
    {
        powerUpTimer += Time.deltaTime;

        powerUpImage.fillAmount = Mathf.InverseLerp(powerUpDuration, 0, powerUpTimer);

        if(powerUpTimer >= powerUpDuration)
        {
            canShoot = false;
            powerUpTimer = 0;
        }
    }
}