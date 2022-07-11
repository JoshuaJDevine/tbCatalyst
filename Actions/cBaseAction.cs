using System;
using System.Collections.Generic;
using DBS.Catalyst.Grid;
using DBS.Catalyst.Unit;
using UnityEngine;

namespace DBS.Catalyst.Actions
{
    public abstract class cBaseAction : MonoBehaviour
    {
        private Action onTakeStartAction;

        public cUnit Unit { get; set; }

        public virtual void Use(Action newTakeStartAction)
        {
            onTakeStartAction = newTakeStartAction;
            onTakeStartAction();
        }
        public virtual void Use(Action newTakeStartAction, Vector3 targetPosition)
        {
             onTakeStartAction = newTakeStartAction;
             onTakeStartAction();
        }
        public bool IsValidActionGridPosition(cGridPosition gridPosition)
        {
            List<cGridPosition> validGridPositionList = GetValidActionGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }
        
        public List<cGridPosition> GetValidActionGridPositionList()
        {
            List<cGridPosition> validGridPositionList = new List<cGridPosition>();

            cGridPosition unitGridPosition = Unit.GetGridPosition();
            for (int x = -Unit.MaxMoveDistance; x <= Unit.MaxMoveDistance; x++)
            {
                for (int z = -Unit.MaxMoveDistance; z <= Unit.MaxMoveDistance; z++)
                {
                    cGridPosition offsetGridPosition = new cGridPosition(x, z);
                    cGridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                    
                    if (!cLevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (cLevelGrid.Instance.GetUnitAtGridPosition(testGridPosition)) continue;
                    
                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }
        
        public void HighlightValidMoves()
        {
            List<cGridPosition> validGridPositionList = GetValidActionGridPositionList();

            foreach (cGridPosition pos in validGridPositionList)
            {
                cGridSystemVisual gridSystemVisual = cLevelGrid.Instance.GetGridSystemVisual(pos);
                if (gridSystemVisual != null) gridSystemVisual.ShowVisual();
            }
        }
        
        public void HideAllMoves()
        {
            List<cGridPosition> validGridPositionList = GetValidActionGridPositionList();

            foreach (cGridPosition pos in validGridPositionList)
            {
                cGridSystemVisual gridSystemVisual = cLevelGrid.Instance.GetGridSystemVisual(pos);
                if (gridSystemVisual != null) gridSystemVisual.HideVisual();
            }

            cGridSystemVisual currentGridSystemVisual = cLevelGrid.Instance.GetGridSystemVisual(Unit.GetGridPosition());
            currentGridSystemVisual.HideVisual();

            cGridSystemVisual nextGridSystemVisual = cLevelGrid.Instance.GetGridSystemVisual(cLevelGrid.Instance.GetGridPosition(Unit.TargetPosition));
            nextGridSystemVisual.HideVisual();
        }
    }
}
