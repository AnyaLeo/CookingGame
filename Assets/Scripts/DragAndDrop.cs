using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Collider2D col; 
    private Vector2 mousePos;

    private bool tryingToDrag;

    void OnEnable()
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

        if (tryingToDrag)
        {
            transform.position = mousePos;
        }
    }

    public void SetTryingToDrag(bool newValue)
    {
        tryingToDrag = newValue;
    }
}
