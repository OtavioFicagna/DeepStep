using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private string gameSceneName = "GameScene";

    public BoardManager BoardManager;
    public PlayerController PlayerController;

    public UIDocument UIDoc;
    private Label m_FoodLabel;
    private Label m_lvlLabel;
    public int m_FoodAmount = 5;
    public int m_CurrentLevel = 1;
    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;
    public int Dificulty { get; set; }

    public TurnManager TurnManager { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Dificulty = PlayerPrefs.GetInt("Dificulty", 1);

        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;

        m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
        m_lvlLabel = UIDoc.rootVisualElement.Q<Label>("LevelLabel");

        m_GameOverPanel = UIDoc.rootVisualElement.Q<VisualElement>("GameOverPanel");
        m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverMessage");

        StartNewGame();
    }

    public void StartNewGame()
    {
        m_GameOverPanel.style.visibility = Visibility.Hidden;

        if (Dificulty == 1)
        {
            m_FoodAmount = 50;
        }
        if (Dificulty == 2)
        {
            m_FoodAmount = 80;
        }

        m_CurrentLevel = 1;
        m_FoodLabel.text = "Food : " + m_FoodAmount;
        m_lvlLabel.text = "Level : " + m_CurrentLevel;

        BoardManager.Clean();
        BoardManager.Init();

        PlayerController.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));
    }

    public void NewLevel()
    {
        BoardManager.Clean();
        BoardManager.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));

        m_CurrentLevel++;
        m_lvlLabel.text = "Level : " + m_CurrentLevel;
    }

    void OnTurnHappen()
    {
        if(Dificulty == 1)
        {
            ChangeFood(-1);
        }
        if (Dificulty == 2)
        {
            int food = Mathf.CeilToInt(m_CurrentLevel / 2f);
            ChangeFood(-food);
        }
    }

    public void ChangeFood(int amount)
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Food : " + m_FoodAmount;

        if (m_FoodAmount <= 0)
        {
            PlayerController.GameOver();
            m_GameOverPanel.style.visibility = Visibility.Visible;
            m_GameOverMessage.text = "Game Over!\n\nMorreu de fome :(\n\n\n\nSobreviveu : " + m_CurrentLevel + " levels";

        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
