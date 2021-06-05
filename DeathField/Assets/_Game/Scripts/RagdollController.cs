using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DeathField
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private Rigidbody _mainRigidobdy;
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _isActiveByDefault;

        private List<Rigidbody> _rigidBodies;

        public Transform Root => root;

        public bool IsActive { get; private set; }

        private void Awake()
        {
            _rigidBodies = root.GetComponentsInChildren<Rigidbody>().ToList();
            _rigidBodies.RemoveAll(rig => rig.gameObject.layer != LayerMask.NameToLayer("Ragdoll"));
            SetActiveRagdoll(_isActiveByDefault);
        }

        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            foreach (var rb in _rigidBodies)
                rb.AddForce(force, forceMode);
        }

        public void AddExplosionForce(Vector3 point, float force, float radius)
        {
            foreach (var rb in _rigidBodies)
                rb.AddExplosionForce(force, point, radius);
        }

        public void SetActiveRagdoll(bool isActive)
        {
            IsActive = isActive;

            if (_animator)
                _animator.enabled = !IsActive;
            if (_mainCollider)
                _mainCollider.enabled = !IsActive;
            if (_mainRigidobdy)
                _mainRigidobdy.isKinematic = isActive;

            foreach (var rb in _rigidBodies)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = !IsActive;
                rb.detectCollisions = IsActive;
                rb.interpolation = isActive ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
            }
        }
    }
}