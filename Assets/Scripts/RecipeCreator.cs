using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeCreator : MonoBehaviour
{
    public List<GameObject> ingredients;

    public string[] adjectives;
    public string[] nouns; 

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
        print("Created food");

        // (Adjective) + (one of the ingredients on the board) + (noun for a food object)
    }
}
