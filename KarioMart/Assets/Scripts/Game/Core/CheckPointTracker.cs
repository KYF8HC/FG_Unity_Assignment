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
        private List<Transform> carTransformsList;
        private List<int> nextCheckPointIndexList;

        private void Awake()
        {
            carTransformsList = new List<Transform>();
        }

        public void CarThroughCheckPoint(CheckPoint checkPoint, Transform carTransform)
        {
            var nextCheckPointIndex = nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)];
            var index = checkPoints.IndexOf(checkPoint);
            if (index != nextCheckPointIndex)
            {
                return;
            }

            if (index == checkPoints.Count - 1)
            {
                nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)] = 0;
                return;
            }

            if (nextCheckPointIndexList[0] == 0 && carTransform == carTransformsList[0])
            {
                lapCount++;
                inGameHUD.UpdateLapCount(lapCount, maxLapCount);
            }

            if (lapCount == maxLapCount + 1)
                GameStateManager.Instance.GameOver();
            OnCarPassThroughCheckPoint?.Invoke(checkPoint);
            nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)]++;
        }

        public Transform GetCheckPointTransform(Transform carTransform)
        {
            var nextCheckPointIndex = nextCheckPointIndexList[carTransformsList.IndexOf(carTransform)];
            return checkPoints[nextCheckPointIndex].transform;
        }

        public void Initialize()
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
            Debug.Log(carTransformsList.Count + ", " + nextCheckPointIndexList.Count);
        }
        
        public void ResetLapCount()
        {
            lapCount = 0;
        }

        public void SetInGameHUD(InGameHUD inGameHUD)
        {
            this.inGameHUD = inGameHUD;
        }

        public void AddElementsToCarTrasnformList(Transform carTransform)
        {
            carTransformsList.Add(carTransform);
        }
    }
}