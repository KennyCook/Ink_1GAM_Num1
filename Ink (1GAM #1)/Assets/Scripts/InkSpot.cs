using System;
using UnityEngine;

public class InkSpot : MonoBehaviour
{
    public float MinScaleX;         // Make private once settled on a number
    public Vector3 ShrinkRate;      // Make private once settled on a rate

    private bool _shrinking;
    private float _shrinkModifier;

	void Start ()
    {
        _shrinking = false;
        _shrinkModifier = 1f;
    }
	
	void Update ()
    {
        if (transform.localScale.x <= MinScaleX)
        {
            // Debug.Log("Kill me.");
            // Destroy(gameObject); OR ShrinkRate = Vector3.zero;
        }

        if (_shrinking && transform.localScale.x > MinScaleX)
        {
            _shrinkModifier += 0.2f;
            transform.localScale -= ShrinkRate * _shrinkModifier;
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
        //_shrinking = true;
        //try
        //{
        //    ballCollider.gameObject.GetComponent<SphereController3D>().InInkSpot = true;
        //    ballCollider.gameObject.GetComponent<SphereController3D>().InkSpotColor = GetComponent<SpriteRenderer>().color;
        //}
        //catch (NullReferenceException e)
        //{
        //    Debug.Log(e.StackTrace);
        //}
    }

    void OnTriggerExit(Collider ballCollider)
    {
        //_shrinking = false;
        if (ballCollider.gameObject.layer == 8)
        {
            _shrinking = false;
            ballCollider.gameObject.GetComponent<SphereController3D>().InInkSpot = false;
        }
        //try
        //{
        //    ballCollider.gameObject.GetComponent<SphereController3D>().InInkSpot = false;
        //}
        //catch (NullReferenceException e)
        //{
        //    Debug.Log(e.StackTrace);
        //}
    }
}
