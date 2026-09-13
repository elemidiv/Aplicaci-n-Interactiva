using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject spriteArrastrable;
    Vector3 posicionInicial;
    float zDistanciaCamara;
    Vector3 posicionDesfase;
    private int idPunteroActual = -100;

    public void OnBeginDrag(PointerEventData eventData)
    {
        spriteArrastrable = gameObject;
        idPunteroActual = eventData.pointerId;
        posicionInicial = transform.position;
        zDistanciaCamara = Mathf.Abs(posicionInicial.z - Camera.main.transform.position.z);

        posicionDesfase = posicionInicial - Camera.main.ScreenToWorldPoint
            (new Vector3(eventData.position.x, eventData.position.y, zDistanciaCamara));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != idPunteroActual)
            return;

        transform.position = Camera.main.ScreenToWorldPoint
            (new Vector3(eventData.position.x, eventData.position.y, zDistanciaCamara)) + posicionDesfase;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != idPunteroActual)
            return;

        spriteArrastrable = null;
        posicionDesfase = Vector3.zero;
    }
}