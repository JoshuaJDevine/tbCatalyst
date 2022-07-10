using System;
using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst;
using DBS.Catalyst.System;
using DBS.Catalyst.Units;
using UnityEngine;

namespace DBS.Catalyst
{
    public class cTests : MonoBehaviour
    {
        [SerializeField] private cUnit unit;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                unit.GetMoveAction().GetValidActionGridPositionList();
            }
        }
    }
}
