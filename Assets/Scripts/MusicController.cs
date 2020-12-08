using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	public GameObject OldMusic;
	public GameObject NewMusic;


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider) 
    {
    	OldMusic.SetActive(false);
    	NewMusic.SetActive(true);
    	Debug.Log("Hit the collider!");
    }
}
