using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

namespace DeathField
{
    public class PlayerManager : MonoBehaviour
    {
        private PhotonView _pv;

        private void Start()
        {
            _pv = GetComponent<PhotonView>();

            if (_pv.IsMine)
                CreateController();
        }

        private void CreateController()
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerParent"), Vector3.zero, Quaternion.identity);
        }
    }
}
