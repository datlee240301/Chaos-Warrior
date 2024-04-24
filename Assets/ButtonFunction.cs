using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour {

    public void LoadScene1() {
        SceneManager.LoadScene("Scene1");
    }
    
    public void LoadMainMenu() {
        SceneManager.LoadScene("Mainmenu");
    }
    
    public void ExitGame() {
        Application.Quit();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
