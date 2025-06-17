using UnityEngine;

public class AutoCamera : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";

    public Transform target;
    public float followSpeed = 5f;

    [Header("Z Axis Settings")]
    public float defaultZ = -10f;
    public float zoomedZ = -8f;
    public float zDistanceThreshold = 2f;
    public float zSmoothSpeed = 5f;

    [Header("Shake Settings")]
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.2f;

    private float shakeTimer = 0f;
    private Vector3 shakeOffset = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(PLAYER_TAG).transform;
    }
    void LateUpdate()
    {
        if (!target) return;

        // Calcular destino suavizado para x/y
        Vector3 targetPosition = new Vector3(
            Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime * followSpeed),
            Mathf.Lerp(transform.position.y, target.position.y + 3, Time.deltaTime * followSpeed),
            transform.position.z // z tratado separadamente
        );

        // Z automático
        float distanceZ = Mathf.Abs(target.position.z - transform.position.z);
        float targetZ = distanceZ < zDistanceThreshold ? defaultZ : zoomedZ;
        float newZ = Mathf.Lerp(transform.position.z, targetZ, Time.deltaTime * zSmoothSpeed);
        targetPosition.z = newZ;

        // Shake ativo
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            shakeOffset = new Vector3(
                Random.Range(-1f, 1f) * shakeMagnitude,
                Random.Range(-1f, 1f) * shakeMagnitude,
                0f
            );
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

        // Aplicar movimento e shake
        transform.position = targetPosition + shakeOffset;
    }
 
    public void Shake()
    {
        shakeTimer = shakeDuration;
    }
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeTimer = duration;
    }
}
