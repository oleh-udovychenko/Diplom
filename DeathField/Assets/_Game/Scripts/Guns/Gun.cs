using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeathField
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> _skins;
        [SerializeField] protected Bullet _bullet;
        [SerializeField] protected Transform _shotPoint;

        [SerializeField] protected Vector2 _damage;

        [SerializeField] protected float _rateOfFire;

        protected float _lastShotTime = 0;

        public abstract void Shot();
    }
}
