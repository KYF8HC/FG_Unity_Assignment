using System;
using System.Collections.Generic;
using KarioMart.UI;
using UnityEngine;

namespace KarioMart.Core
{
    public class CheckPointTracker : MonoBehaviour
    {
        public delegate void CheckPointEventHandler(CheckPoint checkPoint);

        public event CheckPointEventHandler OnCarPassThroughCheckPoint;
        private InGameHUD inGameHUD;
        private List<CheckPoint> checkPoints;
        private int maxLapCount = 4;
        private int nextCheckPointIndex;

        private void Awake()
        {
            checkPoints = new List<CheckPoint>();
            foreach (Transform checkPoint in transform)
            {
                var checkPointInstance = checkPoint.GetComponent<CheckPoint>();
                checkPointInstance.SetCheckPointTracker(this);
                checkPoints.Add(checkPointInstance);
            }
        }

        public void CarThroughCheckPoint(CheckPoint checkPoint)
        {
            var index = checkPoints.IndexOf(checkPoint);
            if (index != nextCheckPointIndex)
                return;
            if (index == checkPoints.Count - 1)
            {
                nextCheckPointIndex = 0;
                return;
            }

            if (index == 0)
                inGameHUD.IncrementLapCount(maxLapCount);

            OnCarPassThroughCheckPoint?.Invoke(checkPoint);
            nextCheckPointIndex++;
        }

        public void SetInGameHUD(InGameHUD inGameHUD)
        {
            this.inGameHUD = inGameHUD;
        }
    }
}