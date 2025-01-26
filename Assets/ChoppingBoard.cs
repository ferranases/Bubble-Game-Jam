using System.Collections;
using UnityEngine;

public class ChoppingBoard : MonoBehaviour
{

    public Transform pointRotation;
    public AudioSource audioSource;
    public AudioClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(rutineAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator rutineAnimation()
    {
        while (true)
        {
            LeanTween.rotateX(pointRotation.gameObject, 62.5f, 1f).setEaseInQuad();
            yield return new WaitForSeconds(1.05f);
            LeanTween.rotateX(pointRotation.gameObject, 0, 0.25f).setEaseInQuad().setOnComplete(() =>
            {
                audioSource.PlayOneShot(clip, 2);

            });
            yield return new WaitForSeconds(1f);
        }
    }
}
