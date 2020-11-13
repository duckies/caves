using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// base monster class so our monsters can inherit from
public class Monster : MonoBehaviour
{
    float moveSpeed;
    int attackDamage;
    // for implementing health bar later
    int maxHealth;
    // distance to start attacking player
    float attackRadius;

    // distance to start following the player
    float followRadius;

    // action monster can take
    public enum MAction
    {
        Patrol,
        Follow,
        Attack 
         // etc...
    }
    MAction action;

    [SerializeField] protected private Slider healthSlider;

    private SpriteRenderer sprite;
    protected private Animator animator;
    protected private Transform _player;
    private float curHealth;
  //  private bool facingRight = true;

    protected virtual void Start()
    {
        curHealth = maxHealth;
        healthSlider.value = 1f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void setAction(MAction currAction)
    {
        action = currAction;
    }

    public void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void setAttackDamage(int attdmg)
    {
        attackDamage = attdmg;
    }

    public void setLifePoints(int lp)
    {
        maxHealth = lp;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public int getAttackDamage()
    {
        return attackDamage;
    }

    public int getLifePoints()
    {
        return maxHealth;
    }

    public MAction getAction()
    {
        return action;
    }

    //movement toward a player
    public void setFollowRadius(float r)
    {
        followRadius = r;
    }
    //attack radius 
    public void setAttackRadius(float r)
    {
        attackRadius = r;
    }

    //if player in radius move toward the player 
    public bool checkFollowRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < followRadius)
        {
            //player in range
            return true;
        }
        else
        {
            return false;
        }
    }

    //if player in radius attack the player
    public bool checkAttackRadius(float playerPosition, float enemyPosition)
    {
        if (Mathf.Abs(playerPosition - enemyPosition) < attackRadius)
        {
            //in range for attack
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    /*protected virtual void FaceCharacter()
    {
        float abs = transform.position.x - player.position.x;

        if (abs > 0 && !facingRight || abs < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }*/

    protected virtual void UpdateHealthBar()
    {
        healthSlider.value = Mathf.Clamp01(curHealth / maxHealth);
    }

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
    }
}
