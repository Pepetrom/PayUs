using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.UIElements;

public class PickableOre : MonoBehaviour
{
    public int id;
    public Rigidbody rb;
    public bool stored, held;
    public Collider boxCollider;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = rb.GetComponent<Collider>();
    }
    public void PickUp(Transform owner, Transform position)
    {
        stored = true;
        transform.SetParent(owner.transform,true);
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
        rb.isKinematic = true;
        boxCollider.isTrigger = true;
    }
    public void PickUp(Transform owner, Transform position, Storage storage)
    {
        stored = true;
        transform.SetParent(owner.transform, true);
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
        rb.isKinematic = true;
        boxCollider.isTrigger = true;
    }
    public void Drop()
    {
        transform.parent = null;
        rb.isKinematic = false;
        boxCollider.isTrigger = false;
        transform.localScale = Vector3.one;
    }
}
