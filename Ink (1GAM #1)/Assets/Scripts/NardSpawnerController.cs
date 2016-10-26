using UnityEngine;

public class NardSpawnerController : MonoBehaviour
{
    public GameObject NardPrefab;
    private float _randomDelay, _currentDelay;

    void Start()
    {
        _randomDelay = _currentDelay = Random.Range(2f, 4f);
    }

	void Update ()
    {
        if (_currentDelay <= 0)
        {
            Instantiate(NardPrefab, transform.position, transform.rotation);
            _currentDelay = _randomDelay;
        }
        else
        {
            _currentDelay -= Time.deltaTime;
        }
	}
}
