using System;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class PlaneGravityController : MonoBehaviour
    {
        [Header("Gravity Settings")]
        [SerializeField] private float fallMultiplier = 2.5f;
        [SerializeField] private float lowJumpMultiplier = 2f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void Update()
        {
            if (!_rigidbody.useGravity && Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.useGravity = true;
            }
        }

        private void FixedUpdate()
        {
            ApplyCustomGravity();
        }
        
        /// <summary>
        /// Applies custom gravity depending on current vertical velocity.
        /// </summary>
        private void ApplyCustomGravity()
        {
            float gravity = Physics.gravity.y;

            if (_rigidbody.linearVelocity.y < 0f)
                _rigidbody.AddForce(Vector3.up * (gravity * (fallMultiplier - 1f)), ForceMode.Acceleration);
            else if (_rigidbody.linearVelocity.y > 0f)
                _rigidbody.AddForce(Vector3.up * (gravity * (lowJumpMultiplier - 1f)), ForceMode.Acceleration);
            else
                _rigidbody.AddForce(Vector3.up * (gravity * (fallMultiplier - 1f)), ForceMode.Acceleration);
        }
    }
}