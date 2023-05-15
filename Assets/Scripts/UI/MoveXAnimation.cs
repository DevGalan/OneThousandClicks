using UnityEngine;

public class MoveXAnimation : UIAnimation
{
    [SerializeField] private float _finalPos;
    [SerializeField] private float _initialPos;

    public override void InMove()
    {
        if (_opening || _closing) return;
        LeanTween.moveX(GetComponent<RectTransform>(), _finalPos, _duration).setEase(_inEaseType).setDelay(_delay).setOnComplete(() => {OnCompleteOpening();_opening = false;});
        AddToGoBackManager();
        _opening = true;
    }

    public override void OutMove()
    {
        if (_closing) return;
        onStartClosing?.Invoke();
        LeanTween.moveX(GetComponent<RectTransform>(), _initialPos, _duration).setEase(_outEaseType).setDelay(_delay).setOnComplete(() => {OnComplete();_closing = false;});
        _closing = true;
    }
}