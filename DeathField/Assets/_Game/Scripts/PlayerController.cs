using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace DeathField
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private JoystickController _joystick;

        private PhotonView _pv;

        private void Start()
        {
            _pv = GetComponent<PhotonView>();

            if (!_pv.IsMine)
                Destroy(GetComponentInChildren<Camera>().gameObject);

            _joystick = JoystickController.Instance;
        }

        private void FixedUpdate()
        {
            if (!_pv.IsMine)
                return;

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = _joystick.GetDirection();
                direction.z = direction.y;
                direction.y = 0;
                Move(direction);
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(transform.position + (Time.fixedDeltaTime * _moveSpeed * direction));

            if (direction != Vector3.zero)
                _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, _rigidbody.rotation * Quaternion.LookRotation(direction), _rotationSpeed * Mathf.Abs(direction.x) * Time.fixedDeltaTime));
        }
    }
}
