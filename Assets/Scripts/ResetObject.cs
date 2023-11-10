using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    

    void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.Instance.soundControl.PlaySoundEffect("savepoint");
        GameController.Instance.resetController.SetResetPosition(collision.otherCollider.transform.position);
        Destroy(gameObject);
    }

}
