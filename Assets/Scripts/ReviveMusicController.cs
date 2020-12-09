using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveMusicController : MonoBehaviour
{
	public GameObject OldMusic1;
  	public GameObject OldMusic2;
  	public GameObject OldMusic3;
 	public GameObject NewMusic;


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider) 
    {
    	OldMusic1.SetActive(false);
    	OldMusic2.SetActive(false);
    	OldMusic3.SetActive(false);
    	NewMusic.SetActive(true);
    }
}
