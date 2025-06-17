using UnityEngine;

public class rotationEffect : MonoBehaviour
{
    [Header("Eixo de rotação")]
    [SerializeField] Vector3 dir = Vector3.one;

    [Header("Velocidade de rotação e aceleração")]
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float aceleration = 3;

    [Header("Alterações na rotação")]
    [SerializeField] bool randomize = false;
    [SerializeField] float minSpeed = 5;
    [SerializeField] float minDelay = 2, maxDelay = 5;

    private float speed = 0;
    private float randomizeTimer = 0;
    private float randomizeSpeed;
    private float randomizeDelay;

    void Start()
    {
        randomizeSpeed = minSpeed;
        randomizeDelay = minDelay;
    }
    void Update()
    {
        if (randomize)
        {
            Random();
            Rotation(randomizeSpeed);
        }
        else Rotation(maxSpeed);
    }

    void Random()
    {
        randomizeTimer += Time.deltaTime;
        if (randomizeTimer > randomizeDelay)
        {
            randomizeDelay = UnityEngine.Random.Range(minDelay, maxDelay);
            randomizeSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }
    }
    void Rotation(float targetSpeed)
    {
        if (Mathf.Abs(speed - targetSpeed) > 0.1f)
        {
            float aceleration = targetSpeed > speed? this.aceleration: -this.aceleration;
            speed += (aceleration * Time.deltaTime);
        }

        transform.Rotate(dir.normalized * speed * Time.deltaTime);
    }
}
