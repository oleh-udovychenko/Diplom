using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeathField
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> _skins;
        [SerializeField] protected Transform _shotPoint;

        [SerializeField] protected DamageRange _damage;

        [SerializeField] protected float _rateOfFire;

        protected float _lastShotTime = 0;

        public abstract void Shot();

        [System.Serializable]
        public struct DamageRange
        {
            public float minValue;
            public float maxValue;
        }
    }
}
