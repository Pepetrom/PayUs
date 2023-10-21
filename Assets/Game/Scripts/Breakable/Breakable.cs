using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] GameObject drop;
    public void TakeHit(float damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Instantiate(drop, transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
