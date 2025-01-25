using UnityEngine;

public class Sponge : MonoBehaviour
{

    public float jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Player")) return;

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        Vector3 dir = rb.linearVelocity.normalized;
        rb.AddForce(dir + new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
    }
}
