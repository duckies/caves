﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base monster class so our monsters can inherit from
public class Monster : MonoBehaviour
{
    float moveSpeed;
    int attackDamage;
    // for implementing health bar later
    int lifePoints;
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
        lifePoints = lp;
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
        return lifePoints;
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

}