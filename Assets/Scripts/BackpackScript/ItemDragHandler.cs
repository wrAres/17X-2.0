using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{   
    public static bool canPlaceItem = true;
    public static Vector3 previousPosition = new Vector3(0,0,0);

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        if (!canPlaceItem)
            this.GetComponent<RectTransform>().anchoredPosition = previousPosition;
        else 
            // PutObject.releaseItem = true;
            PutObject.Put();
    }
 }
