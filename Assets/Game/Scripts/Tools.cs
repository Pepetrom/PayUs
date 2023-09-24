using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    //Pedir para o professor para tornar isso um dicionário
    [SerializeField] float _power;
    [SerializeField] float[] powers;
    [SerializeField] float[] damages;
    [SerializeField] float _damage;
    [SerializeField] bool _isPickaxe;
    public void CraftTool(int head, int body)
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
