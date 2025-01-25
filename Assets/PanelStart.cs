using UnityEngine;

public class PanelStart : MonoBehaviour
{
    public GameObject buttonPlay;

    public void ButtonPlay()
    {
        LeanTween.scale(buttonPlay, Vector3.one * 0.9f, 0.05f).setLoopPingPong(1).setOnComplete(() =>
        {
            StartGame();
        });
    }

    void StartGame()
    {
        GameManager.instance.StartGame();
        gameObject.SetActive(false);
    }
}
