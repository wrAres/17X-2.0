using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    void Start()
    {
        // Cursor.visible = false;
        // cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 mousePos = new Vector2();
        // // Event currentEvent = Event.current;
        // mousePos.x = Input.mousePosition.x;
        // mousePos.y = cam.pixelHeight - Input.mousePosition.y;
        // Vector3 cursorPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        
        // print(cursorPos);
        // // this.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition - new Vector3(348.5f, 196f, 0f);
        // this.GetComponent<RectTransform>().anchoredPosition = cursorPos;
    }
}
