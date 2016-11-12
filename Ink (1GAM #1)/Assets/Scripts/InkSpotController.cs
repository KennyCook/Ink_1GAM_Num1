using UnityEngine;

public class InkSpotController : MonoBehaviour
{
    private Vector3 _shrinkRate;
    private bool _shrinking;
    private float _minScale;


    void Start ()
    {
        _shrinkRate = new Vector3(transform.localScale.x * 0.01f, transform.localScale.y * 0.01f, 0);
        _shrinking = false;
        _minScale = 0;
    }
	
	void Update ()
    {
        if (transform.localScale.x <= _minScale)
        {
            gameObject.SetActive(false);
        }

        if (_shrinking && transform.localScale.x > _minScale)
        { 
            transform.localScale -= _shrinkRate;
        }
    }

    void OnTriggerEnter(Collider ballCollider)
    {
        if (ballCollider.gameObject.layer == 8)
        {
            _shrinking = true;
            ballCollider.gameObject.GetComponent<SphereController3D>().InInkSpot = true;
            ballCollider.gameObject.GetComponent<SphereController3D>().InkSpotColor = GetComponent<SpriteRenderer>().color;
        }
    }

    void OnTriggerExit(Collider ballCollider)
    {
        if (ballCollider.gameObject.layer == 8)
        {
            _shrinking = false;
            ballCollider.gameObject.GetComponent<SphereController3D>().InInkSpot = false;
        }
    }
}
