using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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
