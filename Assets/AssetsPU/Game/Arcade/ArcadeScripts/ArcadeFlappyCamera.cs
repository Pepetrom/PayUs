using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeFlappyCamera : MonoBehaviour
{
        public GameObject player;

        void Start()
        {
            player = GameObject.Find("Cube");
        }

        void Update()
        {
            transform.position = new Vector3(player.transform.position.x, 1.2f, player.transform.position.z - 10);
        }
}
