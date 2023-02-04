﻿namespace Application.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Handles OnTrigger events, and passes them to C# events.
    /// </summary>
    public class TriggerEvents : Trigger
    {
        private readonly List<Rigidbody2D> _currentRigidbodies = new List<Rigidbody2D>();
        private readonly List<Collider2D> _currentColliders = new List<Collider2D>();
        private readonly List<Rigidbody2D> _previousRigidbodies = new List<Rigidbody2D>();
        private readonly List<Collider2D> _previousColliders = new List<Collider2D>();

        [SerializeField]
        [Tooltip("Should this behavior draw debugging information and print to the console?")]
        private bool showDebug;

        [SerializeField]
        [Tooltip("Which layers should be ignored when checking collisions.")]
        private LayerMask excludeLayers;

        [SerializeField]
        [Tooltip("An event that is called when a collider enters this trigger.")]
        private UnityEvent<Collider2D> colliderTriggerEnter = new UnityEvent<Collider2D>();

        [SerializeField]
        [Tooltip("An event that is called when a collider exits this trigger.")]
        private UnityEvent<Collider2D> colliderTriggerExit = new UnityEvent<Collider2D>();

        [SerializeField]
        [Tooltip("An event that is called when a collider enters this trigger.")]
        private UnityEvent<Rigidbody2D> rigidbodyTriggerEnter = new UnityEvent<Rigidbody2D>();

        [SerializeField]
        [Tooltip("An event that is called when a collider exits this trigger.")]
        private UnityEvent<Rigidbody2D> rigidbodyTriggerExit = new UnityEvent<Rigidbody2D>();

        /// <summary>
        /// An event that is called the first frame when an object enters this trigger.
        /// </summary>
        public event Action<Collider2D> ColliderTriggerEnter;

        /// <summary>
        /// An event that is called the first frame when an object exits this trigger.
        /// </summary>
        public event Action<Collider2D> ColliderTriggerExit;

        /// <summary>
        /// An event that is called the first frame when a rigidbody enters this trigger.
        /// </summary>
        public event Action<Rigidbody2D> RigidbodyTriggerEnter;

        /// <summary>
        /// An event that is called the first frame when a rigidbody exits this trigger.
        /// </summary>
        public event Action<Rigidbody2D> RigidbodyTriggerExit;

        /// <inheritdoc/>
        public override event Action<GameObject> CollisionEnter;

        /// <inheritdoc/>
        public override event Action<GameObject> CollisionExit;

        /// <summary>
        /// Gets or sets the layers that should be excluded from triggering this object.
        /// </summary>
        /// <value>
        /// The layers that should be excluded from triggering this object.
        /// </value>
        public LayerMask ExcludeLayers
        {
            get => excludeLayers;
            set => excludeLayers = value;
        }

        /// <summary>
        /// Gets all of the rigidbodies currently inside this trigger.
        /// </summary>
        /// <value>
        /// All of the rigidbodies currently inside this trigger.
        /// </value>
        public IReadOnlyCollection<Rigidbody2D> CurrentRigidbodies => _currentRigidbodies;

        /// <summary>
        /// Gets all of the colliders currently inside this trigger.
        /// </summary>
        /// <value>
        /// All of the colliders currently inside this trigger.
        /// </value>
        public IReadOnlyCollection<Collider2D> CurrentColliders => _currentColliders;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (IsValidCollider(other) && enabled)
            {
                _currentColliders.Add(other);

                if (other.attachedRigidbody != null)
                {
                    _currentRigidbodies.Add(other.attachedRigidbody);
                }
            }
        }

        private void AddRigidbodies()
        {
            foreach (Rigidbody2D currentRigidbody in _currentRigidbodies)
            {
                if (!_previousRigidbodies.Contains(currentRigidbody))
                {
                    // this is new
                    CollisionEnter?.Invoke(currentRigidbody.gameObject);
                    RigidbodyTriggerEnter?.Invoke(currentRigidbody);
                    rigidbodyTriggerEnter.Invoke(currentRigidbody);

                    if (showDebug)
                    {
                        Debug.Log($"Rigidbody {currentRigidbody.name} entered the trigger {name}");
                    }
                }
            }
        }

        private void RemoveRigidbodies()
        {
            foreach (Rigidbody2D previousRigidbody in _previousRigidbodies)
            {
                if (!_currentRigidbodies.Contains(previousRigidbody))
                {
                    // this has been removed
                    CollisionExit?.Invoke(previousRigidbody.gameObject);
                    RigidbodyTriggerExit?.Invoke(previousRigidbody);
                    rigidbodyTriggerExit.Invoke(previousRigidbody);

                    if (showDebug)
                    {
                        Debug.Log($"Rigidbody {previousRigidbody.name} exited the trigger {name}");
                    }
                }
            }
        }

        private void AddColliders()
        {
            foreach (Collider2D currentCollider in _currentColliders)
            {
                if (!_previousColliders.Contains(currentCollider))
                {
                    // this is new
                    CollisionEnter?.Invoke(currentCollider.gameObject);
                    ColliderTriggerEnter?.Invoke(currentCollider);
                    colliderTriggerEnter.Invoke(currentCollider);

                    if (showDebug)
                    {
                        Debug.Log($"Collider {currentCollider.name} entered the trigger {name}");
                    }
                }
            }
        }

        private void RemoveColliders()
        {
            foreach (Collider2D previousCollider in _previousColliders)
            {
                if (!_currentColliders.Contains(previousCollider))
                {
                    // this has been removed
                    CollisionExit?.Invoke(previousCollider.gameObject);
                    ColliderTriggerExit?.Invoke(previousCollider);
                    colliderTriggerExit.Invoke(previousCollider);

                    if (showDebug)
                    {
                        Debug.Log($"Collider {previousCollider.name} exited the trigger {name}");
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (_currentRigidbodies.Count > _previousRigidbodies.Count)
            {
                // we gained some rigidbodies
                AddRigidbodies();
            }

            if (_currentRigidbodies.Count < _previousRigidbodies.Count)
            {
                // we lost some rigidbodies
                RemoveRigidbodies();
            }

            if (_currentColliders.Count > _previousColliders.Count)
            {
                // we gained some colliders
                AddColliders();
            }

            if (_currentColliders.Count < _previousColliders.Count)
            {
                // we lost some colliders
                RemoveColliders();
            }

            // Cleanup, to prepare for next physics update.
            _previousColliders.Clear();
            _previousRigidbodies.Clear();
            _previousColliders.AddRange(_currentColliders);
            _previousRigidbodies.AddRange(_currentRigidbodies);

            _currentRigidbodies.Clear();
            _currentColliders.Clear();
        }

        private bool IsValidCollider(Collider2D other)
        {
            // weird bit-mask code for checking if the object is a valid layer
            bool isExcludeLayer = excludeLayers.value == (excludeLayers.value | (1 << other.gameObject.layer));
            bool isTrigger = other.isTrigger;

            return !isTrigger && !isExcludeLayer;
        }

        private void OnGUI()
        {
            if (showDebug)
            {
                GUILayout.Label($"Colliders: {_currentColliders.Count}");
                GUILayout.Label($"Rigidbodies: {_currentRigidbodies.Count}");
            }
        }
    }
}
