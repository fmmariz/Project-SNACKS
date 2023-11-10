using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryMessage;

    [SerializeField]
    private GameObject resetButton;
    private GameObject _resetIcon;
    private Slider _resetSlider;

    void Start()
    {
        victoryMessage.SetActive(false);

        _resetIcon = resetButton.transform.GetChild(0).gameObject;
        _resetSlider = resetButton.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinningMessage()
    {
        victoryMessage.SetActive(true);
    }

    public void UpdateResetTimer(float pct)
    {
        _resetSlider.value = pct;
        if(pct <= 0)
        {
            _resetIcon.GetComponent<RawImage>().color = new Color(255,255,255,100);
        }
        else
        {
            _resetIcon.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        }

    }
}
