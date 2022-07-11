using DG.Tweening;
using UnityEngine;

namespace DBS.Catalyst.Unit
{
    public class cUnitSelectedVisual : MonoBehaviour
    {
        private Material material;

        private void Awake()
        {
            material = GetComponent<MeshRenderer>().material;
            
            //Ensure no unit is selected on start
            material.DOFade(0, 0f);
        }
        
        public void Select(Color c)
        {
            material.DOColor(c, .5f);
        }

        public void Deselect()
        {
            material.DOFade(0, .5f);
        }
    }
}
