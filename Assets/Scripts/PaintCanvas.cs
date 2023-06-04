using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCanvas : MonoBehaviour
{
    [SerializeField] Vector2 CanvasSize = new Vector2(1000, 1000);
    MeshCollider CanvasCollider;

    [SerializeField] GameObject PaintableObjectParent;
    [SerializeField] GameObject PaintableSpotPrefab;

    [SerializeField] int WidthAmountOfPaintedSpots;
    [SerializeField] int HeightAmountOfPaintedSpots;

    List<GameObject> PaintableSpots = new List<GameObject>();

    int PaintedSpots = 0;
    int AmountOfPaintableSpots = 0;
    PaintLevel Level;

    private void Awake()
    {
        CanvasCollider = GetComponent<MeshCollider>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePaintableSpots();
    }

    private void Update()
    {
       
    }

    public void RemovePaintableSpot()
    {
        AmountOfPaintableSpots--;
    }

    private void CreatePaintableSpots()
    {
        Vector2 SpotSize = new Vector2((float)WidthAmountOfPaintedSpots/CanvasSize.x, (float)HeightAmountOfPaintedSpots/CanvasSize.y);
        PaintableSpot PaintSpot;
        int PaintSpotIndex = 0;

        for (int i = 0; i < HeightAmountOfPaintedSpots; i++)
        {
            for (int j = 0; j < WidthAmountOfPaintedSpots; j++)
            {
                GameObject NewSpot = Instantiate(PaintableSpotPrefab, PaintableObjectParent.transform);

                NewSpot.transform.localPosition =
                    new Vector3
                        (
                        -WidthAmountOfPaintedSpots*0.5f*SpotSize.x + SpotSize.x * 0.5f + SpotSize.x * j,
                         0, 
                         HeightAmountOfPaintedSpots * 0.5f *SpotSize.y- SpotSize.y * 0.5f - SpotSize.y * i
                        );

                PaintSpot = NewSpot.GetComponent<PaintableSpot>();
                PaintSpot.SetSpotSize(SpotSize);
                PaintSpot.SetCanvasInfo(this, PaintSpotIndex);
                PaintSpotIndex++;

                PaintableSpots.Add(NewSpot);
            }
        }

        AmountOfPaintableSpots = PaintSpotIndex;
    }

    public void UpdateCanvasInfo(int AmountToAdd)
    {
        PaintedSpots += AmountToAdd;
    }

    public float GetPercentageOfCanvasColoured()
    {
        return ((float) PaintedSpots / (float) AmountOfPaintableSpots) * 100.0f;
    }
}
