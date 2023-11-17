using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingSpikesController : ResetListeners
{
    // Start is called before the first frame update

    private const float _risenPosition = 6.49f;

    private const float _fallenPosition = 2.59f;

    private bool _activated = false;
    float t;

    void Start()
    {
        t = 0;
        GameController.Instance.charController.AddResetListeners(this);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void OnReset()
    {
        _activated = false;
        transform.localPosition = new Vector3(transform.localPosition.x, _risenPosition, transform.localPosition.z);
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float delayInS = 1f;
        if (_activated)
        {
            t += 2*Time.deltaTime;
            if (t>delayInS && t < 1+delayInS)
            {
                float value = Mathf.SmoothStep(_risenPosition, _fallenPosition, t-delayInS);
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
            }
            if(t > 5+delayInS)
            {
                float value = Mathf.SmoothStep(_fallenPosition,_risenPosition, t-(5+delayInS));
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
