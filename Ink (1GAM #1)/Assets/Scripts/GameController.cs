using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public GameObject InkBallPrefab, InkSpotPrefab, WallPrefab, ThwompPrefab, NardSpawnerPrefab, PauseMenu, WinningMenu, GameOverMenu;
    public Text WinningMenuTimeText;
    public Sprite[] InkSpotSprites;

    private Vector3 _randomPlacementPosition = Vector3.zero;
    private Vector3 _randomInkSpotPosition = new Vector3(0f, 0.35f, 20f);
    private Color[] _inkSpotColors = 
        { Color.black,
        Color.blue,
        Color.cyan,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow };

    private float _startTime, _currentPauseTime, _totalPauseTime;
    private int _inkSpotListIterator = 0;
    private List<GameObject> _inkSpotList = new List<GameObject>();

    private bool _spawnMoreInkSpots;
    public bool SpawnMoreInkSpots { get { return _spawnMoreInkSpots; } set { _spawnMoreInkSpots = value; } }

    private bool _playerWin;
    public bool PlayerWin { get { return _playerWin; } set { _playerWin = value; } }

    private bool _gameOver;
    public bool GameOver { get { return _gameOver; } set { _gameOver = value; } }

    void Awake()
    {
        Time.timeScale = 1.0f;
        Instantiate(InkBallPrefab, new Vector3(0, 2, 0), Quaternion.identity);
    }

	void Start ()
    {
        _startTime = Time.time;
        RandomizeInkSpots();
        RandomizeWallObstacles();
        RandomizeThwompObstacles();
        RandomizeNardObstacles();
    }
	
	void Update ()
    {
        if (_spawnMoreInkSpots)
        {
            RandomizeInkSpots();
            _spawnMoreInkSpots = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (_playerWin)
        {
            PlayerWins();
        }

        if (_gameOver)
        {
            OnGameOver();
        }
    }

    void RandomizeInkSpots()
    {
        float temp = _randomInkSpotPosition.z + 1000f;
        if (temp > 5020f)
        {
            return;
        }
        while (_randomInkSpotPosition.z <= temp)
        {


            // Spawn InkSpot on the track and add to _inkSpotList
            GameObject newInkSpot = (GameObject)Instantiate(InkSpotPrefab, _randomInkSpotPosition, Quaternion.identity);
            _inkSpotList.Add(newInkSpot);
            // Randomize rotation
            newInkSpot.transform.rotation = Quaternion.Euler(90f, Random.Range(0f, 180f), 0f);
            // Randomize scale
            newInkSpot.transform.localScale = new Vector3(Random.Range(3f, 6f), Random.Range(3f, 6f), 1f);
            // Randomize sprite
            newInkSpot.GetComponent<SpriteRenderer>().sprite = InkSpotSprites[1];   // Randomize this
            // Randomize color
            newInkSpot.GetComponent<SpriteRenderer>().color = _inkSpotColors[Random.Range(0, _inkSpotColors.Length)];
            _randomInkSpotPosition.x = Random.Range(-22f, 22f);
            _randomInkSpotPosition.z += Random.Range(20f, 40f);
        }
        _randomInkSpotPosition.z = temp;
    }

    void RandomizeWallObstacles()
    {
        _randomPlacementPosition = new Vector3(0f, 5f, 100f);
        while (_randomPlacementPosition.z < 1500f)
        {
            _randomPlacementPosition.x = Random.Range(-60f, 60f);

            Instantiate(WallPrefab, _randomPlacementPosition, Quaternion.identity);
            if (_randomPlacementPosition.x <= 0f)
            {
                // Spawn second wall on right
                _randomPlacementPosition.x += 50f;
                Instantiate(WallPrefab, _randomPlacementPosition, Quaternion.identity);
            }
            else
            {
                // Spawn second wall on left
                _randomPlacementPosition.x -= 50f;
                Instantiate(WallPrefab, _randomPlacementPosition, Quaternion.identity);
            }

            _randomPlacementPosition.z += 150f;
        }
    }

    void RandomizeThwompObstacles()
    {
        _randomPlacementPosition = new Vector3(0f, 30f, 1500f);
        while (_randomPlacementPosition.z < 2950f)
        {
            _randomPlacementPosition.x = Random.Range(-25f, 25f);
            Instantiate(ThwompPrefab, _randomPlacementPosition, Quaternion.identity);
            _randomPlacementPosition.z += Random.Range(20f, 40f);
        } 
    }

    void RandomizeNardObstacles()
    {
        _randomPlacementPosition = new Vector3(0f, 10f, 3000f);
        while (_randomPlacementPosition.z <= 4400f)
        {
            _randomPlacementPosition.x = (Random.Range(-1f, 1f) <= 0) ? -50f : 50f;
            Instantiate(NardSpawnerPrefab, _randomPlacementPosition, Quaternion.identity);
            _randomPlacementPosition.z += 50f;
        }
    }

    public void TriggerInkSpotDestruction(float triggerZPosition)
    {
        Debug.Log(_inkSpotList.Count);
        // iterate through list and set DestoyMe = true for all ink spots in the list with z coordinates < triggerZPosition
        for (int i = _inkSpotListIterator; i < _inkSpotList.Count; i++)
        {
            if (_inkSpotList[i].transform.position.z >= triggerZPosition - 100f)
            {
                _inkSpotListIterator = i;
                return;
            }
            else
            {
                Destroy(_inkSpotList[i]);
            }
        }
    }

    private void PauseGame()
    { 
        PauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        _currentPauseTime = Time.time;
        GetComponent<AudioSource>().Pause();
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        _totalPauseTime = Time.time - _currentPauseTime;
        GetComponent<AudioSource>().UnPause();
    }

    private void PlayerWins()
    {
        _playerWin = false;
        GameObject.FindGameObjectWithTag("InkBall").GetComponent<SphereController3D>().gameObject.SetActive(false);
        Camera.main.GetComponent<CameraController>().LockPosition = true;
        WinningMenu.SetActive(true);
        WinningMenuTimeText.text = (Time.time - _startTime - _totalPauseTime).ToString("n2") + " seconds";
    }

    public void OnGameOver()
    {
        _gameOver = false;
        GameOverMenu.SetActive(true);
    }
}