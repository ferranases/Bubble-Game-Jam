using UnityEngine;

public class FootSteps : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] steps;

    int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayStep()
    {
        audioSource.PlayOneShot(steps[index]);
        index++;
        if (index == steps.Length) index = 0;
    }
}
