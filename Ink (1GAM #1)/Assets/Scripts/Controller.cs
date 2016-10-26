using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public float MaxRollSpeed, MaxFastRollSpeed, RollAccRate, FastRollAccRate;    // should be a private const
    public Vector3 BallScaleRate;

    private Rigidbody _ballRB;
    private bool _spaceHeld;

	// Use this for initialization
	void Start ()
    {
        _ballRB = this.GetComponent<Rigidbody>();	
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Roll(FastRollAccRate, MaxFastRollSpeed, _spaceHeld = true);
        }
        else
        {
            Roll(RollAccRate, MaxRollSpeed, _spaceHeld = false);
        }
	}

    void Roll(float accelerationRate, float rollSpeed, bool sprinting)
    { 
        if (sprinting)
        {
            transform.localScale += BallScaleRate;
        }
        if (Input.GetAxis("Horizontal") > 0f)
        {
            _ballRB.AddForce(new Vector3(rollSpeed, 0, 0), ForceMode.Force);
        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            _ballRB.AddForce(new Vector3(-rollSpeed, 0, 0), ForceMode.Force);
        }
    }
}

// Rearrange: put Axis detection in Update, detect Space/determine speed in a seperate function
