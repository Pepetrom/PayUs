using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public PickableOre[] ores = new PickableOre[6];
    public Transform[] oreSlots = new Transform[6];

    private void Store(PickableOre oreToStore, int slot)
    {
        RemoveWhatIsNotStored();
        ores[slot] = oreToStore;
        oreToStore.PickUp(oreSlots[slot], oreSlots[slot]);
    }

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
            //if all storage is occupied, the game will try to clean the storage and try to pickup the ore
            RemoveWhatIsNotStored();
            TryToStore(col.GetComponent<PickableOre>());
        }
    }
    private void RemoveWhatIsNotStored()
    {
        Debug.Log("mudança");
        for (int i = 0; i < ores.Length; ++i)
        {
            if (ores[i] != null)
            {
                if (!ores[i].stored)
                {
                    ores[i] = null;
                }
            }
        }
    }
    private void TryToStore(PickableOre ore)
    {
        for (int i = 0; i < ores.Length; i++)
        {
            if (ores[i] == null)
            {
                if (!ore.stored)
                {
                    Store(ore, i);
                }
                break;
            }
        }
    }
}
