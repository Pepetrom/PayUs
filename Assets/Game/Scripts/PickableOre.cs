using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.UIElements;

public class PickableOre : MonoBehaviour
{
    [SerializeField]private int id, arrayPosition;
    [SerializeField] private Rigidbody rb;
    public bool stored;
    public Collider boxCollider;
    public int Id
    {
        get { return id; }
    }
    [SerializeField]private Storage actualStorage;
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
        //check and liberate space in storage
        if (actualStorage != null)
        {
            actualStorage.NullifyOreInArray(arrayPosition);
            actualStorage = null;
        }
    }
    public void PickUp(Transform owner, Transform position, Storage storage, int idForStorage)
    {
        stored = true;
        transform.SetParent(owner.transform, true);
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
        transform.localScale = Vector3.one;
        rb.isKinematic = true;
        boxCollider.isTrigger = true;
        //these are needed to remove the reference from storage
        actualStorage = storage;
        arrayPosition = idForStorage;
    }
    public void Drop()
    {
        stored = false;
        transform.parent = null;
        rb.isKinematic = false;
        boxCollider.isTrigger = false;
        transform.localScale = Vector3.one;
    }
    public void Launch(float power)
    {
        Debug.Log("lancoiu");
        Drop();
        rb.AddForce(GameManager.Instance.playerHead.transform.forward *power * 10, ForceMode.VelocityChange);
    }
    public void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
