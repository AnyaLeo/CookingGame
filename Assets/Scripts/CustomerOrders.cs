using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float maxTime;

    public Order currentOrder;

    public Text orderText;

    public AudioSource orderSound;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        // Subscribe to the event with a function that matches the delegate
        // aka it has the same return type and same input parameters!
        recipeCreator.OnRecipeCreated += FulfillOrder;

        gameManager = FindObjectOfType<GameManager>();
    }

    void FulfillOrder(string createdOrder)
    {
        if (createdOrder == currentOrder.order)
        {
            orderText.text = "";
            gameManager.AddMoney(currentOrder.price);

            // to fix a bug where we can keep forcing the customer to buy multiple versions of the order
            currentOrder.order = "None";
        }
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
                QueueOrder();
            }
            currentTime = 0;
        }
    }

    void QueueOrder()
    {
        string newOrder = GetRandomOrder();
        string ingredients = currentOrder.ingredients.ToString();
        orderText.text = $"Customer Order: {newOrder}\n{ingredients}";

        orderSound.Play();
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
