using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCreator : MonoBehaviour
{
    public int numOfIngredientsRequired = 3;

    public List<GameObject> ingredients;

    public string[] adjectives;
    public string[] nouns;

    public Text dishText;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<GameObject>();

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

        // (Adjective) + (one of the ingredients on the board) + (noun for a food object)

        string randomAdjective = adjectives[Random.Range(0, adjectives.Length)];
        string randomIngredient = ingredients[Random.Range(0, ingredients.Count)].name;
        string randomNoun = nouns[Random.Range(0, nouns.Length)];

        string dishName = $"{randomAdjective} {randomIngredient} {randomNoun}";
        dishText.text = $"Congratulations, you created a \n{dishName}";
    }
}
