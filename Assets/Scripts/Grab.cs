using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Grab : MonoBehaviour
{
    Player player;
    PlayerController controller;
    GameObject objectHeld;
    public GameObject rightDropSpot;
    public GameObject downDropSpot;
    public GameObject holdPos;
    public bool holding = false;
    bool[] isTouchingWall = new bool[5];
    bool touching = false;
    int throwSpeed = 10;
    float cooldown = 0;

    [SerializeField] LayerMask layers;

    void Awake()
    {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {

        if (objectHeld != null && touching && Input.GetMouseButtonDown(0) && !holding && !player.equipped)
        {
            objectHeld.GetComponent<Rigidbody2D>().isKinematic = true;
            objectHeld.GetComponent<Collider2D>().isTrigger = true;
            holding = true;
            if (Time.time > cooldown)
            {
                cooldown = Time.time + .1f;

            }
        }
        if (holding)
        {
            objectHeld.transform.position = holdPos.transform.position;
            objectHeld.transform.parent = transform;          
        }   

        if (objectHeld != null && Input.GetMouseButtonDown(0) && Time.time > cooldown && holding && !player.equipped && !isTouchingWall[4])
        {
            Throw();
        }
        if (objectHeld != null && Input.GetMouseButtonDown(1) && Time.time > cooldown && holding && !player.equipped && !isTouchingWall[4] && !Input.GetKey(KeyCode.S))
        {
            DropSide();
        }
        if (objectHeld != null && Input.GetMouseButtonDown(1) && Time.time > cooldown && holding && !player.equipped && !isTouchingWall[4] && Input.GetKey(KeyCode.S) && !player.onGround)
        {
            DropDown();
        }
            if (objectHeld != null)
        {
            isTouchingWall[0] = Physics2D.Raycast(objectHeld.GetComponent<Collider2D>().bounds.center, Vector2.left, objectHeld.GetComponent<Collider2D>().bounds.extents.x + .1f, layers);
            isTouchingWall[1] = Physics2D.Raycast(objectHeld.GetComponent<Collider2D>().bounds.center, Vector2.right, objectHeld.GetComponent<Collider2D>().bounds.extents.x + .1f, layers);
            isTouchingWall[2] = Physics2D.Raycast(objectHeld.GetComponent<Collider2D>().bounds.center, Vector2.up, objectHeld.GetComponent<Collider2D>().bounds.extents.x + .1f, layers);
            isTouchingWall[3] = Physics2D.Raycast(objectHeld.GetComponent<Collider2D>().bounds.center, Vector2.down, objectHeld.GetComponent<Collider2D>().bounds.extents.x +.1f, layers);
            
        }
        isTouchingWall[4] = isTouchingWall[0] || isTouchingWall[1] || isTouchingWall[2] || isTouchingWall[3];
        
    }

    void OnCollisionStay2D(Collision2D coll)
    {

        if (coll.gameObject.layer == 13 && !holding)
        {
            touching = true;
            objectHeld = coll.gameObject;
        }
        
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 13)
        {
            touching = false;
        }
    }

    void Throw()
    {
        objectHeld.transform.parent = null;
        objectHeld.GetComponent<Rigidbody2D>().isKinematic = false;
        objectHeld.GetComponent<Rigidbody2D>().velocity = controller.facingRight ? new Vector2(throwSpeed, 2) : new Vector2(-throwSpeed, 2);
        objectHeld.GetComponent<Collider2D>().isTrigger = false;
        touching = false;
        holding = false;
    }

    void DropSide()
    {
        objectHeld.transform.parent = null;
        objectHeld.GetComponent<Rigidbody2D>().isKinematic = false;
        objectHeld.GetComponent<Collider2D>().isTrigger = false;
        objectHeld.transform.position = controller.facingRight ? rightDropSpot.transform.position : rightDropSpot.transform.position;
        touching = false;
        holding = false;
    }

    void DropDown()
    {
        objectHeld.transform.parent = null;
        objectHeld.GetComponent<Rigidbody2D>().isKinematic = false;
        objectHeld.GetComponent<Collider2D>().isTrigger = false;
        objectHeld.transform.position = downDropSpot.transform.position;
        touching = false;
        holding = false;
    }

}
