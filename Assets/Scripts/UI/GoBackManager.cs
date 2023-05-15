using System.Collections.Generic;
using Game;
using UnityEngine;

public class GoBackManager : MonoBehaviour
{
    public static GoBackManager instance;

    private Stack<UIAnimation[]> _openedAnimations;

    public void AddToStack(UIAnimation[] uiAnimation)
    {
        _openedAnimations.Push(uiAnimation);
    }
    
    public void RemoveFromStack()
    {
        foreach (var item in _openedAnimations.Pop())
        {
            item.OutMove();
        }
    }

    private void Awake() 
    {
        instance = this;
        _openedAnimations = new Stack<UIAnimation[]>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_openedAnimations.Count == 0) 
            {
                ModalWindowPanel.instance.NewWindow()
                        .SetVertical()
                        .SetTitle("Leave the game")
                        .SetMessage("Are you sure you want to close the game?")
                        .SetConfirmAction("Yes", Application.Quit)
                        .SetDeclineAction("No", () => Debug.Log("Volviendo"))
                        .OpenMenu();
                return;
            }
            RemoveFromStack();
        }
    }
}