using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DBS.Catalyst.Camera
{
     public class cCameraController : MonoBehaviour
    {
        private const float MIN_FOLLOW_Y_OFFSET = 2f;
        private const float MAX_FOLLOW_Y_OFFSET = 12f;
        private Vector3 targetFollowOffest;
        private CinemachineTransposer cinemachineTransposer;
        
        [BoxGroup("Properties")] [SerializeField] public CinemachineVirtualCamera cinemachineVirtualCamera;
        [BoxGroup("Setting")] [ShowInInspector] public float MoveSpeed { get; set; } = 10f;
        [BoxGroup("Setting")] [ShowInInspector] public float RotationSpeed { get; set; } = 100f;
        [BoxGroup("Setting")] [ShowInInspector] public float ZoomAmount { get; set; } = 1f;
        [BoxGroup("Setting")] [ShowInInspector] public float ZoomSpeed { get; set; } = 5f;
        void Update()
        {
            HandleCameraMovement();
            HandleCameraRotation();
            HandleCameraZoom();
        }

        private void Awake()
        {
            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Start()
        {
            targetFollowOffest = cinemachineTransposer.m_FollowOffset;
        }

        private void HandleCameraMovement()
        {
            Vector3 inputMoveDirection = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDirection.z =  +1f;
            }        
            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDirection.z =  -1f;
            }        
            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDirection.x =  -1f;
            }        
            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDirection.x =  +1f;
            }

            Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
            transform.position += moveVector * MoveSpeed * Time.deltaTime;
        }

        private void HandleCameraRotation()
        {        
            Vector3 rotationVector = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector.y =  +1f;
            }        
            if (Input.GetKey(KeyCode.E))
            {
                rotationVector.y =  -1f;
            }
            
            transform.eulerAngles += rotationVector * RotationSpeed * Time.deltaTime;
        }

        private void HandleCameraZoom()
        {
            if (Input.mouseScrollDelta.y > 0) targetFollowOffest.y -= ZoomAmount;
            if (Input.mouseScrollDelta.y < 0) targetFollowOffest.y += ZoomAmount;
            targetFollowOffest.y = Mathf.Clamp(targetFollowOffest.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
            
            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffest, Time.deltaTime * ZoomSpeed);
        }
    }   
}

