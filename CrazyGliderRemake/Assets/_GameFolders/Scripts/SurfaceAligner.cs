using UnityEngine;

namespace _GameFolders.Scripts
{
    public class SurfaceAligner : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] private float rayLength = 5f;
        [SerializeField] private LayerMask groundMask;

        [Header("Alignment Settings")]
        [SerializeField] private float surfaceOffset = 0.5f;
        [SerializeField] private float rotationSmooth = 10f;
        [SerializeField] private bool keepForwardDirection = true;

        private Quaternion _lastValidRotation;

        private void Start()
        {
            _lastValidRotation = transform.rotation;
        }

        private void Update()
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, groundMask))
            {
                Vector3 currentPos = transform.position;
                float targetY = hit.point.y + surfaceOffset;
                Vector3 targetPosition = new Vector3(currentPos.x, targetY, currentPos.z);
                transform.position = targetPosition;

                Quaternion targetRotation;

                if (keepForwardDirection)
                {
                    Vector3 forward = Vector3.Cross(transform.right, hit.normal);
                    targetRotation = Quaternion.LookRotation(forward, hit.normal);
                }
                else
                {
                    targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }

                _lastValidRotation = targetRotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _lastValidRotation, Time.deltaTime * rotationSmooth);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
        }
    }
}