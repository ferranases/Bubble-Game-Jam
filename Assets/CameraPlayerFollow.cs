using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    public Transform target;
    public float speed;

    Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = target.position + offset;
        pos.x = transform.position.x;
        pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
