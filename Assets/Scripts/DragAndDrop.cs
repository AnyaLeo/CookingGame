using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Collider2D col; 
    private Vector2 mousePos;

    private bool tryingToDrag;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) 
        {
            if (col == Physics2D.OverlapPoint(mousePos)) 
            {
                tryingToDrag = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            tryingToDrag = false;
        }
    }

    // Unity function that is called at specific time intervals (similar to Update)
    // used for physics stuff like collision detection, etc.
    void FixedUpdate() 
    {
        if (tryingToDrag) 
        {
            transform.position = mousePos;
        }
    }
}
