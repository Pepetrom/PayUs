using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private int[] keyCodes;
    [SerializeField] private bool[] keysUnlocked;
    [SerializeField] private GameObject door;
    private int openedKeysCount;

    private void Start()
    {
        storage = GetComponent<Storage>();
        keysUnlocked = new bool[keyCodes.Length];
        storage.Itens = new PickableIten[keyCodes.Length];
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
            if (storage.Itens[i] != null)
            {
                for(int j = 0; j < keyCodes.Length; j++)
                {
                    Debug.Log("checou");
                    if (keysUnlocked[j] != true && storage.Itens[i].id == keyCodes[j] )
                    {
                        Debug.Log("aberta");
                        keysUnlocked[j] = true;
                        //break;
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
        for (int i = 0; i < keyCodes.Length; i++)
        {           
            if (keysUnlocked[i])
            {
                openedKeysCount++;
            }
        }
        if (openedKeysCount == keyCodes.Length)
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
