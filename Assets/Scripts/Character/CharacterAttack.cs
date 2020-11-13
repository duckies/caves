using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Animator anim;
    public float attackRange;

    // public Transform attackLocation;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // start attacking
        {

            anim.SetBool("IsAttacking", true);
            // doesn't work yet :(
            Collider2D[] damage = Physics2D.OverlapCircleAll(transform.position, attackRange);

            for (int i = 0; i < damage.Length; i++)
            {
            //  Destroy(damage[i].gameObject); 

               if(damage[i].gameObject != gameObject)
                {
                Blob enemy = damage[i].gameObject.GetComponent<Blob>();
                if (enemy != null){
                        Debug.Log("Hitting the Blob");
                enemy.health -= 1;

                }

                }
            }
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

}
