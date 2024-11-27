using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Main";
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject menuPanel;
    private GameManager gameManager;

    // Função chamada pelo botão Start
    public void StartGame()
    {
        instructionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void NormalGame()
    {
        PlayerPrefs.SetInt("Dificulty", 1);
        SceneManager.LoadScene(gameSceneName);
    }

    public void HardGame()
    {
        PlayerPrefs.SetInt("Dificulty", 2);
        SceneManager.LoadScene(gameSceneName);
    }

    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
    }

    // Função chamada pelo botão Exit
    public void ExitGame()
    {
        // Fecha o jogo
        Application.Quit();

        // Mensagem para o editor do Unity
        #if UNITY_EDITOR
            Debug.Log("O jogo foi fechado.");
        #endif
    }
}