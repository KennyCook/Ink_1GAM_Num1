using UnityEngine;

public class DestructionTriggerController : MonoBehaviour
{
    public GameObject GameController;
    void OnTriggerEnter(Collider collider)
    { 
        GameController.GetComponent<GameController>().TriggerInkSpotDestruction(transform.position.z);
        gameObject.SetActive(false);
    }
}
