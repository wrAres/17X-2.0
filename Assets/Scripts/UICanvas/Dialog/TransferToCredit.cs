using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferToCredit : MonoBehaviour
{
    public void Transfer() {
		Invoke("ToLoadScene", 5);
        print("Active Credits Scene in 5 secs");
	}
    private void ToLoadScene(){
        SceneManager.LoadScene("Credits");
    }
}
