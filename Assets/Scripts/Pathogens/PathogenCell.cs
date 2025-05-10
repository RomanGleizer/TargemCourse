using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathogenCell : MonoBehaviour
{
    [SerializeField] private PathogenDefinition _definition;
    [SerializeField] private Sprite _pathogenSprite;
    [SerializeField] private PathogenController _controller;
    [SerializeField] private Image _pathogenIcon;

    private void Start()
    {
        _pathogenIcon.sprite = _pathogenSprite;
    }
    public void ChoosePathogen()
    {
        _controller.Definition = _definition;
        _controller.Image.sprite = _pathogenSprite;
        _controller.ChangeEquipment();
    }
}
