using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Need Collider on this object and 2d raycaster on camera to be work



[RequireComponent(typeof(BoxCollider2D))]
public class OnClickHandler : MonoBehaviour, IPointerDownHandler
{

    public UnityEvent clickEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        clickEvent?.Invoke();    
    }
}
