using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Net;
using DBS.Catalyst;
using DBS.Catalyst.Units;
using Sirenix.OdinInspector;
using UnityEngine;

public class cMoveAction : MonoBehaviour
{
    [BoxGroup("Setup")] public cUnit unit;
    [BoxGroup("Properties")] [ShowInInspector] public Vector3 TargetPosition { get; set; }
    [BoxGroup("Properties")] [ShowInInspector] public float MoveSpeed { get; set; } = 4f;
    [BoxGroup("Properties")] [ShowInInspector] public float RotateSpeed { get; set; } = 10f;
    [BoxGroup("Properties")] [ShowInInspector] public float StopDistance { get; set; } = .1f;
    [BoxGroup("Properties")] [ShowInInspector] public int MaxMoveDistance { get; set; } = 4;
    private void Awake()
    {
        TargetPosition = transform.position;
    }
    
    private void Update()
    {
        MoveToTargetPosition();
    }
    
    private void MoveToTargetPosition()
    {
        if (Vector3.Distance(transform.position, TargetPosition) > StopDistance)
        {
            unit.IsBusy = true;
            unit.IsMoving = true;
            unit.motor.cAnimator.SetFloat("speed", MoveSpeed);

            Vector3 moveDirection = (TargetPosition - transform.position).normalized;
            transform.position += moveDirection * MoveSpeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime  * RotateSpeed);
        }
        else
        {
            unit.IsBusy = false;
            unit.IsMoving = false;
            unit.motor.cAnimator.SetFloat("speed", 0f);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        cGridPosition targetGridPosition = cLevelGrid.Instance.GetGridPosition(targetPosition);
        
        //don't allow if not in valid position list
        if (!IsValidActionGridPosition(targetGridPosition)) return;
        
        Vector3 newTagetPos = cLevelGrid.Instance.GetWorldPosition(targetGridPosition);
        
        this.TargetPosition = newTagetPos;
        unit.UpdateGridPosition(newTagetPos);
    }

    public bool IsValidActionGridPosition(cGridPosition gridPosition)
    {
        List<cGridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    public List<cGridPosition> GetValidActionGridPositionList()
    {
        List<cGridPosition> validGridPositionList = new List<cGridPosition>();

        cGridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -MaxMoveDistance; x <= MaxMoveDistance; x++)
        {
            for (int z = -MaxMoveDistance; z <= MaxMoveDistance; z++)
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

        cGridSystemVisual currentGridSystemVisual = cLevelGrid.Instance.GetGridSystemVisual(unit.GetGridPosition());
        currentGridSystemVisual.HideVisual();

        cGridSystemVisual nextGridSystemVisual = cLevelGrid.Instance.GetGridSystemVisual(cLevelGrid.Instance.GetGridPosition(TargetPosition));
        nextGridSystemVisual.HideVisual();
    }
}

