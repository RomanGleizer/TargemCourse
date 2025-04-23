using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumber;
    private int _number;
    
    public void UpdateNumber()
    {
        _number = Random.Range(1, 7);
        _textNumber.text = _number.ToString();
    }

    public void UpdateNumber(int value)
    {
        _number = value;
        _textNumber.text = _number.ToString();
    }

    public void InteractionWithCard(Card card)
    {
        card.ReduceNumber(_number);
        Destroy(gameObject);
    }
}
