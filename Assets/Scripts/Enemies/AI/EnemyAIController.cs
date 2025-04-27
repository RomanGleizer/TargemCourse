using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PathogenController), typeof(DicePoolController))]
public class EnemyAIController : MonoBehaviour
{
    // ����� ���� ������� �����, �� ����� ��� ����� ����� ���������
    [SerializeField] private List<EquipmentCard> _equipmentCards;

    private DicePoolController _dicePool;

    private void Awake()
    {
        _dicePool = GetComponent<DicePoolController>();
    }

    public void ExecuteTurn()
    {
        var availableDice = _dicePool.GetDicePool();

        foreach (var card in _equipmentCards)
        {
            var validDice = availableDice
                .Where(d => card.CanActivate(d.Value))
                .OrderBy(d => d.Value)
                .ToList();

            if (validDice.Count > 0)
            {
                var bestDice = validDice.Last();

                // ��� ��� �������������� ����������

                availableDice.Remove(bestDice);
                break;
            }
        }
    }
}
