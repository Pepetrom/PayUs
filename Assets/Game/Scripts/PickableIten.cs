using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.UIElements;

public class PickableIten : MonoBehaviour
{
    [SerializeField] private int _id, _arrayPosition;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private bool isTool;
    [SerializeField] private Storage _storageItenIsIn;
    public bool stored;
    public Collider boxCollider;
    public int Id
    {
        get { return _id; }
    }
    public bool IsTool
    {
        get { return isTool; }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        boxCollider = _rb.GetComponent<Collider>();
    }
    public void PickUp(Transform owner, Transform position)
    {
        stored = true;
        transform.SetParent(owner.transform,true);
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
        _rb.isKinematic = true;
        boxCollider.isTrigger = true;
        //check and liberate space in storage
        if (_storageItenIsIn != null)
        {
            _storageItenIsIn.NullifyOreInArray(_arrayPosition);
            _storageItenIsIn = null;
        }
    }
    public void PickUp(Transform owner, Transform position, Storage storage, int idForStorage)
    {
        stored = true;
        transform.SetParent(owner.transform, true);
        transform.position = position.transform.position;
        transform.rotation = position.transform.rotation;
        transform.localScale = Vector3.one;
        _rb.isKinematic = true;
        boxCollider.isTrigger = true;
        //these are needed to remove the reference from storage
        _storageItenIsIn = storage;
        _arrayPosition = idForStorage;
    }
    public void Drop()
    {
        stored = false;
        transform.parent = null;
        _rb.isKinematic = false;
        boxCollider.isTrigger = false;
        transform.localScale = Vector3.one;
    }
    public void Launch(float power)
    {
        Debug.Log("lancoiu");
        Drop();
        _rb.AddForce(GameManager.instance.playerLogic.transform.forward *power * 10, ForceMode.VelocityChange);
    }
    public void Die()
    {
        Drop();
        Destroy(gameObject);
    }
}
