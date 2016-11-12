using UnityEngine;
using System.Collections;

public class ThwompController : MonoBehaviour
{
    public float LerpSpeed;     // random speed?, make private

    private Vector3 _upPosition;
    private Vector3 _downPosition;

    private bool _isLerpingDown;
    private float _randomDelay, _currentDelay, _startTime, _lerpDistance, _lerpSpeed;

    void Start()
    {
        _upPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        _downPosition = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, transform.GetChild(0).position.z);
        _isLerpingDown = true;
        _randomDelay = _currentDelay = Random.Range(0.5f, 3f);
        _lerpDistance = Vector3.Distance(_upPosition, _downPosition);
        _lerpSpeed = Random.Range(20f, 60f);
    }
	
	void Update ()
    {
        if (_currentDelay <= 0)
        {
            float distanceLerped = (Time.time - _startTime) * _lerpSpeed;
            float journeyFraction = distanceLerped / _lerpDistance;
            if (_isLerpingDown)
            {
                transform.position = Vector3.Lerp(_upPosition, _downPosition, journeyFraction);
                if (transform.position == _downPosition)
                {
                    _isLerpingDown = false;
                    _currentDelay = _randomDelay;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(_downPosition, _upPosition, journeyFraction);
                if (transform.position == _upPosition)
                {
                    _isLerpingDown = true;
                    _currentDelay = _randomDelay;
                }
            }
        }
        else
        {
            _currentDelay -= Time.deltaTime;
            _startTime = Time.time;
        }
    }
    // Detect collisions with ball and kill?
}
