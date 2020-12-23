using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheckpoint : MonoBehaviour
{
    [SerializeField] ParticleSystem ConfettiBurst;

    Animator EndCheckpointAnimator;
    public bool ReachedByPlayer;

    private void Start()
    {
        ConfettiBurst.Stop();
        ReachedByPlayer = false;
        EndCheckpointAnimator = GetComponent<Animator>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !ReachedByPlayer)
        {
            ConfettiBurst.Play();
            ReachedByPlayer = true;

        }
    }

    private void FixedUpdate()
    {
        EndCheckpointAnimator.SetBool("ReachedByPlayer", ReachedByPlayer);
    }
}
