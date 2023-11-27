using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeInimigoF3 : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float t;

    public void Start()
    {
        t = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - t >= 5.0f)
        {
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        }
    }
}
