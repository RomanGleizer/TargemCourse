using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private DicePanel _dicePanel;
    [SerializeField] private DiceDefinition _diceDefinition;
    [SerializeField] private EnemyDefinition _enemyDefinition;
    [SerializeField] private Image _enemyImage;
    [SerializeField] private CardPanel _cardPanel;

    [SerializeField] private float _pulseDuration = 0.5f;
    [SerializeField] private int _pulseLoops = 2;
    [SerializeField] private float _preEffectDelay = 0.5f;
    [SerializeField] private float _postEffectDelay = 1.0f;

    private HealthComponent _healthComponent;
    private PathogenController _playerController;
    private List<EquipmentCard> _equipmentCardsUI;

    public DicePanel DicePanel => _dicePanel;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _playerController = FindObjectOfType<PathogenController>();
        _healthComponent.InitializeHealth();
        _enemyImage.sprite = _enemyDefinition.EnemySprite;
    }

    public IEnumerator StartTurn()
    {
        if (_healthComponent.CurrentPoison > 0)
            _healthComponent.TakeDamageByPoison(_healthComponent.CurrentPoison);

        GenerateDice();
        GenerateCardsUI();

        yield return ActivateEquipment();

        // Очистка
        ClearDice();
        ClearCardsUI();
    }

    private void GenerateDice()
    {
        _dicePanel.ClearDice();
        for (int i = 0; i < _dicePanel.InitialCount; i++)
            _dicePanel.AddDice(_diceDefinition);
    }

    private void GenerateCardsUI()
    {
        ClearCardsUI();
        _equipmentCardsUI = new List<EquipmentCard>();
        foreach (var eqDef in _enemyDefinition.EquipmentDefinitions)
        {
            var card = _cardPanel.AddEquipment(eqDef);
            _equipmentCardsUI.Add(card);
        }
    }

    private void ClearCardsUI()
    {
        _cardPanel.ClearCard();
        _equipmentCardsUI?.Clear();
    }

    private void ClearDice()
    {
        _dicePanel.ClearDice();
    }

    private IEnumerator ActivateEquipment()
    {
        var diceList = _dicePanel.GetDice();
        for (int i = 0; i < _equipmentCardsUI.Count; i++)
        {
            var card = _equipmentCardsUI[i];

            if (card.Definition.Condition is CountCondition)
            {
                if (CanSatisfyCountCondition(card, diceList))
                {
                    yield return HandleCountCondition(card, diceList);
                }
            }
            else
            {
                yield return HandleSingleCondition(card, diceList);
            }

            if (card.RemainingUses <= 0)
            {
                _equipmentCardsUI.RemoveAt(i);
                i--;
            }
        }
    }

    private bool CanSatisfyCountCondition(EquipmentCard card, List<Dice> diceList)
    {
        var cond = (CountCondition)card.Definition.Condition;
        var needed = int.Parse(cond.ConditionText);
        return diceList.Sum(d => d.Value) >= needed;
    }

    private IEnumerator HandleCountCondition(EquipmentCard card, List<Dice> diceList)
    {
        var cond = (CountCondition)card.Definition.Condition;
        var needed = int.Parse(cond.ConditionText);
        var selected = new List<Dice>();
        var sum = 0;

        foreach (var die in diceList.OrderByDescending(d => d.Value))
        {
            selected.Add(die);
            sum += die.Value;
            if (sum >= needed) break;
        }

        yield return AnimatePulse(card.transform);

        if (_preEffectDelay > 0f)
            yield return new WaitForSeconds(_preEffectDelay);

        foreach (var die in selected)
        {
            card.ActivateEquipment(gameObject, _playerController.gameObject, die);
            diceList.Remove(die);
        }

        if (_postEffectDelay > 0f)
            yield return new WaitForSeconds(_postEffectDelay);
    }

    private IEnumerator HandleSingleCondition(EquipmentCard card, List<Dice> diceList)
    {
        var valid = diceList
            .Where(d => card.CanActivate(d.Value))
            .OrderBy(d => d.Value)
            .ToList();

        if (!valid.Any())
            yield break;

        var chosen = valid.Last();

        yield return AnimatePulse(card.transform);

        if (_preEffectDelay > 0f)
            yield return new WaitForSeconds(_preEffectDelay);

        card.ActivateEquipment(gameObject, _playerController.gameObject, chosen);
        diceList.Remove(chosen);

        if (_postEffectDelay > 0f)
            yield return new WaitForSeconds(_postEffectDelay);
    }

    private IEnumerator AnimatePulse(Transform cardTransform)
    {
        yield return cardTransform
            .DOScale(1.2f, _pulseDuration)
            .SetLoops(_pulseLoops, LoopType.Yoyo)
            .WaitForCompletion();
    }
}
