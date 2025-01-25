using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public GameObject panelStart;
    public PanelWin panelWin;

    void Start()
    {
        panelStart.SetActive(true);
        panelWin.gameObject.SetActive(false);

    }

    public void ShowPanelWin()
    {
        panelWin.Activate();
    }

}
