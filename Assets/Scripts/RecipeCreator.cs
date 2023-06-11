using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public struct ThreeIngredients
{
    public string ingredient1;
    public string ingredient2;
    public string ingredient3;

    public ThreeIngredients(string ing1, string ing2, string ing3)
    {
        ingredient1 = ing1;
        ingredient2 = ing2;
        ingredient3 = ing3;
    }

    public override string ToString()
    {
        return $"Ingredients: {ingredient1}, {ingredient2}, {ingredient3}";
    }
}

public class RecipeCreator : MonoBehaviour
{
    public int numOfIngredientsRequired = 3;

    public List<Food> ingredients;

    public string[] adjectives;
    public string[] nouns;

    public Dictionary<ThreeIngredients, string> recipeBook;
    public Dictionary<string, int> foodPrices;

    public Text dishText;

    // Creating an event that broadcasts when we created a recipe
    public delegate void RecipeCreated(string recipeName);
    public event RecipeCreated OnRecipeCreated;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<Food>();
        recipeBook = new Dictionary<ThreeIngredients, string>();
        foodPrices = new Dictionary<string, int>();

        // This is how we load text files from the Resources folder in Unity
        TextAsset textAdjectives = Resources.Load<TextAsset>("adjectives");
        adjectives = textAdjectives.text.Split(',');

        TextAsset textNouns = Resources.Load<TextAsset>("nouns");
        nouns = textNouns.text.Split(',');
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food foodScript;
        bool isFood = collision.gameObject.TryGetComponent(out foodScript);

        if (isFood)
        {
            ingredients.Add(foodScript);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Food foodScript;
        bool isFood = collision.gameObject.TryGetComponent(out foodScript);

        if (isFood)
        {
            ingredients.Remove(foodScript);
        }
    }

    public void CreateFood()
    {
        bool notEnoughIngredients = (ingredients.Count != numOfIngredientsRequired);
        if (notEnoughIngredients)
        {
            return;
        }

        string[] ingredientsArray = new string[numOfIngredientsRequired];
        for (int i = 0; i < numOfIngredientsRequired; i++)
        {
            ingredientsArray[i] = ingredients[i].foodName;
        }

        // need using System to access Array.Sort
        Array.Sort(ingredientsArray);

        ThreeIngredients recipeIngredients = new ThreeIngredients(ingredientsArray[0], ingredientsArray[1], ingredientsArray[2]);

        bool alreadyHaveTheRecipe = recipeBook.ContainsKey(recipeIngredients);

        string dishName = "";

        if (alreadyHaveTheRecipe)
        {
            dishName = recipeBook[recipeIngredients];
        }
        else
        {
            // (Adjective) + (one of the ingredients on the board) + (noun for a food object)

            string randomAdjective = adjectives[UnityEngine.Random.Range(0, adjectives.Length)];
            string randomIngredient = ingredients[UnityEngine.Random.Range(0, ingredients.Count)].foodName;
            string randomNoun = nouns[UnityEngine.Random.Range(0, nouns.Length)];

            dishName = $"{randomAdjective} {randomIngredient} {randomNoun}";

            // Store the new recipe in a recipe book

            recipeBook.Add(recipeIngredients, dishName);

            // Record the price of foods required for this recipe
            for (int i = 0; i < ingredients.Count; i++)
            {
                bool isPriceRecorded = foodPrices.ContainsKey(ingredients[i].foodName);
                if (!isPriceRecorded)
                {
                    foodPrices.Add(ingredients[i].foodName, ingredients[i].price);
                }
            }
        }

        dishText.text = $"Congratulations, you created a \n{dishName}";

        // Consume the ingredients we used to make the food
        int lastIndex = ingredients.Count - 1;
        for (int i = lastIndex; i >= 0; i--)
        {
            Destroy(ingredients[i].gameObject);
        }

        // Broadcasting our event to whoever is listening
        // First, make sure we have actual listeners who care that this event happened
        if (OnRecipeCreated != null)
        {
            // Second, let them know the event happened!
            // the input parameters will be the same as whatever you put into your delegate
            OnRecipeCreated.Invoke(dishName);
        }
    }
}
