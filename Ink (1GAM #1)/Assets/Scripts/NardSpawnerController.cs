using UnityEngine;

public class NardSpawnerController : MonoBehaviour
{
    public GameObject NardPrefab;

    private Color[] _nardColors =
    { Color.black,
        Color.blue,
        Color.cyan,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow };

    private float _randomDelay, _currentDelay;

    void Start()
    {
        _randomDelay = _currentDelay = Random.Range(2f, 4f);
    }

	void Update ()
    {
        if (_currentDelay <= 0)
        {
            GameObject newNard = (GameObject) Instantiate(NardPrefab, transform.position, transform.rotation);
            newNard.GetComponent<MeshRenderer>().materials[0].color = _nardColors[Random.Range(0, _nardColors.Length)];
            _currentDelay = _randomDelay;
        }
        else
        {
            _currentDelay -= Time.deltaTime;
        }
	}
}
