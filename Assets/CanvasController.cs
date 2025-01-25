using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public GameObject panelStart;

    void Start()
    {
        panelStart.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
