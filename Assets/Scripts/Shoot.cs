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
    private BoxCollider2D boxCollider;
    private bool touchingObjRight = false;
    private bool touchingObjLeft = false;
    private bool touchingObjDown = false;
    private bool touchingObjUp = false;
    public float portalSpawnDist = 1.5f;
    public float bulletSpeed;
    public float fireRate;
    float readyForNextShot;

    [SerializeField] private LayerMask layers;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        player = GetComponent<Player>();
        pickup = GetComponent<ItemPickup>();           
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

        gunSprite = pickup.gunObj.GetComponent<SpriteRenderer>();

        boxCollider = pickup.boxCollider;

        touchingObjRight = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, boxCollider.bounds.extents.x + .02f, layers);
        touchingObjLeft = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, boxCollider.bounds.extents.x + .02f, layers);
        touchingObjDown = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + .08f, layers);
        touchingObjUp = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, boxCollider.bounds.extents.y + .08f, layers);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)gun.position;

        FaceMouse();

        bool left = Input.GetMouseButtonDown(0);
        bool right = Input.GetMouseButtonDown(1);
        bool touchingLayer = touchingObjRight || touchingObjLeft || touchingObjDown || touchingObjUp;

        if ((left || right) && !touchingLayer)
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
        gunSprite.transform.rotation = rot;
        gunSprite.flipY = direction.x < 0;

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
