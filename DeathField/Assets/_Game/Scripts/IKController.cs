using UnityEngine;
using System;
using System.Collections;

namespace DeathField
{
    [RequireComponent(typeof(Animator))]
    public class IKController : MonoBehaviour
    {
        private Animator _animator;

        private Transform _rightHandObj = null;
        private Transform _leftHandObj = null;

        public bool ikActive = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK()
        {
            if (_animator)
            {

                if (ikActive)
                {
                    if (_rightHandObj != null)
                    {
                        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObj.position);
                        _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObj.rotation);
                    }

                    if (_leftHandObj != null)
                    {
                        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandObj.position);
                        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandObj.rotation);
                    }
                }
                else
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                    _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

                    _animator.SetLookAtWeight(0);
                }
            }
        }

        public void SetIKPoints(Transform rightHandObj, Transform leftHandObj)
        {
            _rightHandObj = rightHandObj;
            _leftHandObj = leftHandObj;
        }
    }
}
