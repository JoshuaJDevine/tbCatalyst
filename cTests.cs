using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst;
using DBS.Catalyst.System;
using UnityEngine;

namespace DBS.Catalyst
{
    public class cTests : MonoBehaviour
    {
        [SerializeField] private Transform gridDebugObject; 

        private cGridSystem gridSystem;
        void Start()
        {
            gridSystem = new cGridSystem(10, 10, 2f);
            gridSystem.CreateDebugObjects(gridDebugObject);
        }

        void Update()
        {
            Debug.Log(gridSystem.GetGridPosition(cMouseWorld.GetPosition()));
        }
    }
}
