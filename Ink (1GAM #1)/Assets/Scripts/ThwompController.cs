using UnityEngine;
using System.Collections;

public class ThwompController : MonoBehaviour
{
    public float DropSpeed;     // random speed?, make private

    private Vector3 _upPosition;
    private Vector3 _downPosition;

    private bool _isLerpingDown, _isLerpingUp;
    private float _randomDelay, _currentDelay, _startTime, _thwompDistance;

    void Start()
    {
        _upPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        _downPosition = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, transform.GetChild(0).position.z);
        Debug.Log(_upPosition);
        Debug.Log(_downPosition);
        _isLerpingDown = _isLerpingUp = false;
        _randomDelay = _currentDelay = Random.Range(0.5f, 1.5f);
        _thwompDistance = Vector3.Distance(_upPosition, _downPosition);
    }
	
	void Update ()
    {
        if (_isLerpingDown)
        {
            float distanceDropped = (Time.time - _startTime) * DropSpeed;
            float journeyFraction = distanceDropped / _thwompDistance;
            transform.position = Vector3.Lerp(_upPosition, _downPosition, journeyFraction);
            if (transform.position == _downPosition)
            {
                _startTime = Time.time;
                _isLerpingUp = true;
                _isLerpingDown = false;
            }
        }
        else if (_isLerpingUp)
        {
            float distanceRisen = (Time.time - _startTime) * DropSpeed;
            float journeyFraction = distanceRisen / _thwompDistance;
            transform.position = Vector3.Lerp(_downPosition, _upPosition, journeyFraction);
            if (transform.position == _upPosition)
            {
                _isLerpingUp = false;
                _currentDelay = _randomDelay;
            }
        }
        else
        {
            _currentDelay -= Time.deltaTime;
            _startTime = Time.time;
            if (_currentDelay <= 0)
            {
                _isLerpingDown = true;
            }
        }
    }

    // Detect collisions with ball and kill?
}
