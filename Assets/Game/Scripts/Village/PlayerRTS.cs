using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum Places { Village, MineHouse, WoodHouse, FoodHouse, Mine, Forest }
public class PlayerRTS : MonoBehaviour
{
    public static PlayerRTS instance;
    Ray _ray;
    Camera _camera;
    RaycastHit _hit;
    Villager actualVillager, selectedVillager;
    public Vector3[] places = new Vector3[6];
    private void Awake()
    {
        _camera = Camera.main;
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Raycast())
            {
                //Select Villager
                if (_hit.collider.CompareTag("Villager"))
                {
                    if(selectedVillager != null)
                    {
                        selectedVillager.Deselect();
                    }
                    selectedVillager = _hit.collider.GetComponent<Villager>();
                    selectedVillager.Select();
                }               
                //Atribute Action
                else if (_hit.collider.CompareTag("Plant"))
                {
                    if (selectedVillager != null)
                    {
                        selectedVillager.Command(places[1],places[0],Type.Food);
                        selectedVillager = null;
                    }
                }
                else if (_hit.collider.CompareTag("Mine"))
                {
                    if (selectedVillager != null)
                    {
                        selectedVillager.Command(places[1],places[0], Type.Ore);
                        selectedVillager = null;
                    }
                }
                else if (_hit.collider.CompareTag("Chop"))
                {
                    if (selectedVillager != null)
                    {
                        selectedVillager.Command(places[1],places[0], Type.Wood);
                        selectedVillager = null;
                    }
                }
                else if (_hit.collider.CompareTag("Center"))
                {
                    if (selectedVillager != null)
                    {
                        selectedVillager.Command(places[0], places[0], Type.None);
                        selectedVillager = null;
                    }
                }
                else
                {
                    if (selectedVillager != null)
                    {
                        selectedVillager.Deselect();
                        selectedVillager = null;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Raycast())
        {
            if (_hit.collider.CompareTag("Villager"))
            {
              TryOutline();
            }
            else
            {
                if (actualVillager != null)
                {
                    actualVillager.HideOutline();
                    actualVillager = null;
                }
            }
        }
    }
    public void Select()
    {
        
    }
    public void TryOutline()
    {
        if(actualVillager == null)
        {
            actualVillager = _hit.collider.GetComponent<Villager>();
        }                 
        actualVillager.ShowOutline();
    }
    public bool Raycast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);
        if (_hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
