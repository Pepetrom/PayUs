using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite sprite;
    public int id;
    public Rigidbody rb;
    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickItem();
        }
    }
    public void PickItem()
    {
        if (GameManager.instance.inventory.AddItems(id))
        {
            Destroy(gameObject);
        }
    }
}
