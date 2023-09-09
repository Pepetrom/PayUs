using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField] float _power;
    [SerializeField] float _damage;
    [SerializeField] float _durability;
    [SerializeField] bool _isPickaxe;
    public float power
    {
        get { return _power; }
    }
    public float damage
    {
        get { return _damage; }
    }
    public float durability
    {
        get { return _durability; }
    }
    public bool isPickaxe
    {
        get { return _isPickaxe; }
    }
}
