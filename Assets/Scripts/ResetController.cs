using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetController : MonoBehaviour
{
    private Vector3 _latestSavePoint;

    public void SetResetPosition(Vector3 latestSavePoint)
    {   
        _latestSavePoint = latestSavePoint;
    }

    public Vector3 GetResetPosition()
    {
        return _latestSavePoint;
    }


}
