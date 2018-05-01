using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectScript : MonoBehaviour
{

	void Awake ()
    {
        if(GameObject.FindGameObjectWithTag("PersistentObject")!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.tag = "PersistentObject";
            DontDestroyOnLoad(gameObject);
        }
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
