using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    private const string ESPETO_TAG = "Espeto";

    public static CubeController Instance { private set; get; }

    //EVENTOS
    public static event Action ImDead;
    public static event Action ImWin;


    [Header("Impacto")]
    public AudioSource impactAudioSource;
    public AudioClip impactAudioClip;
    public float impactEffectDelay = 0.3f;
    public float impactShakeStrengh = 0.05f;
    public float minImpactVelocity = 3f;
    private bool canPlayImpact = true;

    public AutoCamera mycamera;

    [Header("Movimento")]
    public float moveSpeed = 5f;
    public float rollTorque = 5f;

    public GameObject skate;

    [Header("Pulo")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Vector2 moveInput = Vector2.zero;
    private bool jumpRequested = false;

    private GameObject lastPlatform;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        mycamera = Camera.main.GetComponent<AutoCamera>();    
    }
    void FixedUpdate()
    {
        if (skate.activeSelf)
        {
            transform.rotation = Quaternion.identity;
            Slide(moveInput);
        }
        else
        {
            Roll(moveInput);
        }
        if (jumpRequested)
        {
            TryJump();
            jumpRequested = false;
        }

        if (transform.position.y < -1)
        {
            ImDead?.Invoke();
        }

    }

    void OnCollisionStay(Collision collision)
    {
        OnCollisionEffect(collision);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(ESPETO_TAG))
        {
            ImDead?.Invoke();            
        }
        OnCollisionEffect(collision);
        if( ((1 << collision.collider.gameObject.layer) & groundLayer) != 0)
        {
            lastPlatform = collision.collider.gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End Point"))
        {
            ImWin?.Invoke();
        }
    }



    void OnCollisionEffect(Collision collision)
    {
        float impactStrength = collision.relativeVelocity.magnitude;

        if (impactStrength >= minImpactVelocity && canPlayImpact)
        {
            float shake = impactStrength * impactShakeStrengh;
            mycamera.Shake(shake, shake);

            impactAudioSource.PlayOneShot(impactAudioClip);
            canPlayImpact = false;

            Invoke(nameof(ResetImpactSound), impactEffectDelay);
        }
    }

    void ResetImpactSound()
    {
        canPlayImpact = true;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpRequested = true;
        }
    }
    public void OnPause(InputValue value)
    {
        GameManager.Instance.Pause();
    }

    public void Resurrect()
    {
        transform.position = (lastPlatform != null? lastPlatform.transform.position: Vector3.zero) + (Vector3.up * 2);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    private void Roll(Vector2 input)
    {
        rb.constraints = RigidbodyConstraints.None;

        if (input.sqrMagnitude < 0.01f)
            return;

        Vector3 torque = new Vector3(moveInput.x, 0, moveInput.y) * rollTorque;
        rb.AddForce(torque, ForceMode.Force);
    }

    private void Slide(Vector2 input)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Vector3 move = new Vector3(input.x, 0f, input.y) * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    private void TryJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }

}