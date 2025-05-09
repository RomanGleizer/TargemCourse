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

    //[SerializeField] private List<EquipmentDefinition> _equipmentDefinitions;
    [SerializeField] private CardPanel _cardPanel;

    [SerializeField] private float _pulseDuration = 0.5f;
    [SerializeField] private int _pulseLoops = 2;
    [SerializeField] private float _preEffectDelay = 0.5f;
    [SerializeField] private float _postEffectDelay = 1.0f;

    private HealthComponent _healthComponent;
    private PathogenController _playerController;
    private List<EquipmentCard> _equipmentCardsUI;

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

        for (int i = 0; i < _enemyDefinition.EquipmentDefinitions.Count; i++)
        {
            var eqDef = _enemyDefinition.EquipmentDefinitions[i];
            var valid = diceList
                .Where(d => eqDef.Condition == null || eqDef.Condition.IsSatisfied(d.Value))
                .OrderBy(d => d.Value)
                .ToList();
            if (valid.Count == 0)
                continue;

            var chosenDice = valid.Last();
            var uiCard = _equipmentCardsUI[i];

            yield return uiCard.transform
                .DOScale(1.2f, _pulseDuration)
                .SetLoops(_pulseLoops, LoopType.Yoyo)
                .WaitForCompletion();

            if (_preEffectDelay > 0f)
                yield return new WaitForSeconds(_preEffectDelay);

            foreach (var effect in eqDef.Effects)
                effect.ApplyEffect(gameObject, _playerController.gameObject, chosenDice.Value);

            diceList.Remove(chosenDice);
            Destroy(chosenDice.gameObject);

            if (_postEffectDelay > 0f)
                yield return new WaitForSeconds(_postEffectDelay);
        }
    }
}
