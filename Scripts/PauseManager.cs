using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public GameObject gunsObject;

    //Pause panel wordt standard op niet active gezet zodat deze niet gelijk getoond wordt.
    void Start()
    {
        pausePanel.SetActive(false);
        gunsObject.SetActive(true);
    }

    //Als escape wordt gedrukt wordt er gekeken of het Pause Panel actief is, als dit niet het geval is wordt de functie 
    //PauseGame() opgeroepen waardoor het spel wel op pauze gezet wordt. Als er dan weer op escape wordt gedrukt 
    //en het pauze menu staat nog open wordt de functie ContinueGame() opgeroepen waardoor het panel weer weggehaald wordt.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            } 
        }
    }

    //Game wordt op pauze gezet.
    private void PauseGame()
    {
        gunsObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    
    //Game wordt weer aangezet en panel wordt weer wegehaald.
    private void ContinueGame()
    {
        gunsObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    //Scene wordt opnieuw geladen.
    public void RetryLevel()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    //Menu scene wordt geladen.
    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    //Applicatie wordt gesloten.
    public void QuitGame()
    {
        Application.Quit();
    }

    //Volgende level wordt geladen.
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

