using System;
using DBS.Catalyst.Actions;
using DBS.Catalyst.Grid;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst.Unit
{
    public class cUnit : MonoBehaviour
    {
        [BoxGroup("Animation Setup")] public cMotor motor;
        [BoxGroup("Animation Setup")] public cUnitSelectedVisual selectedVisual;

        [BoxGroup("Properties")] [ShowInInspector] public float RotateSpeed { get; set; } = 10f;
        [BoxGroup("Properties")] [ShowInInspector] public int MaxMoveDistance { get; set; } = 4;

        [BoxGroup("Properties")] [ShowInInspector] public float StopDistance { get; set; } = .1f;

        [BoxGroup("Properties")] [ShowInInspector] public float MoveSpeed { get; set; } = 4f;

        [BoxGroup("Properties")] [ShowInInspector] public Vector3 TargetPosition { get; set; }
        [BoxGroup("Properties")] [ShowInInspector] public string unitName;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsSelected { get; private set; }
        [BoxGroup("Properties")] [ShowInInspector] public bool IsBusy { get; set; } = false;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsSpinning { get; set; } = false;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsMoving { get; set; } = false;
        private cMoveAction MoveAction { get; set; }
        private cSpinAction SpinAction { get; set; }


        public override string ToString()
        {
            return unitName;
        }

        private void Awake()
        {
            TargetPosition = transform.position;
            
            MoveAction = GetComponent<cMoveAction>();
            if (MoveAction) MoveAction.Unit = this;

            SpinAction = GetComponent<cSpinAction>();
            if (SpinAction) SpinAction.Unit = this;
        }

        private void Update()
        {
            MoveToTargetPosition();
        }

        private void MoveToTargetPosition()
        {
            if (Vector3.Distance(transform.position, TargetPosition) > StopDistance)
            {
                IsBusy = true;
                IsMoving = true;
                motor.cAnimator.SetFloat("speed", MoveSpeed);

                Vector3 moveDirection = (TargetPosition - transform.position).normalized;
                transform.position += moveDirection * MoveSpeed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime  * RotateSpeed);
            }
            else
            {
                IsBusy = false;
                IsMoving = false;
                motor.cAnimator.SetFloat("speed", 0f);
            }
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
            return MoveAction;
        }
        
        public cSpinAction GetSpinAction()
        {
            return SpinAction;
        }

        public cGridPosition GetGridPosition()
        {
            return cLevelGrid.Instance.GetGridPosition(transform.position);
        }

        public void StartAction()
        {
            Debug.Log(unitName + " started an action using a delegate");
        }

        public bool CanTakeAction()
        {
            return !IsBusy && 
                   !IsMoving && 
                   !IsSpinning;
        }
    }
}
