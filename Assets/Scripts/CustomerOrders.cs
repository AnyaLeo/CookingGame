using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrders : MonoBehaviour
{
    public RecipeCreator recipeCreator;

    private float currentTime;
    private float maxTime;

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
            Debug.Log("Timer went off!");
            currentTime = 0;
        }
        
    }
}
