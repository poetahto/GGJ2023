using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Health))]
    public class PlayerDeathListener : MonoBehaviour
    {
        private Health _health;

        private void OnEnable()
        {
            _health = GetComponent<Health>();
            _health.onDeath.AddListener(HandlePlayerDeath);
        }

        private void OnDisable()
        {
            _health.onDeath.RemoveListener(HandlePlayerDeath);
        }
        
        private void HandlePlayerDeath()
        {
            gameObject.SetActive(false);
            TransitionManager.instance.HandlePlayerDeath();
        }
    }
}