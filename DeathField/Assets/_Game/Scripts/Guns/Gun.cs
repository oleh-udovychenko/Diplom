using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeathField
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> _skins;

        [SerializeField] protected Transform _shotPoint;
        [SerializeField] protected Transform _leftHandPoint;
        [SerializeField] protected Transform _rightHandPoint;

        [SerializeField] protected DamageRange _damage;

        [SerializeField] protected float _rateOfFire;

        [SerializeField] protected int _bullets;

        protected float _lastShotTime = 0;

        public Transform LeftHandPoint => _leftHandPoint;
        public Transform RightHandPoint => _rightHandPoint;
        public int BulletsCount => _bullets;

        public abstract void Shot();

        [System.Serializable]
        public struct DamageRange
        {
            public float minValue;
            public float maxValue;
        }
    }
}
