using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public LifeManager LifeManager;
    public CharacterController CheckPoint;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LifeManager.currentLife--;
            LifeManager.UpdateUI();
            //go back to last saved point
           
        }
    }
}
