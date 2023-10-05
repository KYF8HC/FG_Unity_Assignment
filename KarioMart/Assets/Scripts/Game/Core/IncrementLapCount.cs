using KarioMart.UI;
using UnityEngine;

namespace KarioMart.Core
{
    public class IncrementLapCount : MonoBehaviour
    {
        private int maxLapCount = 4;
        private InGameHUD hud;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                hud.IncrementLapCount(maxLapCount);
            }
        }

        public void SetInGameHUD(InGameHUD hud)
        {
            this.hud = hud;
        }
    }
}