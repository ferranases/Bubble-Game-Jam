using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject parentBateries;
    int currentBatteries = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < parentBateries.transform.childCount; i++)
        {
            parentBateries.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery") && currentBatteries < 2)
        {
            Destroy(other.gameObject);
            parentBateries.transform.GetChild(currentBatteries).gameObject.SetActive(true);
            currentBatteries++;
        }

        if (other.CompareTag("BatteryBox") && currentBatteries > 0)
        {
            BatteryBox batteryBox = other.GetComponent<BatteryBox>();
            if (batteryBox.CanPlaceBattery())
            {
                batteryBox.PlaceBattery();
                currentBatteries--;
                parentBateries.transform.GetChild(currentBatteries).gameObject.SetActive(false);

            }
        }
    }
}
