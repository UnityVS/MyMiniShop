using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance;

    private void Awake()
    {
        if (Instance) Destroy(gameObject); else Instance = this;
    }

    public void ShootType()
    {

    }
}