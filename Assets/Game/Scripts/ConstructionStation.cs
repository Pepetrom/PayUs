using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ConstructionStation : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private int[] keys;
    [SerializeField] private bool[] keysUnlocked;
    [SerializeField] private GameObject door;
    private int openedKeysCount;
    private bool opened;

    private void Start()
    {
        storage = GetComponent<Storage>();
        keysUnlocked = new bool[keys.Length];
        storage.Ores = new PickableOre[keys.Length];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!opened)
        {
            if (other.CompareTag("Pickable"))
            {
                PickableOre temp = other.GetComponent<PickableOre>();
                if (!temp.stored)
                {
                    TestKey();
                }
            }
        }
    }
    public void TestKey()
    {
        for (int i = 0; i < storage.Ores.Length; i++)
        {
            if (storage.Ores[i].Id == keys[i])
            {
                keysUnlocked[i] = true;
            }
        }
        CheckUnlocked();
    }
    public void CheckUnlocked()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keysUnlocked[i])
            {
                openedKeysCount++;
            }
        }
        if (openedKeysCount == keys.Length)
        {
            OpenDoor();
        }
        openedKeysCount = 0;
    }
    public void OpenDoor()
    {
        door.SetActive(true);
        Destroy(gameObject);
    }
}
