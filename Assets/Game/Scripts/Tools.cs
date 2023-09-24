using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField] float _power;
    [SerializeField] float _damage;
    [SerializeField] bool _isPickaxe;
    public void CraftToll(int idHead, int idBody)
    {

    }
    public float power
    {
        get { return _power; }
    }
    public float damage
    {
        get { return _damage; }
    }
    public bool isPickaxe
    {
        get { return _isPickaxe; }
    }
}
