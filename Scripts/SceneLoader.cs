using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static bool hasDied = false;

    [SerializeField] private GameObject gameLogo, diedText;

    // Verander van scene
    public void ChangeScene(string sceneName)
    {
        ChangeSceneStatic(sceneName);
    }

    public static void ChangeSceneStatic(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Sluit de Applicatie.
    public void QuitGame()
    {
        Application.Quit();
    }

    public static void Died(bool _hasDied)
    {
        hasDied = _hasDied;
    }

    private void OnEnable()
    {
        gameLogo.SetActive(!hasDied);
        diedText.SetActive(hasDied);
    }
}