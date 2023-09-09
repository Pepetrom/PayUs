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
        storage.Itens = new PickableIten[keys.Length];
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                TestKey();
            }
        }
    }
    public void TestKey()
    {
        for (int i = 0; i < storage.Itens.Length; i++)
        {
            if ((storage.Itens[i] != null))
            {
                for(int j = 0; j < keys.Length; j++)
                {
                    if (storage.Itens[i].Id == keys[j] && keysUnlocked[j] != true)
                    {
                        keysUnlocked[j] = true;
                        break;
                    }
                }
            }
            else
            {
                keysUnlocked[i] = false;
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
