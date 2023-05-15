using UnityEngine;
using TMPro;

namespace Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextCopy : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textFrom;
        private TextMeshProUGUI _thisText;

        private void Awake() 
        {
            _thisText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _thisText.text = _textFrom.text;
        }
    }
}