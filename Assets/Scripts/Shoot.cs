using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform gun;

    Vector2 direction;
    public Vector2 bulletDirection;
    public Vector2 bulletPos;

    public GameObject bluePortal;
    public GameObject orangePortal;
    
    public GameObject blueBullet;
    public GameObject orangeBullet;
    public Transform shootPoint;
    public Animator gunAnim;
    Player player;
    private SpriteRenderer gunSprite;
    private ItemPickup pickup;
    private Inventory inventory;
    public float portalSpawnDist = 1.5f;
    public float bulletSpeed;
    public float fireRate;
    float readyForNextShot; 

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        player = GetComponent<Player>();
        
    }

    void Update()
    {
        
        if (inventory.isActive)
        {
            return;
        }
        if (!player.equipped)
        {
            
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)gun.position;

        FaceMouse();

        bool left = Input.GetMouseButtonDown(0);
        bool right = Input.GetMouseButtonDown(1);

        if (left || right)
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / fireRate;
                ShootBullet(left);
            }
        }
        
    }

    void FaceMouse()
    {
        Vector3 up = Vector3.Cross(Vector3.forward, direction);
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, up);
        gun.transform.rotation = rot;
        //gunSprite.transform.rotation = rot;
        //gunSprite.flipY = direction.x < 0;     
        
    }
    void ShootBullet(bool isBlue)
    {
        if (inventory.itemIds[inventory.itemHeld] == 1)
        {
            gunAnim.SetTrigger("shoot");
            GameObject prefab = isBlue ? blueBullet : orangeBullet;
            GameObject bulletIns = Instantiate(prefab, shootPoint.position, shootPoint.rotation);
            bulletIns.GetComponent<PortalBullet>().isBlue = isBlue;
            bulletIns.GetComponent<Rigidbody2D>().AddForce(bulletIns.transform.right * bulletSpeed);
        }
        else if (inventory.itemIds[inventory.itemHeld] == 2)
        {
            gunAnim.SetTrigger("shoot");
            player.rb.velocity = new Vector2(player.rb.velocity.x, 10f);
        }

    }
}
