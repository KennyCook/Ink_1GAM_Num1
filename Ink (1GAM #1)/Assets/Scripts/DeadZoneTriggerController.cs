using UnityEngine;

public class DeadZoneTriggerController : MonoBehaviour
{
    public GameObject GameController;
    void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<SphereController3D>().DestroyMe = true;
        GameController.GetComponent<GameController>().GameOver = true;
    }
}
