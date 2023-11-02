using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject victoryMessage;

    void Start()
    {
        victoryMessage.SetActive(false);
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinningMessage()
    {
        victoryMessage.SetActive(true);
    }
}
