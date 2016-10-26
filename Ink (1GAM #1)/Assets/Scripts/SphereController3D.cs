using UnityEngine;

public class SphereController3D : MonoBehaviour
{
    public float RollSpeedModifier, FastRollSpeedModifier;

    public Camera MainCam;
    public Vector3 GrowthRate;

    private Material _ballMaterial;
    private Rigidbody _ballRB;

    private Color _inkSpotColor;
    public Color InkSpotColor { get { return _inkSpotColor;  } set { _inkSpotColor = value; } }

    private bool _spaceHeld, _inInkSpot;
    public bool InInkSpot { get {return _inInkSpot;} set{_inInkSpot = value;} }

    void Awake()
    {
        _ballMaterial = GetComponent<MeshRenderer>().materials[0];
        _ballRB = GetComponent<Rigidbody>();
        _spaceHeld = _inInkSpot = false;
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.y <= 1f)
        {
            float forwardSpeed = Input.GetAxis("Vertical");
            float sideSpeed = Input.GetAxis("Horizontal");
            Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
            Roll(speed, forwardSpeed);
        }

        if (_inInkSpot)
        {
            Debug.Log("In ink spot");       // DEBUG
            AbsorbInk();
        }
        // DEBUG
        else
        {
            Debug.Log("Not in ink spot");
        }
    }

    private void Roll(Vector3 direction, float turn)
    {
        Debug.Log(_ballRB.velocity);
        if (Input.GetKey(KeyCode.Space))
        {
            _spaceHeld = true;
            _ballRB.AddForce(direction * FastRollSpeedModifier, ForceMode.Acceleration);
        }
        else
        {
            _spaceHeld = false;
            _ballRB.AddForce(direction * RollSpeedModifier, ForceMode.Acceleration);
        }
    }

    private void AbsorbInk()
    {
        // Growth rate should be slowed down as ball gets larger
        transform.localScale += GrowthRate;
        _ballMaterial.color = Color.Lerp(_ballMaterial.color, _inkSpotColor, Time.deltaTime);
    }
}

// TODO: increase/decrease drag with size (smaller more, counterintuitive)
