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

        private void Update()
        {
            InputVector();
        }
        private void FixedUpdate()
        {
            transform.position += _inputVector * (moveSpeed * Time.fixedDeltaTime);
        }

        private Vector3 InputVector()
        {
            _inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            return _inputVector;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("LaunchTrigger"))
            {
                planeGravityController.enabled = true;
                surfaceAligner.enabled = false;
            }
        }
    }
}
