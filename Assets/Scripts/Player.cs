using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Components")]
    Player player;
    CapsuleCollider2D cc;
    public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    public int jumpForce = 3;
    private Inventory inventory;

    [Header("Collision")]
    public bool onGround = false;

    [Header("Horizontal Movement")]
    public float moveSpeed = 7f;
    Vector2 direction;
    bool isMoving = false;

    void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        inventory = GetComponent<Inventory>();
    }

    void FixedUpdate()
    {
        if (inventory.isActive)
        {
            Move(0);
            return;
        }

        onGround = Physics2D.Raycast(cc.bounds.center, Vector2.down, cc.bounds.extents.y + 0.05f, groundLayer);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Move(direction.x);
        Jump();

        if (!isMoving && onGround && !Input.GetKey(KeyCode.Space))
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
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

}
