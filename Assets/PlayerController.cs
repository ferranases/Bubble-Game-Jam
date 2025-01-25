using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem.XR;

public enum TypeMode
{
    iddle,
    jumping,
    bubble
}
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public float speed;

    [Header("Jump")]
    public float jump;

    [Header("Gravity")]
    public float customGravity = 20f;    // Custom gravity force
    public float fallMultiplier = 2.5f;  // Multiplier for falling speed
    public float lowJumpMultiplier = 2f; // Multiplier for releasing jump early



    [Header("Bubble")]
    public float bubbleColdownFirst;
    public float bubbleColdownFall;

    [Space(5)]
    public float bubbleCustomUp = 20f;
    public float bubbleCustomGravity = 20f;    // Custom gravity force
    public float bubbleFallMultiplier = 2.5f;  // Multiplier for falling speed    

    public float sphereRadius = 0.5f; // The radius of the sphere
    public float sphereDistance = 0f; // The distance to check below

    [Header("Other")]
    public Transform pointCheckFloorBelow;
    public Transform pointCheckFloorBehind;


    [Header("Test")]
    public MeshRenderer meshRenderer;
    public Material materialGround;
    public Material materialJump;
    public Material materialBubble;

    //PRIVATE

    TypeMode currentMode = TypeMode.iddle;

    CameraController cameraController;
    Rigidbody rb;
    Coroutine coroutineBubble = null;

    bool isGrounded = true;
    bool bubbleActivated = false;
    bool youJumped = false;

    //Gravity
    float currentCustomGravity = 20f;
    float currentFallMultiplier = 2.5f;
    float currentLowJumpMultiplier = 2f;

    LayerMask layerMask;

    Vector3 moveDirection;

    bool canCheckFloor = true;

    void Start()
    {
        cameraController = CameraController.instance;
        rb = GetComponent<Rigidbody>();

        layerMask = ~LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bool checkGroundBelow = Physics.CheckSphere(
            pointCheckFloorBelow.position + Vector3.down * (sphereDistance / 2),  // Start position
            sphereRadius,
            layerMask,
            QueryTriggerInteraction.Ignore
        );

        if (canCheckFloor)
        {
            if (checkGroundBelow)
            {
                isGrounded = true;
                meshRenderer.material = materialGround;
                bubbleActivated = false;
                youJumped = false;
                currentMode = TypeMode.iddle;
                if (coroutineBubble != null) StopCoroutine(coroutineBubble);
                ResetGravity();
            }
            else
            {
                isGrounded = false;
                meshRenderer.material = materialJump;
            }
        }
        else
        {
            if (!checkGroundBelow)
            {
                canCheckFloor = true;
            }
        }



        if (bubbleActivated)
        {
            meshRenderer.material = materialBubble;
        }

        // isGrounded = Physics.Raycast(transform.position - new Vector3(0, 0, 0.5f), Vector3.down, 1.1f);

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


        if (Input.GetKeyDown(KeyCode.Space) && !bubbleActivated && youJumped)
        {
            ActivateBubble();
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            youJumped = true;
            canCheckFloor = false;
            currentMode = TypeMode.jumping;
        }

        if (bubbleActivated && Input.GetKeyUp(KeyCode.Space))
        {
            if (coroutineBubble != null) StopCoroutine(coroutineBubble);
            ResetGravity();
        }


    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            // If falling, apply stronger gravity
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (currentFallMultiplier - 1) * Time.fixedDeltaTime;
            }
            /* // If jump is released early, apply low jump gravity
             else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
             {
                 rb.linearVelocity += Vector3.up * Physics.gravity.y * (currentLowJumpMultiplier - 1) * Time.fixedDeltaTime;
             }*/
        }

        rb.MovePosition(rb.position + (moveDirection * speed * Time.fixedDeltaTime));
        rb.linearVelocity += Vector3.down * currentCustomGravity * Time.fixedDeltaTime;
    }

    void ActivateBubble()
    {
        bubbleActivated = true;
        currentMode = TypeMode.bubble;

        rb.linearVelocity = Vector3.zero;
        currentLowJumpMultiplier = 0;

        if (coroutineBubble != null) StopCoroutine(coroutineBubble);
        coroutineBubble = StartCoroutine(rutineBubble());
    }

    IEnumerator rutineBubble()
    {
        currentCustomGravity = -bubbleCustomUp;
        currentFallMultiplier = 0;

        yield return new WaitForSeconds(bubbleColdownFirst);

        currentCustomGravity = bubbleCustomGravity;
        currentFallMultiplier = bubbleFallMultiplier;

        yield return new WaitForSeconds(bubbleColdownFall);
        ResetGravity();
    }

    void ResetGravity()
    {
        currentCustomGravity = customGravity;
        currentFallMultiplier = fallMultiplier;
        currentLowJumpMultiplier = lowJumpMultiplier;
    }

    void OnDrawGizmos()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(pointCheckFloorBelow.position + Vector3.down * (sphereDistance / 2), sphereRadius);
    }

    public TypeMode GetMode()
    {
        return currentMode;
    }

    public void SetBubbleGravity()
    {
        if (coroutineBubble != null) StopCoroutine(coroutineBubble);

        currentCustomGravity = bubbleCustomGravity;
        currentFallMultiplier = bubbleFallMultiplier;

        Debug.Log("SetBubbleGravity");
    }

    public void ResetBubbleColdown()
    {
        if (coroutineBubble != null) StopCoroutine(coroutineBubble);
        coroutineBubble = StartCoroutine(rutineBubble());
    }
}
