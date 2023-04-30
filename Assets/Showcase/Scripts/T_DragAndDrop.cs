using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_DragAndDrop : MonoBehaviour
{
    private bool tryingToDrag;

    private Collider2D col;
    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        tryingToDrag = false;
    }

    public void SetTryingToDrag(bool newVal)
    {
        tryingToDrag = newVal;
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

    private void FixedUpdate()
    {
        if (tryingToDrag)
        {
            transform.position = mousePos;
        }
    }
}
