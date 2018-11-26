using UnityEngine;
using UnityEngine.UI;

public class UIManager : BaseManager<UIManager>, IRestart {

    public Button RestartButton;
    public Button PlayerButton;
    public Button EnemyButton;
    public Button AttackButton;
    public GameObject moveButtons;

    public Text ResultText;
    public Text BulletText;
    public Text EnemyText;

    public Character SelectedCharacter { get; private set; }

    public static event System.Action RestartHandler;

    public enum Character
    {
        NONE,
        PLAYER,
        ENEMY
    };

    private void Awake()
    {
        instance = this;
        UIManager.RestartHandler += Restart;
    }

    private void OnDestroy()
    {
        UIManager.RestartHandler -= Restart;
    }

    // Update UI according to game state.
    public void SetUIOnGameState(GameState state)
    {
        switch(state)
        {
            case GameState.SEARCHING:
                RestartButton.gameObject.SetActive(false);
                PlayerButton.gameObject.SetActive(false);
                EnemyButton.gameObject.SetActive(false);
                AttackButton.gameObject.SetActive(false);
                moveButtons.SetActive(false);
                ResultText.gameObject.SetActive(false);
                BulletText.gameObject.SetActive(false);
                EnemyText.gameObject.SetActive(false);
                break;
            case GameState.CREATING:
                RestartButton.gameObject.SetActive(false);
                PlayerButton.gameObject.SetActive(true);
                EnemyButton.gameObject.SetActive(true);
                AttackButton.gameObject.SetActive(false);
                moveButtons.SetActive(false);
                ResultText.gameObject.SetActive(false);
                BulletText.gameObject.SetActive(false);
                EnemyText.gameObject.SetActive(false);
                break;
            case GameState.FIGHTING:
                RestartButton.gameObject.SetActive(true);
                PlayerButton.gameObject.SetActive(false);
                EnemyButton.gameObject.SetActive(false);
                AttackButton.gameObject.SetActive(true);
                moveButtons.SetActive(true);
                ResultText.gameObject.SetActive(false);
                BulletText.gameObject.SetActive(true);
                EnemyText.gameObject.SetActive(true);
                break;
            case GameState.VICTORY:
                RestartButton.gameObject.SetActive(true);
                PlayerButton.gameObject.SetActive(false);
                EnemyButton.gameObject.SetActive(false);
                AttackButton.gameObject.SetActive(false);
                moveButtons.SetActive(false);
                ResultText.gameObject.SetActive(true);
                BulletText.gameObject.SetActive(false);
                EnemyText.gameObject.SetActive(false);
                ShowVictory();
                break;
            case GameState.DEFEAT:
                RestartButton.gameObject.SetActive(true);
                PlayerButton.gameObject.SetActive(false);
                EnemyButton.gameObject.SetActive(false);
                AttackButton.gameObject.SetActive(false);
                moveButtons.SetActive(false);
                ResultText.gameObject.SetActive(true);
                BulletText.gameObject.SetActive(false);
                EnemyText.gameObject.SetActive(false);
                ShowDefeat();
                break;
            default:
                RestartButton.gameObject.SetActive(false);
                PlayerButton.gameObject.SetActive(false);
                EnemyButton.gameObject.SetActive(false);
                AttackButton.gameObject.SetActive(false);
                moveButtons.SetActive(false);
                ResultText.gameObject.SetActive(false);
                BulletText.gameObject.SetActive(false);
                EnemyText.gameObject.SetActive(false);
                break;
        }
    }

    public void OnRestartButtonPress(Button button)
    {
        // Invoke restart event.
        RestartHandler();
    }

    public void OnPlayerButtonPress(Button button)
    {
        SelectedCharacter = Character.PLAYER;
    }

    public void OnEmemyButtonPress(Button button)
    {
        SelectedCharacter = Character.ENEMY;
    }

    public void OnAttackButtonPress(Button button)
    {
        if (Player.instance != null)
            Player.instance.Attack();
    }

    public void ShowVictory()
    {
        ResultText.text = "VICTORY";
    }

    public void ShowDefeat()
    {
        ResultText.text = "DEFEAT";
    }

    // Implement IRestart inferface.
    public void Restart()
    {
    }
}
