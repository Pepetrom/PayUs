using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private GameObject[] _itens;
    [SerializeField] private int[] _ids;
    [SerializeField] private GameObject _sign, _effect;
    [SerializeField] private PickableIten _item;
    [SerializeField] private Transform _place, _itemPlace;
    [SerializeField] private Storage _itemStorage;
    private float _progress;
    private bool _isCrafting;

    private void Start()
    {
        _itemStorage = GetComponent<Storage>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _sign.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _sign.SetActive(false);
            _progress = 0;
            GameManager.instance.uiManager.UpdateCraftingBar(0);
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
        _effect.SetActive(true);
    }
    private void HitEnd()
    {
        _effect.SetActive(false);
        //Check the progress of the crafting
        if (_progress == 1)
        {
            _item.Die();
            Instantiate(_itens[_item.Id], _itemPlace.transform.position,_itemPlace.transform.rotation);
            _progress = 0;
            GameManager.instance.uiManager.UpdateCraftingBar(0);
        }
        else
        {
            _progress += 0.2f;
            GameManager.instance.uiManager.UpdateCraftingBar(_progress);
        }
    }
    public void StartCraft()
    {
        _item = _itemStorage.Itens[0];
        if (_item != null && !_isCrafting)
        {
            try
            {
                if (_itens[_item.Id] != null)
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
        _isCrafting = true;
        GameManager.instance.playerLogic.canMove = false;
        HitStart();
        yield return new WaitForSeconds(0.5f);
        HitEnd();
        GameManager.instance.playerLogic.Craft();
        _isCrafting = false;
    }
}
