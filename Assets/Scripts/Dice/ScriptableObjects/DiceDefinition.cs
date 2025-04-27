using UnityEngine;

[CreateAssetMenu(menuName = "Dice/Definition")]
public class DiceDefinition : ScriptableObject
{
    [SerializeField] private int _sides = 6;
    [SerializeField] private bool _onlyEven;

    public int Sides => _sides;
    public bool OnlyEven => _onlyEven;
}