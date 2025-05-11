using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    [Header("Text UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI poisonText;
    [SerializeField] private TextMeshProUGUI heatText;
    [SerializeField] private TextMeshProUGUI chillText;

    [Header("Components")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private DicePanel dicePanel;

    private void Start()
    {
        // Подписки на события
        healthComponent.OnHealthChanged += UpdateHealth;
        healthComponent.OnShieldChanged += UpdateShield;
        healthComponent.OnPoisonChanged += UpdatePoison;
        dicePanel.OnHeatChanged += UpdateHeat;
        dicePanel.OnChillsChanged += UpdateChill;

        // Инициализация значений при старте
        UpdateHealth(healthComponent.CurrentHealth);
        UpdateShield(healthComponent.CurrentShield);
        UpdatePoison(healthComponent.CurrentPoison);
        UpdateHeat(dicePanel.CurrentHeat);
        UpdateChill(dicePanel.CurrentChills);
    }

    private void UpdateHealth(float value)
    {
        hpText.text = $"НР {Mathf.RoundToInt(value)}";
    }

    private void UpdateShield(float value)
    {
        shieldText.text = $"Щит {Mathf.RoundToInt(value)}";
    }

    private void UpdatePoison(int value)
    {
        poisonText.text = $"Яд {value}";
    }

    private void UpdateHeat(int value)
    {
        heatText.text = $"Ж {value}";
    }

    private void UpdateChill(int value)
    {
        chillText.text = $"Оз {value}";
    }
}
