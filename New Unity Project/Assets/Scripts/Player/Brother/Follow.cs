using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float speedWhenClose, speedWhenFar;
    private float currentSpeed = 12;
    private bool facingRight = false, isAttacking = false;
    public Transform target;

    public Animator animator;

    void Update()
    {
        if(!isAttacking && Vector2.Distance(transform.position, target.position) > 0.3f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
            if (transform.position.x > target.position.x && facingRight)
                Flip();
            else if (transform.position.x < target.position.x && !facingRight)
                Flip();
        }

        if (Input.GetKeyDown(KeyCode.S))
            LightningAttackStart();
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentSpeed = speedWhenClose;
        animator.SetFloat("speedMultiplier", 1f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentSpeed = speedWhenFar;
        animator.SetFloat("speedMultiplier", 1.5f);
    }

    public void SetStateNormal()
    {
        isAttacking = false;
    }

    private void LightningAttackStart()
    {
        isAttacking = true;
        animator.SetTrigger("LightningAttack");
    }
    private void LightningAttack()
    {
        GameObject enemy = FindClosestEnemy();
        if(enemy != null)
        {
            RaycastHit2D rayToFloor = Physics2D.Raycast(enemy.transform.position, Vector2.down, Mathf.Infinity);
            Vector2 spawnPosition = new Vector2(rayToFloor.point.x, rayToFloor.point.y + 10);
            PrefabManager.instance.PlayVFX(PrefabManager.ListOfVFX.LightningBolt, spawnPosition);
        }
    }
    private GameObject FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("EnemyAlive");

        foreach(GameObject currentEnemy in allEnemies)
        {
            float currentDistance = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(currentDistance < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = currentDistance;
                closestEnemy = currentEnemy;
            }
        }

        return closestEnemy;
    }
}
