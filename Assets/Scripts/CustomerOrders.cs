using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Order
{
    public int price;
    public string order;
    public ThreeIngredients ingredients;

    public Order(int newPrice, string newOrder, ThreeIngredients newIngredients)
    {
        price = newPrice;
        order = newOrder;
        ingredients = newIngredients;
    } 
}

public class CustomerOrders : MonoBehaviour
{
    public RecipeCreator recipeCreator;

    private float currentTime;
    private float maxTime;

    public Order currentOrder;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        maxTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // in fixed update, you add Time.fixedDeltaTime instead
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            bool canOrder = recipeCreator.recipeBook.Count > 0;
            if (canOrder)
            {
                Debug.Log(GetRandomOrder());
            }
            currentTime = 0;
        }
    }

    string GetRandomOrder()
    {
        List<ThreeIngredients> keyList = new List<ThreeIngredients>(recipeCreator.recipeBook.Keys);

        int randomIndex = Random.Range(0, keyList.Count);
        ThreeIngredients randomKey = keyList[randomIndex];

        string randomOrder = recipeCreator.recipeBook[randomKey];

        currentOrder = new Order(10, randomOrder, randomKey);

        return randomOrder;
    }
}