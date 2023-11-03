using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public float rotationSpeed = 60.0f;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}