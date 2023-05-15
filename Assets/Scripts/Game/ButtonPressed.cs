using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game
{
    public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _timeBeforeStartAction;
        [SerializeField] private float _actionSpeed;
        [SerializeField] private UnityEvent pressed;
        [SerializeField] private UnityEvent unPressed;
        private float _pressingTime;
        private float _timeSinceLastAction;
        private bool _buttonPressed;

        private void OnDisable()
        {
            _buttonPressed = false;
            _pressingTime = 0;
            _timeSinceLastAction = 0;
        }

        private void Update()
        {
            if (_timeBeforeStartAction == 0 && _actionSpeed == 0)
            {
                return;
            }
            if (!_buttonPressed) return;
            if (_pressingTime < _timeBeforeStartAction) _pressingTime += Time.deltaTime;
            else
            {
                _timeSinceLastAction += Time.deltaTime;
                if (_timeSinceLastAction < _actionSpeed) return;
                _timeSinceLastAction = 0;
                pressed?.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_buttonPressed) return;
            if (_timeBeforeStartAction == 0 && _actionSpeed == 0)
            {
                pressed?.Invoke();
            }
            _buttonPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_buttonPressed) return;
            unPressed?.Invoke();
            _buttonPressed = false;
            _pressingTime = 0;
            _timeSinceLastAction = 0;
        }
    }
}