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
    public SpriteRenderer gunSprite;
    public GameObject blueBullet;
    public GameObject orangeBullet;
    public Transform shootPoint;
    public Animator gunAnim;
    private Inventory inventory;
    public float portalSpawnDist = 1.5f;
    public float bulletSpeed;
    public float fireRate;
    float readyForNextShot; 
    public bool isPortalGun = true;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (inventory.isActive)
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

        gunSprite.flipY = direction.x < 0;     
        
    }
    void ShootBullet(bool isBlue)
    {
        if (isPortalGun)
        {
            gunAnim.SetTrigger("shoot");
            GameObject prefab = isBlue ? blueBullet : orangeBullet;
            GameObject bulletIns = Instantiate(prefab, shootPoint.position, shootPoint.rotation);
            bulletIns.GetComponent<PortalBullet>().isBlue = isBlue;
            bulletIns.GetComponent<Rigidbody2D>().AddForce(bulletIns.transform.right * bulletSpeed);
        }
    }
}
