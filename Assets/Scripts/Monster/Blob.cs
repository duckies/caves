using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Monster
{
    public float speed;
    public int attack;
    public int health;
    public float followDis;
    public float attackDis;
    public GameObject player;
    private bool facingRight = false;
    public int range;
    private int direction;
    // public float distance;
    Vector3 orgPos;

    private MAction currentAction;

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
        orgPos = transform.position;
        setMoveSpeed(speed);
        setAttackDamage(attack);
        setLifePoints(health);
        setFollowRadius(followDis);
        setAttackRadius(attackDis);
        direction = -1;
        currentAction = MAction.Patrol;
        setAction(currentAction);

        StartCoroutine(BlobFSM());
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void Flip()
    {
    // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator BlobFSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentAction.ToString());
        }
    }

    IEnumerator Patrol()
    {
        Debug.Log("Entered Patrol State");
        while (currentAction == MAction.Patrol)
        {
            var target = player.transform.position;
            if (checkFollowRadius(target.x, transform.position.x))
            {
                //Debug.Log("Change to Follow state");
                currentAction = MAction.Follow;
                setAction(currentAction);
            }
            // move back and forth between range
            switch (direction)
            {
                case -1:
                    // Moving Left
                    if (transform.position.x > (orgPos.x - range))
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else
                    {
                        direction = 1;
                        Flip();
                    }
                    break;
                case 1:
                    //Moving Right
                    if (transform.position.x < (orgPos.x + range))
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                    }
                    else
                    {
                        direction = -1;
                        Flip();
                    }
                    break;
            }
            yield return null;
        }
        Debug.Log("Exited Patrol State");
    }

    IEnumerator Follow()
    {
        Debug.Log("Entered Follow State");
        var target = player.transform.position;
        target.x = player.transform.position.x - attackDis;
        //yield return new WaitForSeconds(ActivationDelay);

        while (currentAction == MAction.Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // change to attack if in range
            if (checkAttackRadius(player.transform.position.x, transform.position.x))
            {
                Debug.Log("Change to Attack State");
                currentAction = MAction.Attack;
                setAction(currentAction);
            }
            // if the distance from the player to the blob is out of reach => go back to patrolling
            else if(!checkFollowRadius(player.transform.position.x, transform.position.x))
            {
                // move back to original spot
                transform.position = Vector3.MoveTowards(transform.position, orgPos, speed * Time.deltaTime);
                currentAction = MAction.Patrol;
                setAction(currentAction);
            }
            yield return null;
        }
        Debug.Log("Exited Follow State");
    }

    IEnumerator Attack()
    {
        Debug.Log("Entered Attack State");
        var target = transform.position;
        target.x = player.transform.position.x;
        // yield return new WaitForSeconds(ActivationDelay);

        while (currentAction == MAction.Attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed *30f * Time.deltaTime);

            //reset
            if (transform.position == target)
            {
                transform.position = Vector3.MoveTowards(transform.position, orgPos, speed * Time.deltaTime);
                if (!checkFollowRadius(player.transform.position.x, transform.position.x))
                    currentAction = MAction.Patrol;
            }
            yield return null;
        }
        Debug.Log("Exited Attack State");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (currentAction == MAction.Patrol)
        //{
            setAction(MAction.Attack);
            Debug.Log("Transform to ATTACK state");
            currentAction = MAction.Attack;
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
       //   EventManager.instance.OnCreateItem(item);
            if(health == 0)
            Destroy(gameObject);
            return;
        }
    }
}
