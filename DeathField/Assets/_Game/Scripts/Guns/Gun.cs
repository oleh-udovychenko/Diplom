using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeathField
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> _skins;
        [SerializeField] protected GameObject _bullet;
        [SerializeField] protected Transform _shotPoint;

        [SerializeField] protected Vector2 _damage;

        [SerializeField] protected float _rateOfFire;

        public virtual void Shot()
        {
            Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
        }
    }
}
