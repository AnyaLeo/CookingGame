using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public List<GameObject> ingredients;

    public string[] adjectives;
    public string[] nouns;

    public Dictionary<ThreeIngredients, string> recipeBook;

    public Text dishText;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<GameObject>();
        recipeBook = new Dictionary<ThreeIngredients, string>();

        // This is how we load text files from the Resources folder in Unity
        TextAsset textAdjectives = Resources.Load<TextAsset>("adjectives");
        adjectives = textAdjectives.text.Split(',');

        TextAsset textNouns = Resources.Load<TextAsset>("nouns");
        nouns = textNouns.text.Split(',');
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ingredients.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ingredients.Remove(collision.gameObject);
    }

    public void CreateFood()
    {
        bool notEnoughIngredients = (ingredients.Count != numOfIngredientsRequired);
        if (notEnoughIngredients)
        {
            return;
        }

        ThreeIngredients recipeIngredients = new ThreeIngredients(ingredients[0].name, ingredients[1].name, ingredients[2].name);
        bool alreadyHaveTheRecipe = recipeBook.ContainsKey(recipeIngredients);
        string dishName = "";

        if (alreadyHaveTheRecipe)
        {
            dishName = recipeBook[recipeIngredients];
        }
        else
        {
            // (Adjective) + (one of the ingredients on the board) + (noun for a food object)

            string randomAdjective = adjectives[Random.Range(0, adjectives.Length)];
            string randomIngredient = ingredients[Random.Range(0, ingredients.Count)].name;
            string randomNoun = nouns[Random.Range(0, nouns.Length)];

            dishName = $"{randomAdjective} {randomIngredient} {randomNoun}";

            // Store the new recipe in a recipe book

            recipeBook.Add(recipeIngredients, dishName);

            print($"ADDED A RECIPE: {dishName}");
            print(recipeIngredients.ToString());
        }

        dishText.text = $"Congratulations, you created a \n{dishName}";
    }
}
