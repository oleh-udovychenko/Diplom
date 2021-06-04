using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DeathField
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float _lifeTime;
        [SerializeField] protected float _speed;

        [SerializeField] protected float _damage;

        private void Start()
        {
            DOVirtual.DelayedCall(_lifeTime, () => Destroy(gameObject));
        }

        private void FixedUpdate()
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }

        public virtual void SetGunSettings(float damage)
        {
            _damage += damage;
        }
    }
}
