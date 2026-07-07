using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private bool isOnGround = true;
    private bool isGameOver;
    private Animator playerAnim;

    [SerializeField] private float jumpForce = 50f;

    public InputAction jumpAction;
    public static event Action OnGameOver;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (jumpAction != null)
        {
            jumpAction.performed += OnJump;
            jumpAction.Enable();
        }
    }

    void OnDisable()
    {
        if (jumpAction != null)
        {
            jumpAction.performed -= OnJump;
            jumpAction.Disable();
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (!isOnGround || isGameOver) return;

        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;

        playerAnim.SetTrigger("Jump_trig");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGameOver)
            return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over!!!");

            OnGameOver?.Invoke();

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }
}
