using DBS.Catalyst.Unit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst.Grid
{
    public class cGridObject : MonoBehaviour
    {
        [BoxGroup("Properties")] [ShowInInspector] public cGridSystem GridSystem { get; set; }
        [BoxGroup("Properties")] [ShowInInspector] public cGridPosition GridPosition { get; set; }
        [BoxGroup("Properties")] [ShowInInspector] public cUnit GridUnit { get; set; }

        public cGridObject(cGridSystem gridSystem, cGridPosition gridPosition)
        {
            this.GridSystem = gridSystem;
            this.GridPosition = gridPosition;
        }

        public override string ToString()
        {
            return this.GridPosition.ToString() + "\n" + GridUnit;
        }
    }
}
