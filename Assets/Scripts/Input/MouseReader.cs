using System;
using UnityEngine;

public class MouseReader : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Выберите кнопку для запуска/остановки таймера: LeftClick/RightClick")]
    private InputButton _primaryActionButton = InputButton.LeftClick;

    public event Action Clicked;
    private enum InputButton { LeftClick = 0, RightClick = 1 }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)_primaryActionButton))
        {
            Clicked?.Invoke();
        }
    }
}

