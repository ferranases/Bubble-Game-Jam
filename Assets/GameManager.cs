using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RestarScene();
        }
    }


    public void StartGame()
    {
        PlayerController.instance.Activate();
        CameraController.instance.Activate();
    }

    public void Die()
    {
        CameraController.instance.Stop();
    }

    public void RestarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
