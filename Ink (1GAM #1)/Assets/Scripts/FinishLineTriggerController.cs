using UnityEngine;

public class FinishLineTriggerController : MonoBehaviour
{
    public GameObject GameController;
    void OnTriggerEnter(Collider collider)
    {
        GameController.GetComponent<GameController>().PlayerWin = true;
    }
}
