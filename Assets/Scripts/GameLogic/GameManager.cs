using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PathogenController _player;
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private Button _continueButton;
    [SerializeField] private TextMeshProUGUI _battleLogText;

    [SerializeField] private CountCondition[] _countConditions;

    private bool _continuePressed;
    private bool _battleEnded;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _continueButton.onClick.AddListener(OnContinueClicked);
        _continueButton.gameObject.SetActive(false);

        var playerHealth = _player.GetComponent<HealthComponent>();
        playerHealth.OnDeath += () => EndBattle(playerWon: false);

        var enemyHealth = _enemy.GetComponent<HealthComponent>();
        enemyHealth.OnDeath += () => EndBattle(playerWon: true);
    }

    private void EndBattle(bool playerWon)
    {
        if (_battleEnded) return;
        _battleEnded = true;
        _continuePressed = true;
        Log(playerWon ? "Игрок победил!" : "Враг победил!");
    }

    private void Start()
    {
        StartCoroutine(BattleLoop());
    }

    private void OnContinueClicked()
    {
        _continuePressed = true;
        _continueButton.gameObject.SetActive(false);
    }

    private IEnumerator BattleLoop()
    {
        GameModeManager.Instance.SetMode(GameModeManager.Mode.Battle);

        foreach (var cond in _countConditions)
        {
            cond.ResetCondition();
        }

        while (true)
        {
            Log("Начало хода игрока.");
            _continuePressed = false;
            _continueButton.gameObject.SetActive(true);

            _player.StartTurn();

            yield return new WaitUntil(() =>
                _continuePressed
            );

            _continueButton.gameObject.SetActive(false);
            _player.DicePanel.ClearDice();
            _player.CardPanel.ClearCard();
            Log("Ход игрока завершён.");

            if (_enemy == null || !_enemy.TryGetComponent<HealthComponent>(out var enemyHealth) || enemyHealth.CurrentHealth <= 0)
            {
                Log("Игрок победил!");
                break;
            }

            Log("Ход врага.");
            yield return _enemy.StartTurn();
            Log("Ход врага завершён.");

            if (_player == null || !_player.TryGetComponent<HealthComponent>(out var playerHealth) || playerHealth.CurrentHealth <= 0)
            {
                Log("Враг победил!");
                break;
            }
        }

        Log("Бой завершён.");
    }

    public void Log(string message)
    {
        _battleLogText.text += message + "\n";
    }
}
