using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InflictDamage()
    {
        GameController.Instance.lifeManager.currentLife--;
        GameController.Instance.lifeManager.UpdateUI();
        GameController.Instance.charController.gameObject.GetComponent<DeathAnim>().PlayDeathAnim();
        //GameController.Instance.charController.Reset();
        GameController.Instance.soundControl.PlaySoundEffect("damage");
    }
}
