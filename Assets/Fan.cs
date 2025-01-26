using UnityEngine;

public class Fan : MonoBehaviour
{
    public Transform rotator;
    public GameObject pusher;
    public float speedRotate;

    bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pusher.SetActive(false);
    }

    public void Activate()
    {
        active = true;
        pusher.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
        rotator.Rotate(Vector3.forward * Time.deltaTime * speedRotate);
    }
}
