using UnityEngine;

public class Martelao : MonoBehaviour
{   
    [SerializeField][Range(5,100)] 
    private float speed = 10;

    private Rigidbody rb;
    private Vector3 eulerRotation;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        eulerRotation = rb.rotation.eulerAngles;
    }
    private void FixedUpdate()
    {
        eulerRotation += Vector3.left * speed * Time.fixedDeltaTime;
        rb.MoveRotation(Quaternion.Euler(eulerRotation));
    }
}