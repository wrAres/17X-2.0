﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TalisDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler {
    public enum Elements { METAL, WOOD, WATER, FIRE, EARTH, THUNDER, SUN, WIND, MOON, NONE };
    public Elements element;
    public bool locked, known;
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
            //transform.position = origin;
            gameObject.GetComponent<RectTransform>().localPosition = origin;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        RectTransform invPanel = talisman as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {
            Debug.Log("Added element: " + element);
            //setTalis = true;
            dispManager.GetComponent<TalismanManager>().AddCraft(element);
        }
    }

    // Start is called before the first frame update
    void Start() {
        origin = gameObject.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateOrigin(Vector3 newPos) {
        origin = newPos;
    }
}