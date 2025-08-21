using System;
using UnityEngine;
using UnityEngine.Events;

namespace _GameFolders.Scripts
{
    public enum PlaneState
    {
        Accelerating,
        Downhill,
        Launching,
        Flying,
        Crashing,
        Landing,
    }
    public class PlaneStateChanger : MonoBehaviour
    {
        public static UnityAction<PlaneState> OnPlaneStateChange;
        
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Downhill"))
            {
                OnPlaneStateChange.Invoke(PlaneState.Downhill);
            }
            else if (other.CompareTag("Uphill"))
            {
                OnPlaneStateChange.Invoke(PlaneState.Launching);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Uphill"))
            {
                OnPlaneStateChange.Invoke(PlaneState.Flying);
            }
        }
    }
}
