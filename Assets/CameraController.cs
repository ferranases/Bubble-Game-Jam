
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public Transform target; // The player or object the camera orbits around    
    public float rotationSpeed = 100.0f; // Speed of horizontal rotation

    bool active = false;

    float currentX = 0.0f; // Horizontal rotation angle
    float currentY = 0.0f; // Vertical rotation angle

    float distance = 0;
    float startY;

    public void Activate()
    {
        active = true;
        transform.localEulerAngles = Vector3.zero;
    }

    void Start()
    {
        distance = Vector3.Distance(transform.position, target.position);
        startY = transform.position.y;
    }

    void LateUpdate()
    {
        if (!active) return;
        if (target == null) return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        // Update rotation angles
        currentX += mouseX;

        // Calculate rotation and position
        Quaternion rotation = Quaternion.Euler(currentY, currentX - 90, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Vector3 newPos = target.position + rotation * direction;
        newPos.y = transform.position.y;
        transform.position = newPos;


        Vector3 dir = target.position - transform.position;
        Vector3 rot = Quaternion.LookRotation(dir, Vector3.up).eulerAngles;
        rot.x = 0;
        transform.eulerAngles = rot;
        // Make the camera look at the target

        //transform.LookAt(target);
    }

    public void Stop()
    {
        active = false;
    }

}
