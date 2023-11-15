using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingSpikesController : MonoBehaviour
{
    // Start is called before the first frame update

    private const float _risenPosition = 6.49f;

    private const float _fallenPosition = 2.59f;

    private bool _activated = false;
    float t;

    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (_activated)
        {
            t += 2*Time.deltaTime;
            if (t < 1)
            {
                float value = Mathf.SmoothStep(_risenPosition, _fallenPosition, t);
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
            }
            if(t > 5)
            {
                float value = Mathf.SmoothStep(_fallenPosition,_risenPosition, t-5);
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
            }
        }

    }

    public void Activate()
    {
        if (!_activated)
        {
            _activated = true;
        }
    }

    

   
}
