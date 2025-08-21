using System;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class PlaneMovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float gravityMultiplier = 2f;
        [SerializeField] private Rigidbody rb;
        private Vector3 _moveInput;
        private PlaneState _currentPlaneState;

        private void OnEnable()
        {
            PlaneStateChanger.OnPlaneStateChange += HandlePlaneStateChange;
        }
        private void OnDisable()
        {
            PlaneStateChanger.OnPlaneStateChange -= HandlePlaneStateChange;
        }

        private void HandlePlaneStateChange(PlaneState state)
        {
            _currentPlaneState = state;
            switch (state)
            {
                case PlaneState.Accelerating:
                    GetMovementInput(Input.GetAxis("Horizontal"),0);
                    rb.AddForce(transform.forward * (moveSpeed * _moveInput.magnitude), ForceMode.Force);
                    break;
                case PlaneState.Downhill:
                    rb.AddForce(transform.forward * moveSpeed);
                    break;
                case PlaneState.Launching:
                    rb.AddForce(transform.forward * moveSpeed);
                    break;
                case PlaneState.Flying:
                    GetMovementInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    transform.Rotate(transform.right, Time.deltaTime * moveSpeed * _moveInput.magnitude);
                    break;
                case PlaneState.Crashing:
                    break;
            }
        }
        private void FixedUpdate()
        {
            switch (_currentPlaneState)
            {
                case PlaneState.Accelerating:
                    GetMovementInput(Input.GetAxis("Horizontal"),0);
                    rb.AddForce(transform.forward * (moveSpeed * _moveInput.magnitude), ForceMode.Force);
                    break;
                case PlaneState.Downhill:
                    rb.AddForce(transform.forward * moveSpeed);
                    break;
                case PlaneState.Launching:
                    rb.AddForce(transform.forward * moveSpeed);
                    break;
                case PlaneState.Flying:
                    GetMovementInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    transform.Rotate(transform.right, Time.deltaTime * moveSpeed * _moveInput.magnitude);
                    break;
                case PlaneState.Crashing:
                    break;
            }
        }

        private void GetMovementInput(float horizontalInput,float verticalInput)
        {
            _moveInput = new Vector3(horizontalInput, 0,verticalInput);
        }
    }
}
