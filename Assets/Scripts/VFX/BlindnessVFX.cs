using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DefaultNamespace.VFX
{
    public class BlindnessVFX : MonoBehaviour
    {
        [SerializeField] private Volume volume;
        [SerializeField] private float smoothing;

        public float _currentPercentTarget;
        private Vignette _vignette;

        private void Awake()
        {
            volume.profile.TryGet(out _vignette);
        }

        private void Update()
        {
            volume.weight = Mathf.Lerp(volume.weight, _currentPercentTarget > 0.01f ? 1 : 0,
                smoothing * Time.deltaTime);
            
            _vignette.intensity.value =
                Mathf.Lerp(_vignette.intensity.value, _currentPercentTarget, smoothing * Time.deltaTime);
        }

        public void AddBlindness(float amount)
        {
            _currentPercentTarget = amount;
        }

        public void RemoveBlindness(float amount)
        {
            _currentPercentTarget = amount;
        }
    }
}