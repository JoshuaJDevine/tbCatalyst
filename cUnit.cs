using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DBS.Catalyst.Animations;
using DBS.Catalyst.System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst.Units
{
    public class cUnit : MonoBehaviour
    {
        [BoxGroup("Animation Setup")] public cMotor motor;
        [BoxGroup("Animation Setup")] public cUnitSelectedVisual selectedVisual;

        [BoxGroup("Properties")] [ShowInInspector] public string unitName;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsSelected { get; private set; }
        [BoxGroup("Properties")] [ShowInInspector] public bool IsBusy { get; set; } = false;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsMoving { get; set; } = false;
        private cMoveAction moveAction { get; set; }


        public override string ToString()
        {
            return unitName;
        }

        private void Awake()
        {
            moveAction = GetComponent<cMoveAction>();
        }

        private void Start()
        {
            cUnitActionSystem.Instance.OnSelectedUnitChange += cUnitActionSystem_OnSelectedUnitChange;
            cGridPosition gridPosition = cLevelGrid.Instance.GetGridPosition(transform.position);
            cLevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
        }
        
        private void cUnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty)
        {
            cUnitActionSystem unitActionSystem = (cUnitActionSystem)sender;

            if (unitActionSystem.SelectedUnit == this)
                Select();
            else
                Deselect();
        }

        public void Select()
        {
            selectedVisual.Select(Color.green);
            IsSelected = true;
        }

        public void Deselect()
        {
            selectedVisual.Deselect();
            IsSelected = false;
        }
        
        public void UpdateGridPosition(Vector3 newTargetPosition)
        {
            cLevelGrid levelGrid = cLevelGrid.Instance;
            
            //Don't allow move if there is already a unit at this grid position
            if (levelGrid.GetUnitAtGridPosition(levelGrid.GetGridPosition(newTargetPosition))) return;
            
            //Clear unit from current grid position
            cGridPosition currentGridPosition = levelGrid.GetGridPosition(transform.position);
            levelGrid.ClearUnitAtGridPosition(currentGridPosition);
            
            //Change this target position &&
            //Set unit at the target grid position
            cGridPosition nextGridPosition = levelGrid.GetGridPosition(newTargetPosition);
            levelGrid.SetUnitAtGridPosition(nextGridPosition, this);
        }

        public cMoveAction GetMoveAction()
        {
            return moveAction;
        }

        public cGridPosition GetGridPosition()
        {
            return cLevelGrid.Instance.GetGridPosition(transform.position);
        }
    }
}
