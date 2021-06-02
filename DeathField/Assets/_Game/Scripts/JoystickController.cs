using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeathField
{
    public class JoystickController : MonoBehaviour
    {
        public static JoystickController Instance { get; private set; }

        private Joystick _joystick;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _joystick = GetComponent<Joystick>();
        }

        public Vector2 GetDirection()
        {
            return _joystick.Direction;
        }
    }
}
