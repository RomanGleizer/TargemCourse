using System;
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

    public int CurrentHeat => _currentHeat;
    public int CurrentChills => _currentChills;

    void Start()
    {
        parentTransform = transform;
        for (int i = 0; i < _diceCount; i++)
        {
            Instantiate(dicePrefab, parentTransform);
        }
    }

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
}

