using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public struct RecipeIngredients
{
    public string ingredient1;
    public string ingredient2;
    public string ingredient3;

    public RecipeIngredients(string ing1, string ing2, string ing3)
    {
        ingredient1 = ing1;
        ingredient2 = ing2;
        ingredient3 = ing3;
    }

    public override string ToString() =>
        $"1: {ingredient1}; 2: {ingredient2}; 3: {ingredient3};";
}

public class T_RecipeCreator : MonoBehaviour
{
    public int itemsRequiredForDish = 3;

    public Text dishName;

    public List<T_Food> ingredients;

    public Dictionary<RecipeIngredients, string> recipeBook;

    public string[] listOfAdjectives;
    private string[] listOfNouns;
    private void Start()
    {
        ingredients = new List<T_Food>();
        recipeBook = new Dictionary<RecipeIngredients, string>();

        TextAsset adjectives = Resources.Load<TextAsset>("adjectives");
        listOfAdjectives = adjectives.text.Split(',');

        TextAsset nouns = Resources.Load<TextAsset>("nouns");
        listOfNouns = nouns.text.Split(',');
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        T_Food foodScript;
        bool isIngredient = collision.gameObject.TryGetComponent(out foodScript);
        if (isIngredient)
        {
            ingredients.Add(foodScript);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        T_Food foodScript;
        bool isIngredient = collision.gameObject.TryGetComponent(out foodScript);
        if (isIngredient)
        {
            ingredients.Remove(foodScript);
        }
    }

    public void CreateFood()
    {
        bool notEnoughIngredients = ingredients.Count != itemsRequiredForDish;
        if (notEnoughIngredients)
        {
            Debug.LogWarning("Trying to create a dish with " + gameObject.transform.childCount + " number of ingredients, which is either not enough or too much.");
            return;
        }

       

        // add to the recipe book
        string[] ingredientsArray = new string[itemsRequiredForDish];
        for (int i = 0; i < itemsRequiredForDish; i++)
        {
            ingredientsArray[i] = ingredients[i].foodName;
        }

        // make sure you include "using System" up top
        Array.Sort(ingredientsArray);

        RecipeIngredients recipeIngredients = new RecipeIngredients(ingredientsArray[0], ingredientsArray[1], ingredientsArray[2]);
        print(recipeIngredients.ToString());


        bool recipeAlreadyExists = recipeBook.ContainsKey(recipeIngredients);
        string foodName;
        if (recipeAlreadyExists)
        {
            foodName = recipeBook[recipeIngredients];
        }
        else
        {
            // create a funny name for the dish
            int randomAdjective = UnityEngine.Random.Range(0, listOfAdjectives.Length);
            int randomIngredient = UnityEngine.Random.Range(0, ingredients.Count);
            int randomNoun = UnityEngine.Random.Range(0, listOfNouns.Length);

            foodName = listOfAdjectives[randomAdjective] + " " + ingredients[randomIngredient].name + " " + listOfNouns[randomNoun];

            // record it in the recipe book
            recipeBook.Add(recipeIngredients, foodName);
        }

        // Show the funny name to the user

        dishName.text = "Congratulations, you created a\n" + foodName;

        // delete all the ingredients involved
        int lastIndex = ingredients.Count - 1;
        for (int i = lastIndex; i >= 0; i--)
        {
            Destroy(ingredients[i].gameObject);
        }
    }
}
