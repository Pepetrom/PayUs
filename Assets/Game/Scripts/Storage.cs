using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private PickableIten[] _slots;
    [SerializeField] private Transform[] _slotTransforms;
    private void Start()
    {
        _slots = new PickableIten[_slotTransforms.Length];
    }
    public PickableIten[] Itens
    {
        get { return _slots; }
        set { _slots = value; }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("tentandoguardar");
        if (col.CompareTag("Pickable"))
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)
                {
                    PickableIten aux = col.GetComponent<PickableIten>();
                    if (!aux.stored)
                    {                     
                        Store(aux, i);
                    }
                    break;
                }
            }
        }
    }
    public void TryStore(PickableIten itenToStore)
    {
        Debug.Log("tentandoguardar");
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i] == null)
            {
                Store(itenToStore, i);
                break;
            }
        }
    }
    private void Store(PickableIten itenToStore, int slot)
    {
        _slots[slot] = itenToStore;
        itenToStore.PickUp(_slotTransforms[slot], _slotTransforms[slot],this, slot);
    }
    
    public void NullifyOreInArray(int i)
    {
        _slots[i] = null;
    }
}
