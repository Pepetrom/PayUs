using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private PickableOre[] ores = new PickableOre[6];
    [SerializeField] private Transform[] oreSlots = new Transform[6];


    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("tentandoguardar");
        if (col.CompareTag("Pickable"))
        {
            for (int i = 0; i < ores.Length; i++)
            {
                if (ores[i] == null)
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
        ores[slot] = oreToStore;
        oreToStore.PickUp(oreSlots[slot], oreSlots[slot],this, slot);
    }
    
    public void NullifyOreInArray(int i)
    {
        ores[i] = null;
    }
}
