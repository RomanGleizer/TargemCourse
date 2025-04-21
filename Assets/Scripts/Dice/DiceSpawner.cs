using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private int _cubeCount = 3;
    [SerializeField] private GameObject cubePrefab;
    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform;
        for (int i = 0; i < _cubeCount; i++)
        {
            Instantiate(cubePrefab, parentTransform);
        }
    }
}

