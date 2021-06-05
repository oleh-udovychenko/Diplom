using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

namespace DeathField
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] protected float _lifeTime;
        [SerializeField] protected float _speed;

        [SerializeField] protected float _damage;

        private Tween _lifeTween;

        private void Start()
        {
            _lifeTween = DOVirtual.DelayedCall(_lifeTime, () => PhotonNetwork.Destroy(gameObject));
        }

        private void FixedUpdate()
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.GetDamege(_damage);

                _lifeTween.Kill(false);
                PhotonNetwork.Destroy(gameObject);
            }

        }

        public virtual void SetGunSettings(float damage)
        {
            _damage += damage;
        }
    }
}
