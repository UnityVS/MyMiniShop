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
        }
        RaycastHit[] hit = new RaycastHit[1];
        for (int i = 0; i < collectableBallsList.Count; i++)
        {
            while (true)
            {
                Vector3 position = transform.TransformPoint(new(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                if (Physics.SphereCastNonAlloc(position, collectableBallsList[0].transform.localScale.y * 2, transform.up, hit) < 1)
                {
                    EBallType ballType = (EBallType)Enum.GetValues(typeof(EBallType)).GetValue(Random.Range(0, enumLength));
                    BallsController.GetInstance.GetHowBallsNeeded[ballType] += 1;
                    collectableBallsList[i].ReleaseBall(ballType);
                    collectableBallsList[i].transform.position = position;
                    collectableBallsList[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}