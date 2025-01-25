using UnityEngine;
using UnityEngine.UI;

public class PanelWin : MonoBehaviour
{

    public Image background;

    public void Activate()
    {
        gameObject.SetActive(true);
        float a = background.color.a;

        Color startColor = background.color;
        startColor.a = 0;
        background.color = startColor;

        LeanTween.value(background.gameObject, 0, a, 0.5f).setOnUpdate((float _val) =>
        {
            startColor.a = _val;
            background.color = startColor;
        });
    }

    public void ButtonRetry()
    {
        GameManager.instance.RestarScene();
    }
}
