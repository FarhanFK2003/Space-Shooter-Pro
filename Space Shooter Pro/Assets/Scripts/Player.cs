using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    private float speedMultplier = 2;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1.0f;
    [SerializeField]
    private int lives = 3;
    private SpawnManager spawnManager;

    private bool isTripleShotActive = false;
    private bool isSpeedBoostActive = false;
    private bool isShieldActive = false;

    [SerializeField]
    private GameObject shieldVisualizer;

    [SerializeField]
    private int score;

    [SerializeField]
    private GameObject rightEngine;

    [SerializeField]
    private GameObject leftEngine;

    private UIManager uiManager;

    [SerializeField]
    private AudioClip laserSound;

    private AudioSource audioSource;

    private GameManager gameManager;

    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = Vector3.zero;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

        if (spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null!");
        }

        if (uiManager == null)
        {
            Debug.LogError("UI Manager is Null!");
        }

        if(audioSource == null)
        {
            Debug.LogError("Audio Source is Null!");
        }
        else
        {
            audioSource.clip = laserSound;
        }

        if (gameManager.isCoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne)
        {
            CalculateMovement();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && isPlayerOne)
            {
                FireLaser();
            }
        }

        if(isPlayerTwo)
        {
            CalculatePlayerTwoMovement();
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > canFire && isPlayerTwo)
            {
                FireLaser();
            }

        }
    }

    void CalculateMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontalMovement, verticalMovement, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        canFire = Time.time + fireRate;

        if(isTripleShotActive)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        audioSource.Play();
    }

    void CalculatePlayerTwoMovement()
    {

        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }

        lives--;

        if (lives == 2)
        {
            leftEngine.SetActive(true);
        }
        else if (lives == 1)
        {
            rightEngine.SetActive(true);
        }

        uiManager.UpdateLives(lives);

        if (lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultplier;
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }
}
