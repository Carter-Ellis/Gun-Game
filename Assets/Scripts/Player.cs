using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHealth = 120f;
    public float health = 100f;


    [Header("Components")]
    Player player;
    BoxCollider2D boxCollider;
    public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    public int jumpForce = 3;
    private Inventory _inventory;

    [Header("Collision")]
    public bool onGround = false;
    public bool onGround2 = false;
    public bool onGround3 = false;

    [Header("Horizontal Movement")]
    public float moveSpeed = 7f;
    Vector2 direction;
    bool isMoving = false;

    [Header("Inventory")]
    public bool equipped;
    public Inventory inventory
    {
        get 
        { 
            if (_inventory == null)
            {
                _inventory = GetComponent<Inventory>();
            }
            return _inventory;
        
        }
    }

    void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        equipped = false;
    }

    void FixedUpdate()
    {
        if (inventory.isActive)
        {
            Move(0);
            return;
        }
        onGround3 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(-0.15f, 0, 0), Vector2.down, boxCollider.bounds.extents.y + 0.05f, groundLayer);
        onGround2 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(0.15f, 0, 0), Vector2.down, boxCollider.bounds.extents.y + 0.05f, groundLayer);
        onGround = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.05f, groundLayer);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Move(direction.x);
        Jump();

        if (!isMoving && (onGround || onGround2 || onGround3) && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void Move(float horizontal)
    {
        rb.velocity = new Vector2(moveSpeed * horizontal, rb.velocity.y);
        if (horizontal != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && (onGround || onGround2 || onGround3))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

}
