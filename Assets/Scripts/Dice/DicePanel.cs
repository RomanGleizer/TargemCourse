using System;
using System.Collections.Generic;
using UnityEngine;

public class DicePanel : MonoBehaviour
{
    [SerializeField] private int _diceCount = 3;
    [SerializeField] private GameObject dicePrefab;

    private Transform parentTransform;
    private int _currentHeat;
    private int _currentChills;

    public event Action<int> OnHeatChanged;
    public event Action<int> OnChillsChanged;

    public int InitialCount => _diceCount;

    public int CurrentHeat => _currentHeat;
    public int CurrentChills => _currentChills;

    public void AddDice()
    {
        GameObject diceGO = Instantiate(dicePrefab, parentTransform);
        Dice dice = diceGO.GetComponent<Dice>();
    }

    public void AddDice(int value)
    {
        GameObject diceGO = Instantiate(dicePrefab, parentTransform);
        Dice dice = diceGO.GetComponent<Dice>();
    }

    public void AddDice(DiceDefinition definition)
    {
        var go = Instantiate(dicePrefab, transform);
        var dice = go.GetComponent<Dice>();
        dice.Initialize(definition);
    }

    public void AddHeat(int heat)
    {
        _currentHeat = heat;
        OnHeatChanged?.Invoke(_currentHeat);
    }

    public void AddChills(int chills)
    {
        _currentChills = chills;
        OnChillsChanged?.Invoke(_currentHeat);
    }

    public void ClearDice()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public List<Dice> GetDice()
    {
        var list = new List<Dice>();
        foreach (Transform child in transform)
            if (child.TryGetComponent<Dice>(out var d))
                list.Add(d);
        return list;
    }
}

