using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst
{
    public class cGridObject : MonoBehaviour
    {
        [BoxGroup("Properties")] [ShowInInspector] public cGridSystem GridSystem { get; set; }
        [BoxGroup("Properties")] [ShowInInspector] public cGridPosition GridPosition { get; set; }

        public cGridObject(cGridSystem gridSystem, cGridPosition gridPosition)
        {
            this.GridSystem = gridSystem;
            this.GridPosition = gridPosition;
        }

        public override string ToString()
        {
            return this.GridPosition.ToString();
        }
    }
}
