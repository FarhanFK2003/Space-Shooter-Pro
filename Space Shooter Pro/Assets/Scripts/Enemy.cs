//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    [SerializeField]
//    private float speed = 1.0f;
//    [SerializeField]
//    private GameObject laserPrefab;

//    private Player player;
//    private Animator anim;
//    private AudioSource audioSource;
//    private float fireRate = 3.0f;
//    private float canFire = -1.0f;
//    // Start is called before the first frame update
//    void Start()
//    {
//        player = GameObject.Find("Player").GetComponent<Player>();
//        audioSource = GetComponent<AudioSource>();
//        if (player == null)
//        {
//            Debug.LogError("The Player is Null!");
//        }

//        anim = GetComponent<Animator>();

//        if (anim == null)
//        {
//            Debug.LogError("The Animator is Null!");
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        CalculateMovement();

//        if (Time.time > canFire)
//        {
//            fireRate = Random.Range(3f, 7f);
//            canFire = Time.time + fireRate;
//            GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
//            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

//            for (int i = 0; i < lasers.Length; i++)
//            {
//                lasers[i].AssignEnemyLaser();
//            }

//        }
//    }

//    void CalculateMovement()
//    {
//        transform.Translate(Vector3.down * speed * Time.deltaTime);
//        if (transform.position.y < -5f)
//        {
//            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.tag == "Player")
//        {
//            Player player = other.gameObject.GetComponent<Player>();

//            if (player != null)
//            {
//                player.Damage();
//            }
//            anim.SetTrigger("OnEnemyDeath");
//            speed = 0;
//            audioSource.Play();
//            Destroy(gameObject, 2.8f);
//        }

//        if (other.tag == "Laser")
//        {
//            Destroy(other.gameObject);
//            if (player != null)
//            {
//                player.AddScore(10);
//            }
//            anim.SetTrigger("OnEnemyDeath");
//            speed = 0;
//            audioSource.Play();
//            Destroy(GetComponent<Collider2D>());
//            Destroy(gameObject, 2.8f);
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private GameObject laserPrefab;

    private Player player;
    private Animator anim;
    private AudioSource audioSource;
    private float fireRate = 3.0f;
    private float canFire = -1.0f;
    private bool isAlive = true; // New boolean to check if enemy is alive

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        if (player == null)
        {
            Debug.LogError("The Player is Null!");
        }

        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("The Animator is Null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) // Check if the enemy is alive
        {
            CalculateMovement();

            if (Time.time > canFire)
            {
                fireRate = Random.Range(3f, 7f);
                canFire = Time.time + fireRate;
                GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    lasers[i].AssignEnemyLaser();
                }
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Die(); // Call Die method when colliding with Player
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (player != null)
            {
                player.AddScore(10);
            }
            Die(); // Call Die method when colliding with Laser
        }
    }

    private void Die() // New method to handle enemy death
    {
        isAlive = false; // Set isAlive to false to prevent firing
        anim.SetTrigger("OnEnemyDeath");
        speed = 0;
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 2.8f);
    }
}
