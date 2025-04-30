using TMPro;
using UnityEngine;


public class Dice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textNumber;

    private int _value;
    private DiceDefinition _definition;
    private PathogenController _player;

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
        _textNumber.text = _value.ToString();
    }
}