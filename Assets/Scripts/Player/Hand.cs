using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public void CollisionMoment()
    {
        GameManager.instance.playerLogic.HitObject();
    }
    public void EndAnimation()
    {
        GameManager.instance.playerLogic.canHit = true;
    }
}
