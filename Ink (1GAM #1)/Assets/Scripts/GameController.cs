using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject InkBallPrefab, InkSpotPrefab, WallPrefab, ThwompPrefab, NardSpawnerPrefab;
    public Sprite[] InkSpotSprites;

    private Vector3 _randomPlacementPosition;
    private Color[] _inkSpotColors = 
        { Color.black,
        Color.blue,
        Color.cyan,
        Color.green,
        Color.magenta,
        Color.red,
        Color.yellow };

    private bool _isSpawningInkSpots;

    void Awake()
    {
        // Move this to a method
        Instantiate(InkBallPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        _randomPlacementPosition = new Vector3(0f, 0.3f, 5f);
        _isSpawningInkSpots = false;
    }

	void Start ()
    {
        // Spawn InkSpots
        while(_randomPlacementPosition.z <= 4000f)
        {
            _isSpawningInkSpots = true;
            // Modify _inkPlacementPosition
            _randomPlacementPosition.x += Random.Range(-25f, 25f);
            _randomPlacementPosition.z += Random.Range(20f, 40f);
            // Call spawning function
            RandomizeInkSpots();
        }
        _isSpawningInkSpots = false;

        // Spawn Wall Obstacles
        _randomPlacementPosition = new Vector3(0f, 5f, 100f);
        do
        {
            _randomPlacementPosition.x = Random.Range(-60f, 60f);
            _randomPlacementPosition.z += 150f;
            RandomizeWallObstacles();
        } while (_randomPlacementPosition.z <= 1900);

        // Spawn Thwomps
        _randomPlacementPosition = new Vector3(0f, 30f, 2000f);
        while (_randomPlacementPosition.z <= 2900f)
        {
            _randomPlacementPosition.x = Random.Range(-25f, 25f);
            _randomPlacementPosition.z += Random.Range(20f, 40f);
            RandomizeThwompObstacles();
        }

        // Spawn Nards
	}
	
	void Update ()
    {
        // Detect pause input, etc	
	}

    void InitializeGame() { }

    void RandomizeInkSpots()
    {
        // Spawn InkSpot
        GameObject newInkSpot = (GameObject)Instantiate(InkSpotPrefab, _randomPlacementPosition, Quaternion.identity);
        // Randomize rotation
        newInkSpot.transform.rotation = Quaternion.Euler(90f, Random.Range(0f, 180f), 0f);
        // Randomize scale
        newInkSpot.transform.localScale = new Vector3(Random.Range(3f, 10f), Random.Range(3f, 10f), 1f);
        // Assign Sprite
        newInkSpot.GetComponent<SpriteRenderer>().sprite = InkSpotSprites[1];   // Randomize this
        // Assign color
        newInkSpot.GetComponent<SpriteRenderer>().color = _inkSpotColors[Random.Range(0, _inkSpotColors.Length)];
        // Reset _inkPlacementPosition.x
        _randomPlacementPosition.x = 0f;
    }

    void RandomizeWallObstacles()
    {
        // second wall should be spawned [20] units to the right/left of the first 
        Instantiate(WallPrefab, _randomPlacementPosition, Quaternion.identity);
        if (_randomPlacementPosition.x <= 0f)
        {
            // Spawn on the right
            _randomPlacementPosition.x += 50f;
            Instantiate(WallPrefab, _randomPlacementPosition, Quaternion.identity);
        }
        else
        {
            _randomPlacementPosition.x -= 50f;
            Instantiate(WallPrefab, _randomPlacementPosition, Quaternion.identity);
        }
    }

    void RandomizeThwompObstacles()
    {
        // y component should be consistent (30)
        Instantiate(ThwompPrefab, _randomPlacementPosition, Quaternion.identity); 
    }

    void RandomizeNardObstacles()
    {
        // y component should be consistent
        // x component should be based on a value of -1 to 1
    }
}
