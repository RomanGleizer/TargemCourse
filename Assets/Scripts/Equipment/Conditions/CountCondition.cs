using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/CountCondition", fileName = "NewCountCondition")]
public class CountCondition : AbstractEquipmentCondition
{
    [SerializeField] private int neededValue;
    [SerializeField] private int _initialValue;

    public override bool IsSatisfied(int diceValue)
    {
        return neededValue <= 0;
    }

    public override bool ChangeCondition(int diceValue)
    {       
        neededValue -= diceValue;
        ConditionText = neededValue.ToString();

        return true;
    }

    public override void ResetCondition()
    {
        neededValue = _initialValue;
        ConditionText = neededValue.ToString();
    }
}
