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

        private JoystickController _joystickControl;

        private PhotonView _pv;

        private Vector3 _moveDirection = new Vector3();
        private Vector3 _rotateDirection = new Vector3();

        private void Start()
        {
            _pv = GetComponent<PhotonView>();
            _joystickControl = JoystickController.Instance;
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            _moveDirection = GetCurrentDirection(_joystickControl.GetDirection(JoystickController.Joysticks.Move));
            _rotateDirection = GetCurrentDirection(_joystickControl.GetDirection(JoystickController.Joysticks.Attack));
        }

        private void FixedUpdate()
        {
            if (!_pv.IsMine)
                return;

            Move(_moveDirection);
            Rotate(_rotateDirection);
        }

        private Vector3 GetCurrentDirection(Vector3 direction)
        {
            Vector3 currentDirection = direction;
            currentDirection.z = direction.y;
            currentDirection.y = 0;

            return currentDirection;
        }

        private void Move(Vector3 direction)
        {
            if (direction == Vector3.zero)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                //stay anim
            }
            else
            {
                _rigidbody.MovePosition(transform.position + (Time.deltaTime * _moveSpeed * direction.normalized));

                if (_rotateDirection == Vector3.zero)
                    Rotate(direction);

                //move anim
            }
        }

        private void Rotate(Vector3 direction)
        {
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
