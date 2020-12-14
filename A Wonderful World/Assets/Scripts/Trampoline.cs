using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] int bounceForce;
    Animator TrampolineAnimator;

    public bool playerOn;

    private void Start()
    {
        TrampolineAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.GetComponent<PlayerMovement>().isJumping = true;
            Player.GetComponent<PlayerMovement>().jumpCount = 1;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.up * bounceForce;
            playerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerOn = false;
    }

    private void FixedUpdate()
    {
        TrampolineAnimator.SetBool("PlayerOn", playerOn);
    }
}
