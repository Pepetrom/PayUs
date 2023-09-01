using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverLadoLado : MonoBehaviour
{
    float factor;
    bool side = true;

    void Update()
    {
        if (side)
        {
            if (factor < 5)
            {
                factor += Time.deltaTime;
                transform.Translate(1 * Time.deltaTime, 0, 0);
            }
            else
            {
                side = false;
            }
        }
        else
        {
            if (factor > 0)
            {
                factor -= Time.deltaTime;
                transform.Translate(-1 * Time.deltaTime, 0, 0);
            }
            else
            {
                side = true;
            }
        }
    }
}
