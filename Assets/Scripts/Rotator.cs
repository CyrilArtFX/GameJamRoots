using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void Rotate(float degreeAngleChange)
    {
        Vector3 old_rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(old_rotation.x, old_rotation.y + degreeAngleChange, old_rotation.z);
    }
}
