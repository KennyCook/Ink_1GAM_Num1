using UnityEngine;

public class SphereController3D : MonoBehaviour
{
    public Camera MainCam;
    public Vector3 GrowthRate, ShrinkRate, MaxScale, MinScale; // Make mzx/minscales a float

    public float RollSpeedModifier, FastRollSpeedModifier;

    private Material _ballMaterial;
    private Rigidbody _ballRB;

    private Color _inkSpotColor;
    public Color InkSpotColor { get { return _inkSpotColor;  } set { _inkSpotColor = value; } }

    private bool _inInkSpot;
    public bool InInkSpot { get {return _inInkSpot;} set{_inInkSpot = value;} }
    private bool _destroyMe;
    public bool DestroyMe { get { return _destroyMe; } set { _destroyMe = value; } }

    void Awake()
    {
        _ballMaterial = GetComponent<MeshRenderer>().materials[0];
        _ballRB = GetComponent<Rigidbody>();
        _inInkSpot = false;
    }

    void FixedUpdate()
    {
        if (_destroyMe)
        {
            DestroyInkBall();
        }

        float forwardSpeed = Input.GetAxis("Vertical");
        float sideSpeed = Input.GetAxis("Horizontal");
        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        Roll(speed);

        if (_inInkSpot && transform.localScale.x <= MaxScale.x)
        {
            AbsorbInk();
        }
    }

    private void Roll(Vector3 direction)
    {
        if (Input.GetAxis("Roll Fast") > 0)
        {
            _ballRB.AddForce(direction * FastRollSpeedModifier, ForceMode.Acceleration);
            if (_ballRB.velocity != Vector3.zero)
            {
                transform.localScale -= ShrinkRate * (Mathf.Abs(_ballRB.velocity.x) + Mathf.Abs(_ballRB.velocity.z)) * 0.05f;
            }
        }
        else
        {
            _ballRB.AddForce(direction * RollSpeedModifier, ForceMode.Acceleration);
            // Shrink by regular rate
            if (_ballRB.velocity != Vector3.zero)
            {
                transform.localScale -= ShrinkRate * (Mathf.Abs(_ballRB.velocity.x) + Mathf.Abs(_ballRB.velocity.z)) * 0.01f;
            }
        }
        _destroyMe = (transform.localScale.x <= MinScale.x) ? true : false;
    }

    private void AbsorbInk()
    {
        float growthModifier = 1 / transform.localScale.x;
        transform.localScale += GrowthRate * growthModifier;
        _ballMaterial.color = Color.Lerp(_ballMaterial.color, _inkSpotColor, Time.deltaTime * 2f);
    }

    private void DestroyInkBall()
    {
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver = true;
    }
}
