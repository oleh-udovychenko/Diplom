using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace DeathField
{
    public class PlayerController : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RagdollController _ragdoll;
        [SerializeField] private IKController _ik;
        [SerializeField] private Animator _animator;
        [SerializeField] private Camera _camera;

        [SerializeField] private Rigidbody _gunRigidbody;
        [SerializeField] private List<Gun> _guns;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxHealth;
        #endregion

        #region PrivateFields
        private ControlManager _controlManager;

        private PhotonView _pv;

        private Gun _activeGun;

        private Vector3 _moveDirection = new Vector3();
        private Vector3 _rotateDirection = new Vector3();

        private int _health;
        #endregion

        #region PrivateProperty
        private bool _isDead => _health <= 0f;
        #endregion

        #region UnityMethods
        private void Start()
        {
            _pv = GetComponent<PhotonView>();
            _controlManager = ControlManager.Instance;

            if (_pv.IsMine)
            {
                ImprovedStaticButton.OnPointerClickEvent += Shot;

                for (int i = 0; i < _guns.Count; i++)
                {
                    var currentIndex = i;

                    _controlManager.SwitchWeaponButtons[i].onClick.AddListener(delegate
                    {
                        SwitchWeapon(currentIndex);
                    });
                }
            }
            else
                Destroy(_camera.gameObject);

            _activeGun = _guns[0];
            SwitchWeapon(0);

            _health = (int)_maxHealth;
            _controlManager.SetHealth(_health, (int)_maxHealth);

        }

        private void Update()
        {
            if (!_pv.IsMine || _isDead)
                return;

            _moveDirection = GetCurrentDirection(_controlManager.GetDirection(ControlManager.Joysticks.Move));
            _rotateDirection = GetCurrentDirection(_controlManager.GetDirection(ControlManager.Joysticks.Attack));
        }

        private void FixedUpdate()
        {
            if (!_pv.IsMine || _isDead)
                return;

            Move(_moveDirection);
            Rotate(_rotateDirection);
        }

        private void OnDisable()
        {
            if (_pv.IsMine)
                ImprovedStaticButton.OnPointerClickEvent -= Shot;
        }
        #endregion

        #region PrivateMethods
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
                _animator.SetInteger("State", 0);
            }
            else
            {
                _rigidbody.MovePosition(transform.position + (Time.deltaTime * _moveSpeed * direction.normalized));
                _animator.SetInteger("State", 1);

                if (_rotateDirection == Vector3.zero)
                    Rotate(direction);
            }
        }

        private void Rotate(Vector3 direction)
        {
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.fixedDeltaTime);
        }

        private void Shot()
        {
            if (_isDead)
                return;

            _activeGun.Shot();
            _controlManager.SetBulletsText(_activeGun.BulletsCount);
        }

        private void SwitchWeapon(int index)
        {
            _activeGun.gameObject.SetActive(false);
            _activeGun = _guns[index];
            _activeGun.gameObject.SetActive(true);

            _ik.SetIKPoints(_activeGun.RightHandPoint, _activeGun.LeftHandPoint);
            _controlManager.SetBulletsText(_activeGun.BulletsCount);
        }

        private void Die()
        {
            _ragdoll.SetActiveRagdoll(true);
            _gunRigidbody.isKinematic = false;
            Debug.Log("Die");
        }
        #endregion

        #region PublicMethods
        public void GetDamege(float value)
        {
            if (_isDead)
                return;

            _health -= (int)value;
            _controlManager.SetHealth(_health, (int)_maxHealth);

            if (_isDead)
                Die();
        }
        #endregion
    }
}
