using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatform : MonoBehaviour
{
    [Header("Pontos de movimento")]
    public Transform pointA;
    public Transform pointB;

    [Header("Velocidade")]
    public float maxSpeed = 3f;
    public float acceleration = 5f;
    public float slowDownDistance = 2f;
    public float stopThreshold = 0.05f;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private bool goingToB = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // obrigat�rio para MovePosition funcionar bem
        targetPosition = pointB.position;
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 toTarget = targetPosition - currentPosition;
        float distance = toTarget.magnitude;

        if (distance <= stopThreshold)
        {
            // Trocar dire��o
            goingToB = !goingToB;
            targetPosition = goingToB ? pointB.position : pointA.position;
            return;
        }

        // Dire��o normalizada
        Vector3 direction = toTarget.normalized;

        // Determina velocidade-alvo com desacelera��o suave
        float targetSpeed = maxSpeed;
        if (distance < slowDownDistance)
        {
            targetSpeed = Mathf.Lerp(0f, maxSpeed, distance / slowDownDistance);
        }

        // Acelera��o suave
        float currentSpeed = velocity.magnitude;
        float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        velocity = direction * newSpeed;

        // Movimento via Rigidbody
        rb.MovePosition(currentPosition + velocity * Time.fixedDeltaTime);
    }
}
