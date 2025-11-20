using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float maxClickDuration = 0.5f; 
    [SerializeField] private UnityEvent OnShortClick = new();

    private float pointerDownTime;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownTime = Time.time;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        float duration = Time.time - pointerDownTime;

        if (duration < maxClickDuration)
        {
            OnShortClick.Invoke();
        }
    }
}