using UnityEngine;

public class Fan : MonoBehaviour
{
    public Transform rotator;
    public float speedRotate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotator.Rotate(Vector3.forward * Time.deltaTime * speedRotate);
    }
}
