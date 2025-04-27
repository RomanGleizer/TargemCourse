using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumber;
    private int _value;
    
    public void UpdateNumber()
    {
        _value = Random.Range(1, 7);
        _textNumber.text = _value.ToString();
    }

    public void UpdateNumber(int value)
    {
        _value = value;
        _textNumber.text = _value.ToString();
    }

    public void InteractionWithCard(EquipmentCard card)
    {
        //Enemy enemy = Find....<Enemy>
        //card.ActivateEquipment(gameobject, _value);

        //Надо добавить и разделить врага и патогена все-таки пупуп
    }
}
