using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float lerpTime;


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpTime);
    }

}
