using System;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class PlaneMovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private PlaneGravityController planeGravityController;
        [SerializeField] private SurfaceAligner surfaceAligner;
        private Vector3 _inputVector;
        private PlaneState _currentState;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            PlaneStateChanger.OnPlaneStateChanged += HandlePlaneState;
        }

        private void OnDisable()
        {
            PlaneStateChanger.OnPlaneStateChanged -= HandlePlaneState;
        }


        private void Update()
        {
            InputVector();
        }
        private void FixedUpdate()
        {
            HandlePlaneState(_currentState);
        }
        private void HandlePlaneState(PlaneState state)
        {
            _currentState = state;
            switch (state)
            {
                case PlaneState.Accelerating:
                    transform.position += _inputVector * (moveSpeed * Time.fixedDeltaTime);
                    break;
                case PlaneState.Downhill:
                    transform.position += transform.forward * (moveSpeed * Time.fixedDeltaTime);
                    break;
                case PlaneState.Flying:
                    planeGravityController.enabled = true;
                    surfaceAligner.enabled = false;
                    HandlePlaneRotation();
                    break;
            }
        }

        private void HandlePlaneRotation()
        {
            transform.Rotate(transform.right, 10f * _inputVector.x * Time.deltaTime, Space.World);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("LaunchTrigger"))
            {
                _rigidbody.AddForce(transform.forward * moveSpeed,ForceMode.Impulse);
            }
        }

        private void InputVector()
        {
            _inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}
