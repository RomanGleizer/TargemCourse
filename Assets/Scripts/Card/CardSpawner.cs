using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private int _cardCount = 3;
    [SerializeField] private GameObject cardPrefab;
    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform;
        for (int i = 0; i < _cardCount; i++)
        {
            Instantiate(cardPrefab, parentTransform);
        }
    }
}