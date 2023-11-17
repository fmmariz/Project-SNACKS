using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWarningMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title, _subtitle;

    [SerializeField] private AnimationCurve _animCurve;

    private float timeshowing;
    private bool _activated = false;
    private float _y;
    private float t = 0;
    private void Start()
    {
        _y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_activated)
        {
            Debug.Log($"{t}+{_animCurve.Evaluate(t)}");
            transform.position = new Vector3(-1353f +
                _animCurve.Evaluate(t) * (1353f*3f), _y);
            t += Time.deltaTime/timeshowing;
            if (t >= timeshowing)
            {
                _activated = false;
                Destroy(gameObject);    
            }
        }
    }

    public void ShowMessage(string title, string subtitle = "", float time = 3f)
    {
        t = 0;
        _title.text = title;
        _subtitle.text = subtitle;
        timeshowing = time;
        _activated = true;
    }
}
