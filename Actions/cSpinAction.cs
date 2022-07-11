using System;
using DG.Tweening;
using UnityEngine;

namespace DBS.Catalyst.Actions
{
    public class cSpinAction : cBaseAction
    {
        public override void Use(Action newTakeStartAction)
        {
            base.Use(newTakeStartAction);
        
            Unit.IsSpinning = true;
            transform
                .DOLocalRotate(new Vector3(0, 360,0), 1f, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .OnComplete(() => Unit.IsSpinning = false);
        }
    }
}
