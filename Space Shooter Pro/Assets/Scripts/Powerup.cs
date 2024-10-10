using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -4.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(clip, transform.position);

            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        Debug.Log("TripleShot");
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("Speed");
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shield");
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }

            }
            Destroy(gameObject);
        }
    }
}
