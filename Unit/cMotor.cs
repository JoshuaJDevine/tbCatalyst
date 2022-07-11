using UnityEngine;

namespace DBS.Catalyst.Unit
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
