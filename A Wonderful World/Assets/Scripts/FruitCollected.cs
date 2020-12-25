using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollected : MonoBehaviour
{
    Animator fruitAnimator;
    bool collected;
    Spin spinComponent;


    // Start is called before the first frame update
    void Start()
    {
        collected = false;
        fruitAnimator = GetComponent<Animator>();
        spinComponent = GetComponent<Spin>();
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
            spinComponent.enabled = false;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            collected = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
