using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SpawnFood(GameObject prefab)
    {
        GameObject newFood = Instantiate(prefab);
    }
}
