using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DBS.Catalyst.Animations;
using DBS.Catalyst.System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst.Units
{
    public class cUnit : MonoBehaviour
    {
        [BoxGroup("Animation Setup")] public cMotor motor;
        [BoxGroup("Animation Setup")] public cUnitSelectedVisual selectedVisual;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsSelected { get; private set; }
        [BoxGroup("Properties")] [ShowInInspector] public bool IsBusy { get; set; } = false;
        [BoxGroup("Properties")] [ShowInInspector] public bool IsMoving { get; set; } = false;
        [BoxGroup("Properties")] [ShowInInspector] public Vector3 TargetPosition { get; set; }
        [BoxGroup("Properties")] [ShowInInspector] public float MoveSpeed { get; set; } = 4f;
        [BoxGroup("Properties")] [ShowInInspector] public float RotateSpeed { get; set; } = 10f;
        [BoxGroup("Properties")] [ShowInInspector] public float StopDistance { get; set; } = .1f;

        private void Awake()
        {
            TargetPosition = transform.position;
        }

        private void Start()
        {
            cUnitActionSystem.Instance.OnSelectedUnitChange += cUnitActionSystem_OnSelectedUnitChange;
        }
        private void Update()
        {
            MoveToTargetPosition();
        }

        private void cUnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty)
        {
            cUnitActionSystem unitActionSystem = (cUnitActionSystem)sender;

            if (unitActionSystem.SelectedUnit == this)
                Select();
            else
                Deselect();
        }
        private void MoveToTargetPosition()
        {
            if (Vector3.Distance(transform.position, TargetPosition) > StopDistance)
            {
                IsBusy = true;
                IsMoving = true;
                motor.cAnimator.SetFloat("speed", MoveSpeed);

                Vector3 moveDirection = (TargetPosition - transform.position).normalized;
                transform.position += moveDirection * MoveSpeed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime  * RotateSpeed);


            }
            else
            {
                IsBusy = false;
                IsMoving = false;
                motor.cAnimator.SetFloat("speed", 0f);
            }
        }

        public void Select()
        {
            selectedVisual.Select(Color.green);
            IsSelected = true;
        }

        public void Deselect()
        {
            selectedVisual.Deselect();
            IsSelected = false;
        }

        public void Move(Vector3 targetPosition)
        {
            this.TargetPosition = targetPosition;
        }
    }
}
