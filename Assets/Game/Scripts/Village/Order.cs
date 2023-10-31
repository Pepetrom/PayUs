using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum Type { Food,Ore,Wood,None}
public class Order : MonoBehaviour
{
    public Vector3 returnPoint, objectivePoint;
    public Villager owner;
    bool doing = false, returning;
    public Order next;
    public Type type;

    private void Update()
    {
        if (doing)
        {
            if (!returning)
            {
                if (Vector3.Distance(owner.transform.position, objectivePoint) < 2)
                {
                    StartCoroutine(Extract());
                }
            }
            else
            {
                if (Vector3.Distance(owner.transform.position, returnPoint) < 2)
                {
                    End();
                }
            }
        }
    }
    public void Started()
    {
        owner.ChangeProfession((int)type);
        doing = true;
        owner.SetDestination(objectivePoint);
        returning = false;
    }
    public void End()
    {
        doing = false;
        switch (type)
        {
            case Type.Ore:
                RandomOre();
                break;
            case Type.Wood:
                GameManager.instance.inventory.AddItems(9);
                break;
            case Type.Food:
                GameManager.instance.inventory.AddItems(10);
                break;
            case Type.None:
                break;
        }
        if(owner.orders.Count > 2)
        {
            Order aux = owner.orders[1];
            for (int i = 1; i < owner.orders.Count - 1; i++)
            {
                owner.orders[i] = owner.orders[i + 1];
            }
            owner.orders.RemoveAt(owner.orders.Count-1);
            Destroy(aux.gameObject);
        }    
            owner.orders[1].Started();
    }
    private void RandomOre()
    {
        GameManager.instance.inventory.AddItems(Random.Range(0, 9));
    }
    IEnumerator Extract()
    {        
        yield return new WaitForSeconds(5);
        owner.SetDestination(returnPoint);
        returning = true;
    }
}
