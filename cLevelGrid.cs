using System;
using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst;
using UnityEngine;

public class cLevelGrid : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObject; 

    private cGridSystem gridSystem;
    private void Awake()
    {
        gridSystem = new cGridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObject);
    }
}
