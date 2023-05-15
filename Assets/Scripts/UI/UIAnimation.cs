using UnityEngine;
using UnityEngine.Events;

public abstract class UIAnimation : MonoBehaviour
{
    [SerializeField] protected UIAnimation[] _uiAnimations;
    [SerializeField] protected LeanTweenType _inEaseType;
    [SerializeField] protected LeanTweenType _outEaseType;
    [SerializeField] protected float _duration;
    [SerializeField] protected float _delay;
    [SerializeField] protected UnityEvent onCompleteCallback;
    [SerializeField] protected UnityEvent onCompleteOpeningCallback;
    [SerializeField] protected UnityEvent onStartClosing;
    [SerializeField] protected bool _closing;
    [SerializeField] protected bool _opening;

    public abstract void InMove();
    public abstract void OutMove();

    public void AddToGoBackManager()
    {
        if (_uiAnimations.Length == 0) return;
        if (!_opening) GoBackManager.instance.AddToStack(_uiAnimations);
    }

    public void RemoveFromGoBackManager()
    {
        if (_uiAnimations.Length == 0) return;
        if (!_closing) 
        {
            GoBackManager.instance.RemoveFromStack();
        }
    }

    public void OnComplete()
    {
        if (onCompleteCallback != null) onCompleteCallback?.Invoke();
    }

    public void OnCompleteOpening()
    {
        if (onCompleteCallback != null && !_closing) onCompleteOpeningCallback?.Invoke();
    }
}