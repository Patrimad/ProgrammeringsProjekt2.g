using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class MMScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
