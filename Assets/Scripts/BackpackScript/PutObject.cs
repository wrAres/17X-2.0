using UnityEngine;
using UnityEngine.EventSystems;

public class PutObject : MonoBehaviour
{
    public static GameObject itemOnGround;
    public static bool holdItem;
    // public static GameObject floor;

    // Start is called before the first frame update
    void Start()
    {
        itemOnGround = null;
        holdItem = false;
        // floor = GameObject.Find("Floor");
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonUp(0) && holdItem) {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
                GameObject imageObj = new GameObject(itemOnGround.name);
                SpriteRenderer image = imageObj.AddComponent<SpriteRenderer>(); //Add the Image Component script
                image.sprite = Resources.Load<Sprite>(itemOnGround.name); //Set the Sprite of the Image Component on the new GameObject
                imageObj.transform.position = hitInfo.point + new Vector3(0, 0.8f, 0);
                // imageObj.transform.position = hitInfo.point;
                imageObj.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
                Vector3 temp = imageObj.transform.rotation.eulerAngles;
                temp.x = 45f;
                imageObj.transform.rotation = Quaternion.Euler(temp);
                // imageObj.transform.rotation.x = 45;
                Backpack.backpack.GetComponent<Backpack>().RemoveItem(itemOnGround.name);
                holdItem = false;
            }
        }
    }
}
