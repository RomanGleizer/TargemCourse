using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumber;
    [SerializeField] private Sprite[] _diceSprite;
    [SerializeField] private Image _diceImage;

    private int _value;
    private DiceDefinition _definition; 

    public int Value => _value;

    public void Initialize(DiceDefinition definition)
    {
        _definition = definition;
        Roll();
    }

    public void Roll()
    {
        if (_definition == null) return;
        int max = _definition.Sides;
        int val;
        do
        {
            val = Random.Range(1, max + 1);
        } while (_definition.OnlyEven && val % 2 != 0);
        _value = val;
        _diceImage.sprite = _diceSprite[val-1];

        //_textNumber.text = _value.ToString();
    }
}