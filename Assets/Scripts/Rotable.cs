using StarterAssets;
using UnityEngine;

public class Rotable : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 20.0f;

    public void Rotate( float dir )
    {
        float amount = dir * turnSpeed;

        Vector3 old_rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler( old_rotation.x, old_rotation.y + amount, old_rotation.z );
    }
}
