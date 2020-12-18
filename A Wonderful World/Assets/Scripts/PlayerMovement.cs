using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D PlayerRB;
    SpriteRenderer PlayerSprite;
    Animator PlayerAnimator;
    [SerializeField] ParticleSystem particlesDust;

    public float speed;
    public LayerMask layerGround;
    public bool isJumping;
    bool isGrounded;
    float maxGravityScale;   
    public int jumpCount;
    int jumpForce;
    bool onWall;
    RaycastHit2D hit1;
    RaycastHit2D hit2;

    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerAnimator = GetComponent<Animator>();
        maxGravityScale = PlayerRB.gravityScale;
        isJumping = false;
        isGrounded = false;
        jumpCount = 0;
        jumpForce = 10;
        onWall = false;
        particlesDust.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        isGrounded = IsGrounded();
        onWall = IsOnWall();

        

        if (onWall && PlayerRB.velocity.y < -1.99f)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, -2);
            jumpCount = 1;

            if (hit1)
            {
                particlesDust.transform.position = new Vector3(this.transform.position.x + (0.09f * 5), this.transform.position.y - (0.15f * 5), 0);
            } else
            {
                particlesDust.transform.position = new Vector3(this.transform.position.x - (0.1f * 5), this.transform.position.y - (0.15f * 5), 0);
            }


            particlesDust.enableEmission = true;

        }
        else if (particlesDust.isEmitting)
        {
            particlesDust.enableEmission = false;
        }


        if (isGrounded && jumpCount >= 2)
        {
            jumpCount = 0;
        }

        if (isGrounded)
        {
            PlayerRB.gravityScale = 0;
        }
        else if (!isGrounded && PlayerRB.velocity.y < 0)
        {
            PlayerRB.gravityScale = maxGravityScale;
        }
        else
        {
            PlayerRB.gravityScale = maxGravityScale;

        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            PlayerRB.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpCount++;
        }

    }

    private void FixedUpdate()
    {
        PlayerAnimator.SetFloat("VelocityX", Mathf.Abs(PlayerRB.velocity.x));
        PlayerAnimator.SetBool("Jumping", isJumping);
        PlayerAnimator.SetFloat("VelocityY", PlayerRB.velocity.y);
        PlayerAnimator.SetBool("IsGrounded", isGrounded);
        PlayerAnimator.SetInteger("JumpCount", jumpCount);
        PlayerAnimator.SetBool("OnWall", IsOnWall());
    }

    private void Move()
    {
        float x_input = Input.GetAxis("Horizontal");

        if (x_input < 0 && !onWall)
        {
            PlayerSprite.flipX = true;
        }
        else if (x_input > 0 && !onWall) 
        {
            PlayerSprite.flipX = false;
        }

        if (x_input != 0)
        {
            PlayerRB.velocity = new Vector2(x_input * speed, PlayerRB.velocity.y);
        }
        else
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);

        }
        
        if (isGrounded && !isJumping)
        {
            PlayerRB.velocity = new Vector2(x_input * speed, 0);
        }

    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, layerGround);

        if (hit && (jumpCount > 1 || PlayerRB.velocity.y <= 0f))
        {
            isJumping = false;
            jumpCount = 0;
            return true;
        }

        return false;
    }

    private bool IsOnWall()
    {
        LayerMask layerWall = LayerMask.GetMask("Wall");

        hit1 = Physics2D.Raycast(transform.position, Vector2.right, 0.57f, layerWall);
        hit2 = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, layerWall);

        if (hit1 || hit2)
        {
            if (PlayerRB.velocity.y < -1.99f)
            {
                if (hit1)
                {
                    PlayerSprite.flipX = false;
                }
                else
                {
                    PlayerSprite.flipX = true;
                }

            }

            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 0.8f, 0));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + 0.57f, transform.position.y, 0));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - 0.6f, transform.position.y, 0));
    }

    void CopyAndStickParticle()
    {

    }
}
