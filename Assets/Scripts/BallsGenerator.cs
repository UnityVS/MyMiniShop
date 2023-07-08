using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EBallType
{
    White,
    Red,
    Green
}

public class BallsGenerator : MonoBehaviour
{
    [SerializeField] private List<CollectableBall> collectableBallsList = new();
    private void Awake()
    {
        int enumLength = Enum.GetValues(typeof(EBallType)).Length;
        BallsController.GetInstance.SetEBallsTypeCount(enumLength);
        for (int i = 0; i < enumLength; i++)
        {
            BallsController.GetInstance.GetHowBallsNeeded[(EBallType)Enum.GetValues(typeof(EBallType)).GetValue(i)] = 0;
            BallsController.GetInstance.GetHowBallsCollected[(EBallType)Enum.GetValues(typeof(EBallType)).GetValue(i)] = 0;
        }
        int countTryes = 0;
        for (int i = 0; i < collectableBallsList.Count; i++)
        {
            while (true)
            {
                countTryes++;
                Vector3 position = transform.TransformPoint(new Vector3(Random.Range(-0.35f, 0.35f), 0, Random.Range(-0.5f, -0.2f)));
                if (!Physics.CheckSphere(position, 0.25f, int.MaxValue, QueryTriggerInteraction.Collide))
                {
                    EBallType ballType = (EBallType)Enum.GetValues(typeof(EBallType)).GetValue(Random.Range(0, enumLength));
                    BallsController.GetInstance.GetHowBallsNeeded[ballType] += 1;
                    collectableBallsList[i].ReleaseBall(ballType);
                    collectableBallsList[i].transform.position = position;
                    collectableBallsList[i].gameObject.SetActive(true);
                    break;
                }
                if (countTryes == 50) break;
            }
        }
    }
}