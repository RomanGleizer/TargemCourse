using TMPro;
using UnityEngine;

// Переменовать в EquipmentCard на что-то другое (в данном случае это кубик)
public class EquipmentCard : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition equipmentDefinition;
    [SerializeField] private TextMeshProUGUI _textNumber;

    private int _diceValue;

    public EquipmentDefinition EquipmentDefinition => equipmentDefinition;

    void Start()
    {
        UpdateNumber();
    }

    public void UpdateNumber()
    {
        _diceValue = Random.Range(1, 7);
        _textNumber.text = _diceValue.ToString();
    }

    public void ActivateEquipment(GameObject target)
    {
        if (equipmentDefinition.Condition == null || equipmentDefinition.Condition.IsSatisfied(_diceValue))
        {
            print("Можно применить эффект");
        }
        else
        {
            Debug.Log($"EquipmentCard: Выпавшее значение {_diceValue} не удовлетворяет условию активации для {equipmentDefinition.Name}.");
        }
    }

    public void InitializeEquipmentDefinitionOnStart()
    {

    }
}