using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform SphereTransform;
    private Vector3 _cameraPos;

    private bool _lockPosition;
    public bool LockPosition { get { return _lockPosition;  } set { _lockPosition = value; } }

    void Start()
    {
        SphereTransform = GameObject.FindGameObjectWithTag("InkBall").transform;
    }

    void Update()
    {
        if (SphereTransform.position.y < -1f)
        {
            _lockPosition = true;
        }

        if (_lockPosition)
        {
            return;
        }

        _cameraPos.x = SphereTransform.position.x;
        _cameraPos.y = SphereTransform.position.y + 4;
        _cameraPos.z = SphereTransform.position.z - 10;
        transform.position = _cameraPos;
    }
}
