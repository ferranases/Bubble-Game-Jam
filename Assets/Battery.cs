using UnityEngine;

public class Battery : MonoBehaviour
{

    public GameObject parent;
    public float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeanTween.moveLocalY(parent, parent.transform.localPosition.y + 0.075f, 1f).setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

    }

}
