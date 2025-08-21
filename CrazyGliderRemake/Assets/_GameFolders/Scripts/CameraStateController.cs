using UnityEngine;

namespace _GameFolders.Scripts
{
    public class CameraStateController : MonoBehaviour
    {
        [SerializeField] private Transform targetObject;
        [SerializeField] private float planeAccelerationOffset;
        [SerializeField] private float planeDownhillOffset;
        [SerializeField] private float planeFlyingOffset;
        [SerializeField] private float cameraSmoothSpeed = 5f;

        private Vector3 _desiredPosition;
        private PlaneState _planeState;
        private void OnEnable()
        {
            PlaneStateChanger.OnPlaneStateChanged += HandleCameraState;
        }
        private void OnDisable()
        {
            PlaneStateChanger.OnPlaneStateChanged -= HandleCameraState;
        }

        private void FixedUpdate()
        {
            HandleCameraState(_planeState);
        }

        private void HandleCameraState(PlaneState state)
        {
            _planeState = state;
            switch (state)
            {
                case PlaneState.Accelerating:
                    _desiredPosition = new Vector3(targetObject.position.x, targetObject.position.y, targetObject.position.z + planeAccelerationOffset);
                    break;
                case PlaneState.Downhill:
                    _desiredPosition = new Vector3(targetObject.position.x, targetObject.position.y, targetObject.position.z +planeDownhillOffset);
                    break;
                case PlaneState.Flying:
                    _desiredPosition = new Vector3(targetObject.position.x, targetObject.position.y, targetObject.position.z +planeFlyingOffset);
                    break;
            }
            transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * cameraSmoothSpeed);
        }
    }
}