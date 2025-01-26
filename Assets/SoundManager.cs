using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public AudioSource audioSource;
    public AudioClip playSound;
    public AudioClip playLose;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(playSound);
    }

    public void PlayLose()
    {
        audioSource.PlayOneShot(playLose);
    }
}
