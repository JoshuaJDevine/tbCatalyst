using System;
using DBS.Catalyst.Grid;
using UnityEngine;

namespace DBS.Catalyst.Actions
{
    public class cMoveAction : cBaseAction
    {
        public override void Use(Action newTakeStartAction, Vector3 targetPosition)
        {
            base.Use(newTakeStartAction);

            cGridPosition targetGridPosition = cLevelGrid.Instance.GetGridPosition(targetPosition);
        
            //don't allow if not in valid position list
            if (!IsValidActionGridPosition(targetGridPosition)) return;
        
            Vector3 newTagetPos = cLevelGrid.Instance.GetWorldPosition(targetGridPosition);
        
            Unit.TargetPosition = newTagetPos;
            Unit.UpdateGridPosition(newTagetPos);
        }
    }
}

