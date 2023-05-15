using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class ModalWindowPanel : MonoBehaviour
    {
        public static ModalWindowPanel instance;

        [SerializeField]
        private float _closeTime;
        private float _closingTime;
        [Header("Header")] 
        [SerializeField]
        private Transform _headerArea;
        [SerializeField]
        private TextMeshProUGUI _titleField;
        [Header("Content")]
        [SerializeField]
        private Transform _contentArea;
        [SerializeField]
        private Transform _verticalLayoutArea; 
        [SerializeField] 
        private Image _image;
        [SerializeField]
        private TextMeshProUGUI _text;
        [Space()]
        [SerializeField]
        private Transform _horizontalLayoutArea;
        [SerializeField]
        private Transform _iconContainer;
        [SerializeField]
        private Image _iconImage;
        [SerializeField]
        private TextMeshProUGUI _iconText;
        [Header("Footer")]
        [SerializeField]
        private Transform _footerArea;
        [SerializeField]
        private Button _confirmButton;
        [SerializeField]
        private Button _declineButton;
        [SerializeField]
        private Button _alternateButton;
        [SerializeField]
        private TextMeshProUGUI _confirmText;
        [SerializeField]
        private TextMeshProUGUI _declineText;
        [SerializeField]
        private TextMeshProUGUI _alternateText;
        [Header("Data Request")]
        [SerializeField]
        private TMP_InputField _dataRequest;
        [SerializeField]
        private TMP_Dropdown _selectionRequest;

        private bool _closing;

        public Action<TMP_InputField> checkDataRequest;
        public Action afterClose;
        private Action onConfirmAction;
        private Action onDeclineAction;
        private Action onAlternativeAction;

        [SerializeField]
        private UnityEvent onOpen;
        [SerializeField]
        private UnityEvent onClose;

        public TMP_InputField DataRequest { get => _dataRequest; }
        public TMP_Dropdown SelectionRequest { get => _selectionRequest; }
        public bool Opened { get => _closing; set => _closing = value; }

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        private void Update()
        {
            if (!_closing) return;
            _closingTime += Time.deltaTime;
            if (_closingTime >= _closeTime)
            {
                _closingTime = 0;
                _closing = false;
                afterClose?.Invoke();
            }
        }

        public void Confirm()
        {
            if (_closing) return;
            onConfirmAction?.Invoke();
            Close();
        }

        public void Decline()
        {
            if (_closing) return;
            onDeclineAction?.Invoke();
            Close();
        }

        public void Alternate()
        {
            if (_closing) return;
            onAlternativeAction?.Invoke();
            Close();
        }

        public ModalWindowPanel NewWindow()
        {
            _titleField.text = "";
            _text.text = "";
            _iconText.text = "";
            _iconImage.sprite = null;
            _image.sprite = null;
            _confirmText.text = "";
            _declineText.text = "";
            _alternateText.text = "";
            onConfirmAction = null;
            onDeclineAction = null;
            onAlternativeAction = null;
            _dataRequest.placeholder.gameObject.GetComponent<TextMeshProUGUI>().text = "";
            _dataRequest.text = "";
            _selectionRequest.options = new List<TMPro.TMP_Dropdown.OptionData>();
            afterClose = null;
            checkDataRequest = null;
            return instance;
        }

        public ModalWindowPanel SetVertical()
        {
            _horizontalLayoutArea.gameObject.SetActive(false);
            _verticalLayoutArea.gameObject.SetActive(true);
            return instance;
        }

        public ModalWindowPanel SetHorizontal()
        {
            _horizontalLayoutArea.gameObject.SetActive(true);
            _verticalLayoutArea.gameObject.SetActive(false);
            return instance;
        }

        public ModalWindowPanel SetTitle(string title)
        {
            _titleField.text = title;
            return instance;
        }

        public ModalWindowPanel SetImage(Sprite imageToShow)
        {
            if (_horizontalLayoutArea.gameObject.activeSelf)
                _iconImage.sprite = imageToShow;
            else
                _image.sprite = imageToShow;
            return instance;
        }

        public ModalWindowPanel SetMessage(string message)
        {
            if (_horizontalLayoutArea.gameObject.activeSelf)
                _iconText.text = message;
            else
                _text.text = message;
            return instance;
        }

        public ModalWindowPanel SetConfirmAction(string confirmMessage, Action confirmAction)
        {
            _confirmText.text = confirmMessage;
            onConfirmAction = confirmAction;
            return instance;
        }

        public ModalWindowPanel SetDeclineAction(string declineMessage, Action declineAction)
        {
            _declineText.text = declineMessage;
            onDeclineAction = declineAction;
            return instance;
        }

        public ModalWindowPanel SetAlternateAction(string alternateMessage, Action alternateAction)
        {
            _alternateText.text = alternateMessage;
            onAlternativeAction = alternateAction;
            return instance;
        }

        public ModalWindowPanel SetDataRequestPlaceHolder(string placeHolder, int maxLength,
                TMP_InputField.ContentType contentType)
        {
            _dataRequest.placeholder.gameObject.GetComponent<TextMeshProUGUI>().text = placeHolder;
            _dataRequest.characterLimit = maxLength;
            _dataRequest.contentType = contentType;
            return instance;
        }

        public ModalWindowPanel SetDataRequestMessage(string message, int maxLength,
                TMP_InputField.ContentType contentType)
        {
            _dataRequest.text = message;
            _dataRequest.characterLimit = maxLength;
            _dataRequest.contentType = contentType;
            return instance;
        }

        public ModalWindowPanel SetDataRequestCheck(Action<TMP_InputField> checkDataRequest)
        {
            this.checkDataRequest = checkDataRequest;
            return instance;
        }

        public ModalWindowPanel SetSelectionRequestOptions(List<string> opciones, int selected)
        {
            if (selected >= opciones.Count) selected = opciones.Count - 1;
            _selectionRequest.AddOptions(opciones);
            _selectionRequest.value = selected;
            return instance;
        }

        public void OpenMenu()
        {
            bool hasTitle = !string.IsNullOrEmpty(_titleField.text);
            _headerArea.gameObject.SetActive(hasTitle); 
            bool hasImage = (_image.sprite != null || _iconImage.sprite != null);
            _image.gameObject.SetActive(hasImage);
            _iconImage.gameObject.SetActive(hasImage);
            bool hasDecline = (onDeclineAction != null);
            _declineButton.transform.parent.gameObject.SetActive(hasDecline);
            bool hasAlternate = (onAlternativeAction != null);
            _alternateButton.transform.parent.gameObject.SetActive(hasAlternate);
            bool hasDataRequest = !string.IsNullOrEmpty(_dataRequest.placeholder.gameObject.GetComponent<TextMeshProUGUI>().text)
                                    || !string.IsNullOrEmpty(_dataRequest.text);
            _dataRequest.gameObject.SetActive(hasDataRequest); 
            bool hasSelectionRequest = _selectionRequest.options.Count > 0;
            _selectionRequest.gameObject.SetActive(hasSelectionRequest); 
            if (_closing) return;
            Open();
        }

        public void CheckDataRequest()
        {
            checkDataRequest?.Invoke(_dataRequest);
        }

        private void Close()
        {
            _closing = true;
            onClose?.Invoke();
        }

        private void Open()
        {
            onOpen?.Invoke();
        }
    }
}