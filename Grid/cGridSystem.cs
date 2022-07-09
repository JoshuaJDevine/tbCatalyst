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

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    cGridPosition gridPosition = new cGridPosition(x, z);
                    Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), quaternion.identity);
                    cGridDebugObject gridDebugObject = debugTransform.GetComponent<cGridDebugObject>();
                    gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }

        public cGridObject GetGridObject(cGridPosition gridPosition)
        {
            return gridObjectArray[gridPosition.x, gridPosition.z];
        }
    }
}
