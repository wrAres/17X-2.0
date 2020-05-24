using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrors : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject f1;
	public GameObject f2;
	public GameObject f3;
	public GameObject f4;
	public GameObject f5;
	public GameObject f6;
	public GameObject[] mirrorArray = new GameObject[6];
	public GameObject shuffle;
	void Start()
    {
       
		mirrorArray[0] = f1;
		mirrorArray[1] = f2;
		mirrorArray[2] = f3;
		mirrorArray[3] = f4;
		mirrorArray[4] = f5;
		mirrorArray[5] = f6;
		shuffle.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void reset(){

		AIDataManager.ClickedWrongMirror();

		for(int i = 0;i<6;i++){
			int x = Random.Range(0,6);
			int y = Random.Range(0,6);
			Vector3 temp;
			temp = mirrorArray[x].transform.position;
			mirrorArray[x].transform.position = mirrorArray[y].transform.position;
			mirrorArray[y].transform.position = temp;
		}
	}
	public void disappear(){

		Debug.Log("disappeared");
		shuffle.GetComponent<Renderer>().enabled = true;
		f1.GetComponent<Renderer>().enabled = false;
		f2.GetComponent<Renderer>().enabled = false;
		f3.GetComponent<Renderer>().enabled = false;
		f4.GetComponent<Renderer>().enabled = false;
		f5.GetComponent<Renderer>().enabled = false;
		f6.GetComponent<Renderer>().enabled = false;
	}
	public void appear(){
		shuffle.GetComponent<Renderer>().enabled = false;
		f1.GetComponent<Renderer>().enabled = true;
		f2.GetComponent<Renderer>().enabled = true;
		f3.GetComponent<Renderer>().enabled = true;
		f4.GetComponent<Renderer>().enabled = true;
		f5.GetComponent<Renderer>().enabled = true;
		f6.GetComponent<Renderer>().enabled = true;
	}
	public void crackAll(){
		f1.GetComponent<flowerInMirror>().crack();
		f2.GetComponent<flowerInMirror>().crack();
		f3.GetComponent<flowerInMirror>().crack();
		f4.GetComponent<flowerInMirror>().crack();
		f5.GetComponent<flowerInMirror>().crack();
		f6.GetComponent<flowerInMirror>().crack();
	}
}
