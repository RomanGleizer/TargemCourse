using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/MaxCondition", fileName = "NewMaxCondition")]
public class MaxCondition : AbstractEquipmentCondition
{
    [SerializeField] int neededValue;
    public override bool IsSatisfied(int diceValue)
    {
        return diceValue <= neededValue;
    }

    public override bool ChangeCondition(int diceValue)
    {
        Debug.Log($"EquipmentCard: Выпавшее значение {diceValue} больше необходимого.");
        return false;
    }
}
