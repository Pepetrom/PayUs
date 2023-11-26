using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OrdenadorBotoes : MonoBehaviour
{
    public Button[] buttons;
    public Transform[] positions;
    public void Start()
    {
        /*positions = new Transform[buttons.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = buttons[i].transform;
            Debug.Log(positions[i].position);
        }*/
    }
    public void OrdenarCrescente()
    {
        List<Button> listaDeBotoes = new List<Button>(buttons);
        listaDeBotoes = listaDeBotoes.OrderBy(b => b.GetComponentInChildren<Text>().text).ToList();
        for (int i = 0; i < listaDeBotoes.Count; i++)
        {
            listaDeBotoes[i].transform.position = positions[i].position;
        }
    }

    public void OrdenarDecrescente()
    {
        List<Button> listaDeBotoes = new List<Button>(buttons);
        listaDeBotoes = listaDeBotoes.OrderBy(b => b.GetComponentInChildren<Text>().text).ToList();
        for (int i = listaDeBotoes.Count - 1, j = 0; i>=0; i--, j++)
        {
            listaDeBotoes[i].transform.position = positions[j].position;
        }
    }
}
