using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //ordenarlo con el [Header("")]
    //solo funciona después de un public o un [SerializeField]

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

    [SerializeField] private float _dashForce = 20;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCoolDown = 1;
    private bool _canDash = true;
    private bool _isDashing = false;

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackDamage = 10;
    [SerializeField] private float _attackRadius = 1;
    [SerializeField] private Transform _hitBoxPosition;

    [SerializeField] private float _baseChargedAttackDamage = 15;
    [SerializeField] private float _maxChargedAttackDamage = 40;
    private float _chargedAttackDamage;



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
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(!_gameManager.isPlaying)
        {
            return;
        }

        if(_isDashing)
        {
            return;
        }  


        if(Input.GetButtonDown("Jump"))
        {
            if(_groundSensor.isGrounded || _groundSensor.canDoubleJump)
            {
                Jump();
            }
        }


        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }


        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }


        if(Input.GetButtonDown("Fire2"))
        {
            NormalAttack();
        }


        if(Input.GetButton("Fire2"))
        {
            AttackCharge();
        }

        if(Input.GetButtonUp("Fire2"))
        {
            ChargedAttack();
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
        if(_isDashing)
        {
            return;
        }

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
        if(!_groundSensor.isGrounded)
        {
            _groundSensor.canDoubleJump = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        }
        else
        {
            _animator.SetBool("IsJumping", true);
        }

        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _audioSource.PlayOneShot(jumpSFX);
    }

    IEnumerator Dash()
    {
        float gravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);

        _isDashing = true;
        _canDash = false;
        rigidBody.AddForce(transform.right * _dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_dashDuration);
        rigidBody.gravityScale = gravity;
        _isDashing = false;

        yield return new WaitForSeconds(_dashCoolDown);
        _canDash = true;
    }

    void NormalAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_hitBoxPosition.position, _attackRadius, _enemyLayer); //como el OnTriggerEnter, con un if que compruba si lo que entra esta en la capa de Enemy
        

        foreach(Collider2D enemy in enemies)
        {
            Mushroom enemyScript = enemy.GetComponent<Mushroom>();
            enemyScript.TakeDamage(_attackDamage);
        }
    }


    void AttackCharge()
    {
        if(_chargedAttackDamage < _maxChargedAttackDamage)
        {
            _chargedAttackDamage += Time.deltaTime; 
            Debug.Log(_chargedAttackDamage);
        }
        else
        {
            _chargedAttackDamage = _maxChargedAttackDamage;
        }
        
    }

    void ChargedAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_hitBoxPosition.position, _attackRadius, _enemyLayer);

        foreach(Collider2D enemy in enemies)
        {
            Mushroom enemyScript = enemy.GetComponent<Mushroom>();
            enemyScript.TakeDamage(_chargedAttackDamage);
        }

        _chargedAttackDamage = _baseChargedAttackDamage;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_hitBoxPosition.position, _attackRadius);
    }
}