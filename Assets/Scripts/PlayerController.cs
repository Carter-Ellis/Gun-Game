using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player player;
    private Inventory inventory;
    public Animator anim;
    public Rigidbody2D rb;
    private bool facingRight = true;
    float horizontal = 0;

   void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (inventory.isActive)
        {
            return;
        }
        anim.SetFloat("horizontal", Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetFloat("vertical", rb.velocity.y);
        anim.SetBool("isGrounded", player.onGround);
        horizontal = Input.GetAxisRaw("Horizontal");
        
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}
