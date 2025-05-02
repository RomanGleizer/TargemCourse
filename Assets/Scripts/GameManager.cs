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

    private bool _continuePressed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _continueButton.onClick.AddListener(OnContinueClicked);
        _continueButton.gameObject.SetActive(false);
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
        while (true)
        {
            Log("Начало хода игрока.");
            _continuePressed = false;
            _continueButton.gameObject.SetActive(true);

            _player.StartTurn();

            yield return new WaitUntil(() =>
                _continuePressed ||
                _player.DicePanel.GetDice().Count == 0
            );

            _continueButton.gameObject.SetActive(false);
            _player.DicePanel.ClearDice();
            Log("Ход игрока завершён.");

            Log("Ход врага.");
            _enemy.StartTurn();
            yield return new WaitForSeconds(1f);

            if (_enemy.GetComponent<HealthComponent>().CurrentHealth <= 0)
            {
                Log("Игрок победил!");
                break;
            }
            if (_player.GetComponent<HealthComponent>().CurrentHealth <= 0)
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
