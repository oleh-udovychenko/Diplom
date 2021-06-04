using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeathField
{
    public class JoystickController : MonoBehaviour
    {
        public static JoystickController Instance { get; private set; }

        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private Joystick _attackJoystick;

        public Joystick MoveJoystick => _moveJoystick;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public Vector2 GetDirection(Joysticks joystick)
        {
            switch (joystick)
            {
                case Joysticks.Move:
                    return _moveJoystick.Direction;
                case Joysticks.Attack:
                    return _attackJoystick.Direction;
                default:
                    return Vector2.zero;
            }
        }

        public enum Joysticks
        {
            Move,
            Attack,
        }
    }
}
