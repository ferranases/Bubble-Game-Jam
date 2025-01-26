using UnityEngine;

public class BatteryBox : MonoBehaviour
{
    public GameObject parentBatteries;

    public Fan[] fans;

    int currentBatteries = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < parentBatteries.transform.childCount; i++)
        {
            parentBatteries.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public bool CanPlaceBattery()
    {
        return currentBatteries < 2;
    }

    public void PlaceBattery()
    {
        parentBatteries.transform.GetChild(currentBatteries).gameObject.SetActive(true);
        currentBatteries++;

        if (currentBatteries == 2)
        {
            for (int i = 0; i < fans.Length; i++)
            {
                fans[i].Activate();
            }
        }
    }
}
