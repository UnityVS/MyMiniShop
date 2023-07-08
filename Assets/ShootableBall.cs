using UnityEngine;

public class ShootableBall : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodyComponent;
    public Rigidbody GetRigidbody => rigidbodyComponent;
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
