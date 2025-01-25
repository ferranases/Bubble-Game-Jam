using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance = null;

    Rigidbody rb;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public float speed;


    CameraController cameraController;
    //CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        cameraController = CameraController.instance;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 direction = transform.position - cameraController.transform.position;
        Vector3 move = new Vector3(direction.x, 0, direction.z);

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Get the forward and right directions relative to the camera
        Vector3 forward = cameraController.transform.forward;
        Vector3 right = cameraController.transform.right;

        // Project forward and right directions onto the XZ plane (ignore Y axis)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calculate the desired movement direction
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        moveDirection.y = 0;
        // Move the player using the CharacterController
        //characterController.Move(moveDirection * speed * Time.deltaTime);

        rb.MovePosition(rb.position + (moveDirection * speed * Time.deltaTime));


        // Face the movement direction (optional)
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        //characterController.Move(move * Time.deltaTime * speed);
    }
}
