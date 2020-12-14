using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D PlayerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
        }
    }
}
