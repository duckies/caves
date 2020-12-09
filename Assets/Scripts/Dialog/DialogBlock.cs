using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBlock : MonoBehaviour
{
    //[SerializeField] private GameObject tree = null;
    //[SerializeField] private Dialog allArea = null;
    [SerializeField] private Dialog forestArea = null;
    [SerializeField] private Dialog lavaArea = null;

    Collider2D collide;
    //private int treeState;
    void Start()
    {
        //Fetch the GameObject's Collider (make sure it has a Collider component)
        collide = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D()
    {
        if (collide.enabled)
        {
            Debug.Log(name + " activated");
            //DialogManager.instance.StartDialog(allArea);

            if(name == "ForestBarrier")
                DialogManager.instance.StartDialog(forestArea);
            else
                DialogManager.instance.StartDialog(lavaArea);
        }
    }
}
