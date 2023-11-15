using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : ResetListeners
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Vector3 _originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _originalPosition = rb.position;

        GameController.Instance.charController.AddResetListeners(this);
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

    public override void OnReset()
    {
        rb.position = _originalPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
