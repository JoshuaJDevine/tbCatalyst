using System;
using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst;
using DBS.Catalyst.Units;
using UnityEngine;

public class cLevelGrid : MonoBehaviour
{
    public event EventHandler OnSetUnitAtGridPosition;
    public event EventHandler OnClearUnitAtGridPosition;

    public static cLevelGrid Instance { get; private set; }
    
    [SerializeField] private Transform gridDebugObject; 

    private cGridSystem gridSystem;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one unit action system! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        
        gridSystem = new cGridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObject);
    }

    public void SetUnitAtGridPosition(cGridPosition gridPosition, cUnit unit)
    {
        cGridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.GridUnit = (unit);
        OnSetUnitAtGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public cUnit GetUnitAtGridPosition(cGridPosition gridPosition)
    {
        cGridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GridUnit;
    }

    public void ClearUnitAtGridPosition(cGridPosition gridPosition)
    {
        cGridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.GridUnit = (null);
        OnClearUnitAtGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public cGridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }
}
