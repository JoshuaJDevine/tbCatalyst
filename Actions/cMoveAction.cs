using System.Collections;
using System.Collections.Generic;
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
        this.TargetPosition = targetPosition;
        unit.UpdateGridPosition(targetPosition);
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
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
