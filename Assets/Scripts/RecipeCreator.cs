using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeCreator : MonoBehaviour
{
    public List<GameObject> ingredients;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ingredients.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ingredients.Remove(collision.gameObject);
    }
}
