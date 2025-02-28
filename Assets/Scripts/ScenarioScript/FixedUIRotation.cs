using UnityEngine;

public class FixedUIRotation : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity; // Prevent rotation
    }
}
