using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private GameObject[] itens;
    [SerializeField] private int[] ids;
    [SerializeField] private GameObject sign, effect;
    [SerializeField] private PickableOre item;
    [SerializeField] private Transform place, itemPlace;
    [SerializeField] private Storage itemStorage;
    private float progress;
    private bool isCrafting;

    private void Start()
    {
        itemStorage = GetComponent<Storage>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(false);
            progress = 0;
            GameManager.Instance.uiManager.CraftingBar(0);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {    
            if (Input.GetKey(KeyCode.F))
            {
                StartCraft();
            }
        }
    }
    private void HitStart()
    {
        effect.SetActive(true);
    }
    private void HitEnd()
    {
        effect.SetActive(false);
        //Check the progress of the crafting
        if (progress == 1)
        {
            item.Die();
            Instantiate(itens[item.Id], itemPlace.transform.position,itemPlace.transform.rotation);
            progress = 0;
            GameManager.Instance.uiManager.CraftingBar(0);
        }
        else
        {
            progress += 0.2f;
            GameManager.Instance.uiManager.CraftingBar(progress);
        }
    }
    public void StartCraft()
    {
        item = itemStorage.Ores[0];
        if (item != null && !isCrafting)
        {
            try
            {
                if (itens[item.Id] != null)
                {
                    StartCoroutine(Crafting());
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
    }
    private IEnumerator Crafting()
    {
        isCrafting = true;
        GameManager.Instance.playerHead.canMove = false;
        HitStart();
        yield return new WaitForSeconds(0.5f);
        HitEnd();
        GameManager.Instance.playerHead.Craft();
        isCrafting = false;
    }
}
