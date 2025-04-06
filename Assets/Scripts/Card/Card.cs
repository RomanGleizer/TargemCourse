using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumber;
    private int _number;
    private Cube _cube;

    void Start()
    {
        UpdateNumber();
    }

    private void Update()
    {
        if (_number <= 0)
        {
            UseCard();
        }
    }

    public void UpdateNumber()
    {
        _number = Random.Range(1, 7);
        _textNumber.text = _number.ToString();
    }

    public void UpdateNumber(int number)
    {
        _number = number;
        _textNumber.text = _number.ToString();
    }

    public void ReduceNumber(int number)
    {
        int newNumber = _number - number;
        UpdateNumber(newNumber);
    }

    public void UseCard()
    {
        print("UseCard");
        Destroy(gameObject);
    }
}
