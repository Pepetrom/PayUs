using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private float _lifePoints = 3;
    [SerializeField] private GameObject _drop;
    [SerializeField] private int _oreLevel;
    [SerializeField] private bool _isGem;
    private int maxOreDrop = 1;
    public void takeDamage(float damage, float toolPower, bool isPickaxe)
    {
        if (isPickaxe && !_isGem)
        {
            Debug.Log("bateu com a picareta na pedra");
            _lifePoints -= damage * (toolPower / _oreLevel);
            maxOreDrop = (int)toolPower + 1;
        }
        else if (!isPickaxe && _isGem)
        {
            Debug.Log("bateu na gema com martelo");
            _lifePoints -= damage * (toolPower / _oreLevel);
            maxOreDrop = (int)toolPower + 1;
        }
        else
        {
            Debug.Log("ferramenta errada");
            _lifePoints -= damage / _oreLevel;
            maxOreDrop = 1;
        }
        if (_lifePoints <= 0)
        {
            for (int i = Random.Range(1, maxOreDrop+1); i > 0; i--)
            {

            }
            Instantiate(_drop, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
