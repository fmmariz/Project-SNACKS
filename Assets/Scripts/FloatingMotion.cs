using UnityEngine;

public class FloatingMotion : MonoBehaviour
{
    public float floatSpeed = 3.0f;
    public float floatAmplitude = 0.1f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
