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
        private int lapCount = 0;
        private int maxLapCount = 4;
        [SerializeField] private List<Transform> carTransformsList;
        private List<int> nextCheckPointIndexList;

        private void Awake()
        {
            checkPoints = new List<CheckPoint>();
            foreach (Transform checkPoint in transform)
            {
                var checkPointInstance = checkPoint.GetComponent<CheckPoint>();
                checkPointInstance.SetCheckPointTracker(this);
                checkPoints.Add(checkPointInstance);
            }

            nextCheckPointIndexList = new List<int>();
            foreach (var carTransform in carTransformsList)
            {
                nextCheckPointIndexList.Add(0);
            }
        }

        public void CarThroughCheckPoint(CheckPoint checkPoint, Transform carTransform)
        {
            int nextCheckPointIndex = nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)];
            Debug.Log(nextCheckPointIndex);
            var index = checkPoints.IndexOf(checkPoint);
            if (index != nextCheckPointIndex)
            {
                Debug.Log("asd");
                return;
            }

            if (index == checkPoints.Count - 1)
            {
                nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)] = 0;
                return;
            }

            if (index == 0)
            {
                lapCount++;
                //inGameHUD.UpdateLapCount(lapCount, maxLapCount);
            }

            if (lapCount == maxLapCount + 1)
                GameStateManager.Instance.GameOver();
            Debug.Log("Car passed through check point " + checkPoint.name);
            OnCarPassThroughCheckPoint?.Invoke(checkPoint);
            nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)]++;
        }

        public Transform GetCheckPointTransform(Transform carTransform)
        {
            var nextCheckPointIndex = nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)];
            return checkPoints[nextCheckPointIndex].transform;
        }

        public void ResetLapCount()
        {
            lapCount = 0;
        }

        public void SetInGameHUD(InGameHUD inGameHUD)
        {
            this.inGameHUD = inGameHUD;
        }
    }
}