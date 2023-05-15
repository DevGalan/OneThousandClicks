using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game
{
    [ExecuteInEditMode()]
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerField;
        [SerializeField] private TextMeshProUGUI _contentField;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private int _characterWrapLimit;

        private void Update() 
        {
            int headerLenght = _headerField.text.Length;
            int contentLenght = _contentField.text.Length;

            _layoutElement.enabled = (headerLenght > _characterWrapLimit 
                                    || contentLenght > _characterWrapLimit) ? true
                                                                            : false;
        }
    }
}