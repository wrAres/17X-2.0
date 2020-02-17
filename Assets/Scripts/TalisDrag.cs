using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TalisDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler {
    public enum Elements { METAL, WOOD, WATER, FIRE, EARTH, THUNDER, SUN, WIND, MOON };
    public Elements element;
    public bool locked;
    public Transform talisman, dispManager;

    private Vector3 origin;
    private bool setTalis;

    // Reset position of talisman
    private void OnDisable() {
        transform.position = origin;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!locked) {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!setTalis) {
            transform.position = origin;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        RectTransform invPanel = talisman as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {
            Debug.Log("Spell Detected: " + element);
            setTalis = true;
        }
    }

    // Start is called before the first frame update
    void Start() {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update() {

    }
}