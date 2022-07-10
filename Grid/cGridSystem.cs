using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace DBS.Catalyst
{
    public class cGridSystem
    {
        private int width;
        private int height;
        private float cellSize;
        private cGridObject[,] gridObjectArray;
        private cGridSystemVisual[,] gridSystemVisualArray;
        public cGridSystem(int width, int height, float cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridObjectArray = new cGridObject[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    cGridPosition gridPosition = new cGridPosition(x, z);
                    gridObjectArray[x,z] = new cGridObject(this, gridPosition);
                }
            }
        }

        public Vector3 GetWorldPosition(cGridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
        }

        public cGridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new cGridPosition(
                Mathf.RoundToInt(worldPosition.x / cellSize),
                Mathf.RoundToInt(worldPosition.z / cellSize)
            );
        }

        public void CreateDebugObjects(Transform debugPrefab, GameObject gridVisual)
        {
            Vector3 gridVisualOffset = new Vector3(0, .03f, 0);
            gridSystemVisualArray = new cGridSystemVisual[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    cGridPosition gridPosition = new cGridPosition(x, z);
                    Transform debugTransform = Object.Instantiate(debugPrefab, GetWorldPosition(gridPosition), quaternion.identity);
                    cGridDebugObject gridDebugObject = debugTransform.GetComponent<cGridDebugObject>();
                    gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                    
                    GameObject newGridVisual = Object.Instantiate(gridVisual, GetWorldPosition(gridPosition) + gridVisualOffset, gridVisual.transform.rotation) as GameObject;
                    gridSystemVisualArray[x, z] = newGridVisual.GetComponent<cGridSystemVisual>();
                }
            }
        }
        public cGridObject GetGridObject(cGridPosition gridPosition)
        {
            return gridObjectArray[gridPosition.x, gridPosition.z];
        }
        
        public cGridSystemVisual GetGridSystemVisual(cGridPosition gridPosition)
        {
            return gridSystemVisualArray[gridPosition.x, gridPosition.z];
        }

        public bool IsValidGridPosition(cGridPosition gridPosition)
        {
            return (gridPosition.x >= 0 && 
                    gridPosition.z >= 0 && 
                    gridPosition.x < width && 
                    gridPosition.z < height);
        }
    }
}
