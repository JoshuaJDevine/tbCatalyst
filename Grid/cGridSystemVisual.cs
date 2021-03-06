using DG.Tweening;
using UnityEngine;

namespace DBS.Catalyst.Grid
{
    public class cGridSystemVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer.material.DOFade(0, 0);
        }

        public void ShowVisual()
        {
            meshRenderer.material.DOFade(1, .3f);
        }

        public void HideVisual()
        {
            meshRenderer.material.DOFade(0, .3f);
        }
    }
}
