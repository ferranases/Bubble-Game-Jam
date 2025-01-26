using System.Collections;
using UnityEngine;

public class Stove : MonoBehaviour
{

    public ParticleSystem[] fires;

    public GameObject killer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].Stop();
        }

        killer.SetActive(false);
        StartCoroutine(rutineFires());
    }

    IEnumerator rutineFires()
    {
        while (true)
        {
            for (int i = 0; i < fires.Length; i++)
            {
                fires[i].Play();
            }

            killer.SetActive(true);

            yield return new WaitForSeconds(7);

            for (int i = 0; i < fires.Length; i++)
            {
                fires[i].Stop();
            }
            killer.SetActive(false);
            yield return new WaitForSeconds(7);

        }
    }

}
