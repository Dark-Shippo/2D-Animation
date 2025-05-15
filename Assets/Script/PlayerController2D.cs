using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Animator animator;

    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform grounded;
    [SerializeField] private float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal == 1f && isGrounded)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isRunning", true);
        }
        if (horizontal == -1f && isGrounded)
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isRunning", true);
        }
        if (horizontal == 0f)
        {
            animator.SetBool("isRunning", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }

        if (!isGrounded && rb.linearVelocityY <= 0.0f)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            RaycastHit2D hitInfo = Physics2D.Raycast(grounded.position, Vector2.down, .5f);
            if (hitInfo.collider != null)
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    isGrounded = true;
                    animator.SetBool("isFalling", false);
                }
            }
        }
    }
}
