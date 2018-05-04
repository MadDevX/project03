using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(1);
        }
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
