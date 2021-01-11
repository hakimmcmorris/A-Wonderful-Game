using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] Rigidbody2D PlayerRB;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (PlayerRB.velocity.y < 0)
        {
            PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (PlayerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
