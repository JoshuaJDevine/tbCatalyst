using System;
using System.Collections;
using System.Collections.Generic;
using DBS.Catalyst.Units;
using UnityEngine;

namespace DBS.Catalyst.System
{
    public class cMouseWorld : MonoBehaviour
    {
        private static cMouseWorld instance;
        [SerializeField] private LayerMask mousePlaneLayerMask;
        [SerializeField] private LayerMask unitLayerMask;

        private void Awake()
        {
            instance = this;
        }

        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
            return raycastHit.point;
        }

        public static cUnit GetUnit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out cUnit unit))
                {
                    return unit;
                }
            }
            return null;
        }
    }
}

