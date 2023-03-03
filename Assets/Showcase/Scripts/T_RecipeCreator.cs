using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class T_RecipeCreator : MonoBehaviour
{
    public int minItemsForDish = 2;
    public int maxItemsForDish = 6;

    public Text dishName;

    public List<GameObject> ingredients;

    public string[] listOfAdjectives;
    private string[] listOfNouns;
    private void Start()
    {
        ingredients = new List<GameObject>();

        TextAsset adjectives = Resources.Load<TextAsset>("adjectives");
        listOfAdjectives = adjectives.text.Split(',');

        TextAsset nouns = Resources.Load<TextAsset>("nouns");
        listOfNouns = nouns.text.Split(',');
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isIngredient = collision.gameObject.CompareTag("Ingredient");
        if (isIngredient)
        {
            ingredients.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isIngredient = collision.gameObject.CompareTag("Ingredient");
        if (isIngredient)
        {
            ingredients.Remove(collision.gameObject);
        }
    }

    public void CreateFood()
    {
        bool notEnoughIngredients = ingredients.Count <= minItemsForDish && ingredients.Count >= maxItemsForDish;
        if (notEnoughIngredients)
        {
            Debug.LogWarning("Trying to create a dish with " + gameObject.transform.childCount + " number of ingredients, which is either not enough or too much.");
            return;
        }

        // create a funny name for the dish
        int randomAdjective = Random.Range(0, listOfAdjectives.Length);
        int randomIngredient = Random.Range(0, ingredients.Count);
        int randomNoun = Random.Range(0, listOfNouns.Length);

        string foodName = listOfAdjectives[randomAdjective] + " " + ingredients[randomIngredient].name + " " + listOfNouns[randomNoun];

        dishName.text = "Congratulations, you created a\n" + foodName;

        // delete the ingredients and show the funny name to the user
    }
}
