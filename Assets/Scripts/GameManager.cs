using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SpawnFood(GameObject prefab)
    {
        GameObject newFood = Instantiate(prefab);
        newFood.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        DragAndDrop newFoodScript = newFood.GetComponent<DragAndDrop>();
        newFoodScript.SetTryingToDrag(true);
    }
}
