using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollected : MonoBehaviour
{
    [SerializeField] Animator fruitAnimator;

    bool collected;


    // Start is called before the first frame update
    void Start()
    {
        collected = false;
        fruitAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        fruitAnimator.SetBool("Collected", collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collected)
        {
            transform.localScale -= new Vector3(2, 2, 0);
            collected = true;
            Destroy(gameObject, fruitAnimator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }
}
