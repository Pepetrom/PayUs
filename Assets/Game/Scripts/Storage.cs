using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private PickableOre[] oreSlots = new PickableOre[6];
    [SerializeField] private Transform[] oreTransforms = new Transform[6];
    public PickableOre[] Ores
    {
        get { return oreSlots; }
        set { oreSlots = value; }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("tentandoguardar");
        if (col.CompareTag("Pickable"))
        {
            for (int i = 0; i < oreSlots.Length; i++)
            {
                if (oreSlots[i] == null)
                {
                    PickableOre aux = col.GetComponent<PickableOre>();
                    if (!aux.stored)
                    {                     
                        Store(aux, i);
                    }
                    break;
                }
            }
        }
    }
    private void Store(PickableOre oreToStore, int slot)
    {
        oreSlots[slot] = oreToStore;
        oreToStore.PickUp(oreTransforms[slot], oreTransforms[slot],this, slot);
    }
    
    public void NullifyOreInArray(int i)
    {
        oreSlots[i] = null;
    }
}
