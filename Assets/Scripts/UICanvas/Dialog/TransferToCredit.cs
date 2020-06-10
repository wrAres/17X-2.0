using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransferToCredit : MonoBehaviour
{
    public void Transfer() {
        DontDestroyVariables.fromGame = true;
        GameObject.Find("Finish 1").GetComponent<Image>().enabled = true;
        GameObject.Find("Finish 2").GetComponent<Image>().enabled = true;
        GameObject.Find("Prologue").GetComponent<Text>().enabled = true;
        GameObject.Find("Ends").GetComponent<Text>().enabled = true;
		Invoke("ToLoadScene", 5);
        print("Active Credits Scene in 5 secs");
	}
    private void ToLoadScene(){
        SceneManager.LoadScene("Credits");
    }
}
