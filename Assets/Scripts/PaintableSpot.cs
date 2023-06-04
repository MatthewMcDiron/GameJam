using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableSpot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider boxCollider;
    PaintCanvas paintCanvas;
    int PaintCanvasIndex;
    bool SpotRemoved = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SetSpotSize(Vector2 SpotSize)
    {
        gameObject.transform.localScale = 
            new Vector3
            (SpotSize.x/boxCollider.size.x, 
            SpotSize.x / boxCollider.size.x, 
            SpotSize.y / boxCollider.size.z
            );
       // spriteRenderer = SpotSize;
       // boxCollider = SpotSize;
    }

    public void SetCanvasInfo(PaintCanvas _canvas, int _index)
    {
        paintCanvas = _canvas;
        PaintCanvasIndex = _index;
    }

    public int GetCanvasIndex()
    {
        return PaintCanvasIndex;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Environment" && !SpotRemoved)
        {
            SpotRemoved = true;
            paintCanvas.RemovePaintableSpot();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spriteRenderer.color = Color.red;
            paintCanvas.UpdateCanvasInfo(this);
        }
    }
}
