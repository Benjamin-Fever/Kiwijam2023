using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("BensScene");
    }

    public void OnTutorialClick()
    {
        
    }

    public void OnCreditsClick()
    {

    }
}
