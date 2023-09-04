using UnityEngine;

public class CaveGenerator : MonoBehaviour
{
    [SerializeField] int quadradosCaverna = 23, random, sizeX, sizeY, tileID, x, y;
    [SerializeField] GameObject[] caveTiles;
    [SerializeField] float tileSize, CaveHeight;
    int[,] array;
    int limiar = 1;
    GameObject tileBeingWorkedOn;
    private void Start()
    {
        array = new int[sizeX, sizeY];
        CreateCaveArray();
        BuildCave();
    }
    private void BuildCave()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                tileBeingWorkedOn = Instantiate(caveTiles[array[i, j]], transform);
                tileBeingWorkedOn.transform.position = new Vector3(transform.position.x + i * tileSize * 1.55f, CaveHeight, transform.position.y + j * tileSize * 1.55f);             
            }
        }
    }
    private void CheckSurroundingTiles()
    {
    }
    private void CreateCaveArray()
    {
        //Check if cave is possible
        if (array.Length < quadradosCaverna)
        {
            quadradosCaverna = array.Length / 2;
        }

        //Make array zero
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = 0;
            }
        }

        //Create entrance
        x = Random.Range(0, array.GetLength(0));
        y = array.GetLength(1) - 1;
        array[y, x] = 2;

        //Draw Map in array
        for (int i = 0; i < quadradosCaverna; i++)
        {
            ChangeNumber(RandomBool(), RandomBool());
        }
    }
    void ChangeNumber(bool isX, bool positive)
    {
        if (isX)
        {
            if (positive) { x++; }
            else { x--; }
        }
        else
        {
            if (positive) { y++; }
            else { y--; }
        }
        x = Mathf.Clamp(x, 0, array.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, array.GetLength(1) - 1);

        if (array[y, x] == 2 || array[y, x] == 1)
        {
            if (isX)
            {
                if (x == array.GetLength(0) - 1)
                {
                    if (y == array.GetLength(1) - 1)
                    {
                        ChangeNumber(RandomBool(), false);
                    }
                    else if (y == 0)
                    {
                        if (RandomBool())
                        {
                            ChangeNumber(false, true);
                        }
                        else
                        {
                            ChangeNumber(true, false);
                        }
                    }
                    else
                    {
                        ChangeNumber(false, RandomBool());
                    }
                }
                else if (x == 0)
                {
                    if (y == array.GetLength(1) - 1)
                    {
                        if (RandomBool())
                        {
                            ChangeNumber(false, false);
                        }
                        else
                        {
                            ChangeNumber(true, true);
                        }
                    }
                    else if (y == 0)
                    {
                        ChangeNumber(RandomBool(), true);
                    }
                    else
                    {
                        ChangeNumber(false, RandomBool());
                    }
                }
                else
                {
                    ChangeNumber(RandomBool(), RandomBool());
                }
            }
            else
            {
                if (y == array.GetLength(1) - 1)
                {
                    if (x == array.GetLength(0) - 1)
                    {
                        ChangeNumber(RandomBool(), false);
                    }
                    else if (x == 0)
                    {
                        if (RandomBool())
                        {
                            ChangeNumber(true, true);
                        }
                        else
                        {
                            ChangeNumber(false, false);
                        }
                    }
                    else
                    {
                        ChangeNumber(true, RandomBool());
                    }
                }
                else if (y == 0)
                {
                    if (x == array.GetLength(0) - 1)
                    {
                        if (RandomBool())
                        {
                            ChangeNumber(false, true);
                        }
                        else
                        {
                            ChangeNumber(true, false);
                        }
                    }
                    else if (x == 0)
                    {
                        ChangeNumber(RandomBool(), true);
                    }
                    else
                    {
                        ChangeNumber(true, RandomBool());
                    }
                }
                else
                {
                    ChangeNumber(RandomBool(), RandomBool());
                }
            }
        }
        else
        {
            array[y, x] = 1;
        }
        limiar++;
    }

    bool RandomBool()
    {
        random = Random.Range(0, 2);
        Debug.Log(random);
        switch (random)
        {
            case 0:
                return true;
            case 1:
                return false;
            default: return false;
        }
    }

}
