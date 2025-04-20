using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Conditions/EvenCondition", fileName = "NewEvenCondition")]
public class EvenCondition : AbstractEquipmentCondition
{
    public override bool IsSatisfied(int diceValue)
    {
        return diceValue % 2 == 0;
    }
}
