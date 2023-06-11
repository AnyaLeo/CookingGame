using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int startMoneyAmount = 50;
    private int currentMoney;
    public Text moneyText;

    private void Start()
    {
        currentMoney = startMoneyAmount;
        moneyText.text = currentMoney.ToString();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        moneyText.text = currentMoney.ToString();
    }

    public void SpawnFood(GameObject prefab)
    {
        GameObject newFood = Instantiate(prefab);
        newFood.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        DragAndDrop newFoodScript = newFood.GetComponent<DragAndDrop>();
        newFoodScript.SetTryingToDrag(true);
    }
}
