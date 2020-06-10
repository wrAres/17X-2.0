using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushHud : MonoBehaviour
{
    public GameObject MessagePanel;
    public Text PushMessage;

    // Start is called before the first frame update
    void Start()
    {
        MessagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessagePanel(string item)
    {
        PushMessage.text = "Hold F to move the rock";
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
