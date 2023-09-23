using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    Player player;
    Vector3 spawnPos;
    Vector2 direction;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    bool facingLeft = false;
    public Sprite hitSprite;
    Sprite originalSprite;
    
    [Header("Health")]

    int health = 30;
    int minHitSpeed = 7;
    

    [Header("Laser")]
    public GameObject laser;
    public GameObject shootPoint;
    public int laserSpeed = 600;
    public float fireRate = 1;
    float readyForNextShot;
    int range = 5;
    public int damage = 10;

    [Header("Audio")]
    AudioSource source;
    public AudioClip fire;


    [SerializeField] private LayerMask playerLayer;

    void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        spawnPos = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = fire;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        if (spriteRenderer.flipX)
        {
            facingLeft = true;
            shootPoint.transform.localPosition = new Vector2(-shootPoint.transform.localPosition.x, shootPoint.transform.localPosition.y);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        if (gameObject.transform.position.y < -10)
        {
            gameObject.transform.position = spawnPos;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(boxCollider.bounds.center, facingLeft ? Vector2.left : Vector2.right, boxCollider.bounds.extents.x + range);
        if (hits.Length < 2 || hits[1].collider.GetComponent<Player>() == null)
        {
            return;
        } 
        direction = (player.transform.position - transform.position).normalized;
        
        if (Time.time > readyForNextShot)
        {
            readyForNextShot = Time.time + 1 / fireRate;
                
            ShootLaser();
        }        
        range = 15;
     
    }

    void ShootLaser()
    {
        Vector3 up = Vector3.Cross(Vector3.forward, direction);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, up);
        GameObject laserIns = Instantiate(laser, shootPoint.transform.position, rot);
        laserIns.GetComponent<Rigidbody2D>().AddForce(direction * laserSpeed);   
        laserIns.transform.rotation = rot;
        source.Play();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.relativeVelocity.magnitude > minHitSpeed) 
        {
            health -= (int) coll.relativeVelocity.magnitude;
            spriteRenderer.sprite = hitSprite;
            //spriteRenderer.sprite = originalSprite;
            print("OWA");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}

