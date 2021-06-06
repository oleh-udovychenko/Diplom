using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DeathField
{
    public class PistolGun : Gun
    {
        public override void Shot()
        {
            if (_bullets == 0 || Time.time - _lastShotTime <= _rateOfFire)
                return;

            _lastShotTime = Time.time;

            var bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BulletPistol"), _shotPoint.position, _shotPoint.rotation).GetComponent<Bullet>();

            bullet.SetGunSettings(Random.Range(_damage.minValue, _damage.maxValue));
            _bullets--;
        }
    }
}