using UnityEngine;
using UnityEngine.InputSystem;

public class FanPusher : MonoBehaviour
{

    public float forcePush;
    public Transform posDesired;
    Rigidbody rbPlayer = null;

    bool isSetGravityNull = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (rbPlayer == null) rbPlayer = other.GetComponent<Rigidbody>();
        isSetGravityNull = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (PlayerController.instance.GetMode() != TypeMode.bubble)
        {
            isSetGravityNull = false;
            return;
        };

        if (!isSetGravityNull)
        {
            isSetGravityNull = true;
            PlayerController.instance.SetBubbleGravity();
        }

        float distance = Vector3.Distance(rbPlayer.transform.position, posDesired.position);
        //Debug.Log("Dsitance: " + distance);
        if (rbPlayer.transform.position.y < posDesired.position.y)
        {
            rbPlayer.AddForce(transform.forward * distance * Time.fixedDeltaTime * forcePush, ForceMode.Acceleration);
        }


        //rbPlayer.AddForce(transform.forward * Time.fixedDeltaTime * forcePush, ForceMode.Acceleration);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (PlayerController.instance.GetMode() != TypeMode.bubble) return;

        PlayerController.instance.ResetBubbleColdown();

    }


}
