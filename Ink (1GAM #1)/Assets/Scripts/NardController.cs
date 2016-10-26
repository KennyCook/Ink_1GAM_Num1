using UnityEngine;
using System.Collections;

public class NardController : MonoBehaviour
{
    public float ExplosionForce; // Random?, make private

    private Rigidbody _nardRB;
    private Vector3 _explosionPoint;

    private float _explosionOffset;

	void Awake ()
    {
        _nardRB = GetComponent<Rigidbody>();
        // Need to subtract 1 or -1 depending on position relative to track 
        // This may need to be a bigger number OR make the explosive force large and apply only once during start
        _explosionOffset = (transform.position.x < 0) ? 1f : -1f;
        _explosionPoint = new Vector3(transform.position.x - _explosionOffset, transform.position.y, transform.position.z);
    }
	
	void Update ()
    {
        _nardRB.AddExplosionForce(ExplosionForce, _explosionPoint, 1f);

        if (transform.position.y < -20f)
        {
            Destroy(gameObject);
        }	
	}
}
