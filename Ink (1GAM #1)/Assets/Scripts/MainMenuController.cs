using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject ConrtolsMenu, CreditsMenu;

    public void OnStartGameButtonPress()
    {
        SceneManager.LoadScene("Main Game Scene", LoadSceneMode.Single);
    }

    public void OnExitGameButtonPress()
    {
        Application.Quit();
    }
}
