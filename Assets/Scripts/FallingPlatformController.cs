using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
