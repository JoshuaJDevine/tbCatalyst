using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DBS.Catalyst
{
    public class cGridDebugObject : MonoBehaviour
    {
        private cGridObject gridObject;
        [SerializeField] private TextMeshPro text;
        public void SetGridObject(cGridObject gridObject)
        {
            this.gridObject = gridObject;
            UpdateText();
        }

        private void Awake()
        {
            cLevelGrid.Instance.OnSetUnitAtGridPosition += cLevelGridSystem_OnSetUnitAtGridPosition;
            cLevelGrid.Instance.OnClearUnitAtGridPosition += cLevelGridSystem_OnClearUnitAtGridPosition;
        }

        public void cLevelGridSystem_OnSetUnitAtGridPosition(object sender, EventArgs empty)
        {
            UpdateText();
        }

        public void UpdateText()
        {
            text.text = gridObject.ToString();
        }

        public void cLevelGridSystem_OnClearUnitAtGridPosition(object sender, EventArgs empty)
        {
            UpdateText();
        }
    }
}
