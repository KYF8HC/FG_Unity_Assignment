using KarioMart.Vehicle;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CarController>().ActivateSpeedBoost();
        }
    }
}
