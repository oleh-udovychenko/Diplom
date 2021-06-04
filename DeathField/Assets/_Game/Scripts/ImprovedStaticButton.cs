using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeathField
{
    public class ImprovedStaticButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static Action OnPointerDownEvent;
        public static Action OnPointerClickEvent;
        public static Action OnPointerUpEvent;

        private bool _isClick = false;

        private void Start()
        {
            OnPointerDownEvent += IsClick;
            OnPointerUpEvent += IsNotClick;
        }

        private void OnDisable()
        {
            OnPointerDownEvent -= IsClick;
            OnPointerUpEvent -= IsNotClick;
        }

        private void Update()
        {
            if (_isClick)
                OnPointerClickEvent?.Invoke();
        }

        private void IsClick()
        {
            _isClick = true;
        }

        private void IsNotClick()
        {
            _isClick = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke();
        }
    }
}
