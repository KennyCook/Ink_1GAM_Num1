using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform SphereTransform;
    private Vector3 _cameraPos;

    void Start()
    {
        SphereTransform = GameObject.FindGameObjectWithTag("InkBall").transform;
    }

    void Update()
    {
        _cameraPos.x = SphereTransform.position.x;
        _cameraPos.y = SphereTransform.position.y + SphereTransform.localScale.y;
        _cameraPos.z = SphereTransform.position.z - (SphereTransform.localScale.z + 5);
        transform.position = _cameraPos;
    }
}
