using UnityEngine;

public class CollectableBall : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRendererComponent;
    private Color _ballColor;
    private EBallType _currentBallType;

    private void OnEnable()
    {
        meshRendererComponent.material.color = _ballColor;
    }

    public void ReleaseBall(EBallType ballTypeValue)
    {
        switch (ballTypeValue)
        {
            case EBallType.White:
                _ballColor = Color.white;
                break;
            case EBallType.Red:
                _ballColor = Color.red;
                break;
            case EBallType.Green:
                _ballColor = Color.green;
                break;
        }
        _currentBallType = ballTypeValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CollectableBall ball))
        {
            BallsController.GetInstance.GetHowBallsCollected[_currentBallType] += 1;
            
            Destroy(gameObject);
        }
    }
}