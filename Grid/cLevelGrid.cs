using System;
using DBS.Catalyst.Unit;
using UnityEngine;

namespace DBS.Catalyst.Grid
{
    public class cLevelGrid : MonoBehaviour
    {
        public event EventHandler OnSetUnitAtGridPosition;
        public event EventHandler OnClearUnitAtGridPosition;

        public static cLevelGrid Instance { get; private set; }
        
        [SerializeField] private Transform gridDebugObject; 
        [SerializeField] private GameObject gridSystemVisual;

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
            gridSystem.CreateDebugObjects(gridDebugObject, gridSystemVisual);
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

        public bool IsValidGridPosition(cGridPosition gridPosition)
        {
            return gridSystem.IsValidGridPosition(gridPosition);
        }

        public Vector3 GetWorldPosition(cGridPosition gridPosition)
        {
            return gridSystem.GetWorldPosition(gridPosition);
        }
        
        public cGridSystemVisual GetGridSystemVisual(cGridPosition gridPosition)
        {
            return gridSystem.GetGridSystemVisual(gridPosition);
        }
    }
}
