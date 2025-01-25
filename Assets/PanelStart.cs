using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelStart : MonoBehaviour
{
    public GameObject buttonPlay;
    public GameObject buttonSettings;
    public GameObject settingsText;

    public TMP_InputField inputFieldName;



    private void Start()
    {
        settingsText.gameObject.SetActive(false);
    }
    public void ButtonPlay()
    {
        LeanTween.scale(buttonPlay, Vector3.one * 0.9f, 0.05f).setLoopPingPong(1).setOnComplete(() =>
        {
            StartGame();
        });
    }

    public void ButtonSettings()
    {
        LeanTween.scale(buttonSettings, Vector3.one * 0.9f, 0.05f).setLoopPingPong(1).setOnComplete(() =>
        {
            buttonSettings.gameObject.SetActive(false);
            settingsText.transform.localScale = Vector3.zero;

            settingsText.gameObject.SetActive(true);
            LeanTween.scale(settingsText, Vector3.one, 0.05f);
        });
    }

    void StartGame()
    {
        GameManager.instance.StartGame();
        gameObject.SetActive(false);


    }
}
