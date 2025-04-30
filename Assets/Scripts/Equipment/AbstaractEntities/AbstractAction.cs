using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAction : ScriptableObject
{
    public abstract void ApplyEffect(GameObject attacker, GameObject target, int diceValue);
}

