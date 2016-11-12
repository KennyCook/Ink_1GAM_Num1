using UnityEngine;
using System.Collections;

public class CreationTriggerController : MonoBehaviour
{
    public GameObject GameController;
    void OnTriggerEnter(Collider collider)
    {
        GameController.GetComponent<GameController>().SpawnMoreInkSpots = true;
        gameObject.SetActive(false);
    }
}
