using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private GameObject[] _itens;
    //[SerializeField] private int[] _ids;
    [SerializeField] private GameObject _keySignalText, _effect;
    private PickableIten _item;
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
            _keySignalText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _keySignalText.SetActive(false);
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
            Instantiate(_itens[_item.id], _itemPlace.transform.position,_itemPlace.transform.rotation);
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
        if (_item != null)
        {
            if (_item.id != 0 && !_isCrafting)
            {
                if (_item.id < _itens.Length)
                {
                    if (_itens[_item.id] != null)
                    {
                        StartCoroutine(Crafting());
                    }
                }
            }
        }
    }
    private IEnumerator Crafting()
    {
        //GameManager.instance.playerLogic.transform.position = _place.transform.position;
        _isCrafting = true;
        GameManager.instance.playerMovement.canMove = false;
        GameManager.instance.playerLogic.canMove = false;
        GameManager.instance.playerLogic.Craft();
        //GameManager.instance.playerLogic.transform.LookAt( _place.transform.position );
        HitStart();
        yield return new WaitForSeconds(0.5f);
        HitEnd();
        GameManager.instance.playerMovement.canMove = true;
        GameManager.instance.playerLogic.canMove = true;
        _isCrafting = false;
    }
}
