using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/CountCondition", fileName = "NewCountCondition")]
public class CountCondition : AbstractEquipmentCondition
{
    [SerializeField] private int neededValue;
    [SerializeField] private int _initialValue;

    public override bool IsSatisfied(int diceValue)
    {
        return neededValue - diceValue <= 0;
    }

    public override void ChangeCondition(int diceValue)
    {       
        neededValue -= diceValue;
        ConditionText = neededValue.ToString();
    }

    public override void ResetCondition()
    {
        neededValue = _initialValue;
        ConditionText = neededValue.ToString();
    }
}
