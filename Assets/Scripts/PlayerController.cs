using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float speed = 7;

    [Header("Audio & Effects")]
    public AudioClip collectSound;
    private AudioSource playerAudio;
    public GameObject starsEffect;

    [Header("Jumping")]
    private float jumpForce = 5f;
    private bool ground = true;

    [Header("Shield")]
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;
    public GameObject bonusShield;

    private GameManager gameManager; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;

        bonusShield.SetActive(false);
        playerAudio = GetComponent<AudioSource>();

        gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer < 0f)
            {
                isInvulnerable = false;
                bonusShield.SetActive(false);
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && ground)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ground = false;
        }

        if (transform.position.y < -1f)
        {
            Die();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            other.gameObject.SetActive(false);
            Instantiate(starsEffect, other.transform.position, Quaternion.identity);

            playerAudio.PlayOneShot(collectSound);

            gameManager.AddGem();
        }

        if (other.gameObject.CompareTag("BonusGem"))
        {
            other.gameObject.SetActive(false);
            Instantiate(starsEffect, other.transform.position, Quaternion.identity);
            isInvulnerable = true;
            invulnerabilityTimer = 5f;

            bonusShield.SetActive(true);
            playerAudio.PlayOneShot(collectSound);
        }
    }

    public void UpdateSpeed(float newValue)
    {
        speed = newValue;
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemyCollision(collision);

        if (collision.gameObject.CompareTag("Ground"))
        {
            ground = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        EnemyCollision(collision);
    }

    void EnemyCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isInvulnerable)
            {
                Die();
            }
        }
    }

    void Die()
    {
        gameManager.TriggerGameOver();
        Destroy(gameObject);
    }
}