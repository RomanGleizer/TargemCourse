using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment/Effects/Heal/ConstantHealEffect", fileName = "NewConstantHealEffectv")]
public class ConstantHealEffect : AbstractHealAction
{
    [SerializeField] private int _healAmount;
    protected override int CalculateHeal(int diceValue)
    {
        return _healAmount;
    }
}
