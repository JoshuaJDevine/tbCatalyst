using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBS.Catalyst.Animations
{
    [RequireComponent(typeof(Animator))]
    public class cMotor : MonoBehaviour
    {
        public Animator cAnimator { get; set; }

        private void Awake()
        {
            cAnimator = GetComponent<Animator>();
        }
    }
}
