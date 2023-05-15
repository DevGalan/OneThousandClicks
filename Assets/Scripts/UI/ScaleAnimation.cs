using UnityEngine;

public class ScaleAnimation : UIAnimation
{
    [SerializeField] private Vector3 _finalPos;
    [SerializeField] private Vector3 _initialPos;

    public override void InMove()
    {
        if (_opening || _closing) return;
        LeanTween.scale(GetComponent<RectTransform>(), _finalPos, _duration).setEase(_inEaseType).setDelay(_delay).setOnComplete(() => {OnCompleteOpening();_opening = false;});
        AddToGoBackManager();
        _opening = true;
    }

    public override void OutMove()
    {
        if (_closing) return;
        onStartClosing?.Invoke();
        LeanTween.scale(GetComponent<RectTransform>(), _initialPos, _duration).setEase(_outEaseType).setDelay(_delay).setOnComplete(() => {OnComplete();_closing = false;});
        _closing = true;
    }
}