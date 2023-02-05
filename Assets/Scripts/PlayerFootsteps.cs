using System;
using FMODUnity;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerFootsteps : MonoBehaviour
    {
        public EventReference footstepEvent;
        public float stepDistance;

        private Vector3 _previousPosition;
        private float _elapsedDistance;
        
        private void Update()
        {
            var currentPosition = transform.position;

            if (Input.anyKey)
            {
                _elapsedDistance += (currentPosition - _previousPosition).magnitude;
            }

            if (_elapsedDistance >= stepDistance)
            {
                _elapsedDistance = 0;
                RuntimeManager.PlayOneShot(footstepEvent);
            }
            
            _previousPosition = currentPosition;
        }
    }
}