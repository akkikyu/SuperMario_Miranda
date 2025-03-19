using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float playerSpeed = 4.5f;

    public int direction = 1;

    private float inputHorizontal;

    public Rigidbody2D rigidBody;

    public float jumpForce = 15f;

    public GrowndSensor _groundSensor;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    private AudioSource _audioSource;

    public AudioClip jumpSFX;

    

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        _groundSensor = GetComponentInChildren<GrowndSensor>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //esto teletransporta al personaje
        //transform.position = new Vector3(-92.13f,4.9f,0);

        
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && _groundSensor.isGrounded == true)
        {
            Jump();
        }

        Movement();

        _animator.SetBool("IsJumping", !_groundSensor.isGrounded);

      /*if(_groundSensor.isGrounded)
        {
            _animator.SetBool("IsJumping", false);
        }
        else
        {
            _animator.SetBool("IsJumping", true);
        }*/

        //transform.position = new Vector3(transform.position.x + direction * playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        //transform.Translate(new Vector3(direction * playerSpeed * Time.deltaTime, 0, 0));
        //es para controlar desde el inspector de Unity 
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + inputHorizontal, transform.position.y), playerSpeed * Time.deltaTime);
       
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
            _spriteRenderer.flipX = false;
            _animator.SetBool("IsRunning", true); 
        }

      else if(inputHorizontal < 0)
        {
            _spriteRenderer.flipX = true;
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
}