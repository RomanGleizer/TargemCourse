using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumber;
    private int _number;
    void Start()
    {
        UpdateNumber();        
    }

    public void UpdateNumber()
    {
        _number = Random.Range(1, 7);
        _textNumber.text = _number.ToString();
    }

}
