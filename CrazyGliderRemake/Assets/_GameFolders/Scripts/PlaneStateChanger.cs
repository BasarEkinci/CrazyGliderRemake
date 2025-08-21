using System;
using UnityEngine;
using UnityEngine.Events;

namespace _GameFolders.Scripts
{
    public enum PlaneState
    {
        Accelerating,
        Downhill,
        Flying
    }
    public class PlaneStateChanger : MonoBehaviour
    {
        public static UnityAction<PlaneState> OnPlaneStateChanged;

        private void Start()
        {
            OnPlaneStateChanged?.Invoke(PlaneState.Accelerating);
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("DownhillTrigger"))
            {
                OnPlaneStateChanged.Invoke(PlaneState.Downhill);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("LaunchTrigger"))
            {
                OnPlaneStateChanged.Invoke(PlaneState.Flying);
            }
        }
    }
}
