using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    

    void Update()
    {
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Instance.soundControl.PlaySoundEffect("savepoint");
        GameController.Instance.resetController.SetResetPosition(collision.transform.position);
        Destroy(gameObject);
    }

}
