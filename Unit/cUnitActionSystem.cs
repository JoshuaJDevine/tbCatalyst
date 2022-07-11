using System;
using DBS.Catalyst.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst.Unit
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
                if (SelectedUnit.IsSelected && SelectedUnit.CanTakeAction())
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        SelectedUnit.GetMoveAction().Use(SelectedUnit.StartAction, cMouseWorld.GetPosition());
                        SelectedUnit.GetMoveAction().HideAllMoves();
                        DeselectAllUnits();
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
                    if (!unit.IsSelected && unit.CanTakeAction())
                    {
                        SelectUnit(unit);
                        unit.GetMoveAction().HighlightValidMoves();
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (SelectedUnit)
                {
                    if (SelectedUnit.IsSelected && SelectedUnit.CanTakeAction())
                    {
                        SelectedUnit.GetSpinAction().Use(SelectedUnit.StartAction);
                        DeselectAllUnits();
                    }
                }
            }
        }

        private void SelectUnit(cUnit unit)
        {
            SelectedUnit = unit;
            OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
        }

        private void DeselectAllUnits()
        {
            SelectedUnit = null;
            OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
