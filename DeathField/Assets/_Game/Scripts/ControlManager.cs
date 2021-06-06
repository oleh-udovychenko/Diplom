using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeathField
{
    public class ControlManager : MonoBehaviour
    {
        public static ControlManager Instance { get; private set; }

        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private Joystick _attackJoystick;

        [SerializeField] private Text _healthText;
        [SerializeField] private Text _bulletsText;

        [SerializeField] private List<Button> _switchWeaponButtons;

        public Joystick MoveJoystick => _moveJoystick;
        public List<Button> SwitchWeaponButtons => _switchWeaponButtons;

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

        public void SetHealth(int nowValue, int maxValue)
        {
            _healthText.text = nowValue + "/" + maxValue;
        }

        public void SetBulletsText(int value)
        {
            _bulletsText.text = value.ToString();
        }

        public enum SwitchWeapons
        {
            Assault,
            Pistol,
            Sniper,
        }
    }
}
