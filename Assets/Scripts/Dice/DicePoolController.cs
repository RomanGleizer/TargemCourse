using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DicePoolController : MonoBehaviour
{
    [SerializeField] private List<DiceDefinition> _diceDefinitions;
    [SerializeField] private Dice _dicePrefab;
    [SerializeField] private Transform _diceContainer;

    private List<Dice> _dicePool = new();

    public void InitializePool()
    {
        ClearPool();
        foreach (var def in _diceDefinitions)
        {
            var diceObj = Instantiate(_dicePrefab, _diceContainer);
            diceObj.Initialize(def);
            _dicePool.Add(diceObj);
        }
    }

    public void RollAll()
    {
        foreach (var dice in _dicePool)
            dice.Roll();
    }

    public List<Dice> GetDicePool() => _dicePool.ToList();

    private void ClearPool()
    {
        foreach (var d in _dicePool)
            Destroy(d.gameObject);
        _dicePool.Clear();
    }
}
