using System;
using UnityEditor.PackageManager.UI;
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
    private AudioSource playerAudio;

    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    public static event Action OnGameOver;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
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

        isOnGround = false;

        playerAudio.PlayOneShot(jumpSound, 0.35f);

        dirtParticle.Stop();

        playerAnim.SetTrigger("Jump_trig");

        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGameOver)
            return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            OnGameOver?.Invoke();

            playerAudio.PlayOneShot(crashSound, 0.75f);

            dirtParticle.Stop();

            explosionParticle.Play();

            isGameOver = true;

            Debug.Log("Game Over!!!");

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }
}
