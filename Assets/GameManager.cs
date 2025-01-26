using System;
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


    double timeStart;
    double timeEnd;


    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestarScene();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    public void StartGame()
    {
        PlayerController.instance.Activate();
        CameraController.instance.Activate();

        timeStart = Time.time;
    }

    public void Win()
    {
        PlayerController.instance.Stop();
        CameraController.instance.Stop();

        timeEnd = Time.time;

        Debug.Log("Your Time: " + TimeDifferenceInMinutesAndSeconds(timeStart, timeEnd));
        CanvasController.instance.ShowPanelWin();
    }

    public void Die()
    {
        CameraController.instance.Stop();
        SoundManager.instance.PlayLose();
    }

    public void RestarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    static string TimeDifferenceInMinutesAndSeconds(double timeStart, double timeEnd)
    {
        // Calculate the absolute difference in seconds
        double differenceInSeconds = Math.Abs(timeEnd - timeStart);

        // Get whole minutes
        int minutes = (int)(differenceInSeconds / 60);

        // Get remaining seconds (up to 60)
        int seconds = (int)(differenceInSeconds % 60);

        // Return formatted time in "minutes:seconds"
        return $"{minutes}:{seconds:D2}";  // :D2 ensures seconds are always two digits
    }

    public string GetYourTime()
    {
        return TimeDifferenceInMinutesAndSeconds(timeStart, timeEnd);
    }


}
