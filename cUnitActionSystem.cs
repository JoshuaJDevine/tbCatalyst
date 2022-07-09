using System;
using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst.System;
using Sirenix.OdinInspector;
using UnityEngine;


namespace DBS.Catalyst.Units
{
    public class cUnitActionSystem : MonoBehaviour
    {
        public static cUnitActionSystem Instance { get; private set; }
        
        public event EventHandler OnSelectedUnitChange;
        [BoxGroup("Properties")] [ShowInInspector] public cUnit SelectedUnit { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one unit action system! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            HandleUnitSelection();
            HandleUnitMovement();
        }

        private void HandleUnitMovement()
        {
            if (SelectedUnit)
            {
                if (SelectedUnit.IsSelected && !SelectedUnit.IsBusy && !SelectedUnit.IsMoving)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        SelectedUnit.Move(cMouseWorld.GetPosition());
                    }
                }
            }
        }
        private void HandleUnitSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                cUnit unit = cMouseWorld.GetUnit();
                if (unit)
                {
                    SelectUnit(unit);
                }
            }
        }

        private void SelectUnit(cUnit unit)
        {
            SelectedUnit = unit;
            OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
