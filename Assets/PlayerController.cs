using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public float speed;
    public float jump;
    public float customGravity = 20f;    // Custom gravity force
    public float fallMultiplier = 2.5f;  // Multiplier for falling speed
    public float lowJumpMultiplier = 2f; // Multiplier for releasing jump early

    CameraController cameraController;
    Rigidbody rb;
    bool isGrounded = true;

    Vector3 moveDirection;

    void Start()
    {
        cameraController = CameraController.instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position - new Vector3(0, 0, 0.5f), Vector3.down, 1.1f);

        // Get input for movement
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down

        float horizontalRot = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalRot = Input.GetAxis("Vertical");     // W/S or Up/Down
        // Get the forward and right directions relative to the camera
        Vector3 forward = cameraController.transform.forward;
        Vector3 right = cameraController.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;
        Vector3 lookDirection = (forward * verticalRot + right * horizontalRot).normalized;
        lookDirection.y = 0;
        moveDirection.y = 0;

        if (horizontal == 0 && vertical == 0)
        {
            moveDirection = Vector3.zero;
        }

        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            // If falling, apply stronger gravity
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            // If jump is released early, apply low jump gravity
            else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }

        rb.MovePosition(rb.position + (moveDirection * speed * Time.fixedDeltaTime));
        rb.linearVelocity += Vector3.down * customGravity * Time.fixedDeltaTime;
    }
}
