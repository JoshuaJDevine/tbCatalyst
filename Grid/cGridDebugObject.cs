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
            UpdateText(gridObject.ToString());
        }

        public void UpdateText(string newText)
        {
            text.text = newText;
        }
    }
}
