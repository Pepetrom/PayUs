using UnityEngine;

public class PickableIten : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private bool _isTool;
    [SerializeField] private string _name;
    private int _arrayPosition;
    private Rigidbody _rb;
    private Storage _storageItenIsIn;
    private Collider boxCollider;
    [HideInInspector]public bool stored;
    public int id
    {
        get { return _id; }
    }
    public string nameOfIten
    {
        get { return _name; }
    }
    public bool IsTool
    {
        get { return _isTool; }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        boxCollider = _rb.GetComponent<Collider>();
    }
    public void PickUp(Transform owner, Transform position)
    {
        gameObject.layer = 2;
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
        gameObject.layer = 9;
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
