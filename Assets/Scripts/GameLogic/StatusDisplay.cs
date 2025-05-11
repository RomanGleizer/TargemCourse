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
        // �������� �� �������
        healthComponent.OnHealthChanged += UpdateHealth;
        healthComponent.OnShieldChanged += UpdateShield;
        healthComponent.OnPoisonChanged += UpdatePoison;
        dicePanel.OnHeatChanged += UpdateHeat;
        dicePanel.OnChillsChanged += UpdateChill;

        // ������������� �������� ��� ������
        UpdateHealth(healthComponent.CurrentHealth);
        UpdateShield(healthComponent.CurrentShield);
        UpdatePoison(healthComponent.CurrentPoison);
        UpdateHeat(dicePanel.CurrentHeat);
        UpdateChill(dicePanel.CurrentChills);
    }

    private void UpdateHealth(float value)
    {
        hpText.text = $"�� {Mathf.RoundToInt(value)}";
    }

    private void UpdateShield(float value)
    {
        shieldText.text = $"��� {Mathf.RoundToInt(value)}";
    }

    private void UpdatePoison(int value)
    {
        poisonText.text = $"�� {value}";
    }

    private void UpdateHeat(int value)
    {
        heatText.text = $"� {value}";
    }

    private void UpdateChill(int value)
    {
        chillText.text = $"�� {value}";
    }
}
