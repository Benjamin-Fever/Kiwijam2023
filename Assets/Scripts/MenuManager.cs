using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void OnTutorialClick()
    {
        SceneManager.LoadScene("Game Tutorial");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Game Credits");
    }

    public void OnReturnClick() {
        SceneManager.LoadScene("Game Main Menu");
    }
}
