using UnityEngine;
using UnityEngine.InputSystem;

public class FanPusher : MonoBehaviour
{

    public float forcePush;
    public Transform posDesired;
    Rigidbody rbPlayer = null;

    public float maxForce = 100;

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
        Vector3 direction = Vector3.up; // Direction from the fan to the player

        // Apply an exponential or squared force for more impact the closer they are
        float pushStrength = Mathf.Clamp(distance, 0f, 5f); // Clamp the distance to avoid excessive force
        float force = Mathf.Pow(pushStrength, 2) * forcePush; // Exponentially increase the force as the player gets closer

        // Limit the maximum force applied
        force = Mathf.Min(force, maxForce);

        // Apply the force in the direction from the fan to the player
        if (rbPlayer.transform.position.y < posDesired.position.y)
        {
            rbPlayer.AddForce(direction * force * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        //Limit the Pos Y of player
        Vector3 posPlayer = rbPlayer.transform.position;
        posPlayer.y = Mathf.Min(rbPlayer.transform.position.y, posDesired.position.y);
        rbPlayer.transform.position = posPlayer;

        //rbPlayer.AddForce(transform.forward * Time.fixedDeltaTime * forcePush, ForceMode.Acceleration);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (PlayerController.instance.GetMode() != TypeMode.bubble) return;

        PlayerController.instance.ResetBubbleColdown();

    }


}
