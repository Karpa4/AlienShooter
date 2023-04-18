using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Проверяем, находится ли курсор мыши на кнопке паузы. Чтобы игрок не стрелял при нажатии на кнопку
/// </summary>
public class CheckButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Shooting _shooting;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _shooting.OnPauseButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _shooting.OnPauseButton = false;
    }

    /// <summary>
    /// Если игрок нажал на кнопку паузы, то сбрасываем этим методом при возвращении в игру
    /// </summary>
    public void SetFalse()
    {
        _shooting.OnPauseButton = false;
    }
}