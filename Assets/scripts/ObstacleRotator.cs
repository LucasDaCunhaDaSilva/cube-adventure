using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleRotator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector3 axes = Vector3.one;
    [SerializeField] float maxSpeed = 2;
    [SerializeField] float aceleration = 2;

    Rigidbody rb;

    float rotationSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(rotationSpeed != maxSpeed) rotationSpeed += (rotationSpeed < maxSpeed? aceleration:-aceleration) * Time.fixedDeltaTime;

        Quaternion quat = rb.rotation;
        quat.eulerAngles += axes * rotationSpeed;

        rb.MoveRotation(quat);
    }
}