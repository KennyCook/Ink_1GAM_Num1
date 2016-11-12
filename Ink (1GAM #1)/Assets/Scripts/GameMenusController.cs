using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenusController : MonoBehaviour
{
    public GameObject PauseMenu, GameController;

    public void OnResumeGameButtonPress()
    {
        GameController.GetComponent<GameController>().ResumeGame();        
    }
    public void OnRestartGameButtonPress()
    {
        SceneManager.LoadScene("Main Game Scene", LoadSceneMode.Single);
    }
    public void OnMainMenuButtonPress()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
    public void OnQuitGameButtonPress()
    {
        Application.Quit();
    }
}
