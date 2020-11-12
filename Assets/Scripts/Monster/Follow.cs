using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public float speed = 4;
    public float attackDistance;
    public float bufferDistance;
    public GameObject player;
    public float minDist;
    public float maxDist;
    public int direction;
    public float distance;

    Transform playerTransform;

    [SerializeField] private Item item = null;
    void GetPlayerTransform()
    {
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.Log("Player not specified in Inspector");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //  GetPlayerTransform();
        //initialPosition = transform.position;
        direction = -1;
        
        //maxDist += transform.position.x;
        //minDist -= transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //distance = Vector2.Distance(playerTransform.position.x, transform.position.x);
        //Debug.Log("Distance to Player " + distance);
        // patrolling the area until player get close
        switch (direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x > minDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                //Moving Right
                if (transform.position.x < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = -1;
                }
                break;
        }
        /*var distance = Vector3.Distance(playerTransform.position, transform.position);
       
        if (distance <= attackDistance)
        {
            if (distance >= bufferDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); //transform.forward * attackSpeed * Time.deltaTime;
            }
        }
        else
        {

        }*/
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.instance.OnCreateItem(item);
            Destroy(gameObject);
            return;
        }
    }

}

