using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapeList;

    private Image spriteImage;
    private Image nextSquare;
    public Image exchangeSquare;

   // [HideInInspector]
    public string shapeColor;
    public string shapeShape;

    private void OnEnable()
    {
        GameEvents.RequestNewShapes += RequestNewShapes;
    }

    private void OnDisable()
    {
        GameEvents.RequestNewShapes -= RequestNewShapes;
    }

    void Start()
    {
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            var nextIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.CreateShape(shapeData[shapeIndex]);

            GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
            if (contectShape != null)
            {
                GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
                spriteImage = squareImage.GetComponent<Image>();
            }
            GameObject contectNext = GameObject.FindGameObjectWithTag("NextSquare");
            if (contectNext != null)
            {
                nextSquare = contectNext.GetComponent<Image>();
            }
            spriteImage.sprite = shapeData[shapeIndex].sprite;
            shapeColor = shapeData[shapeIndex].color;
            shapeShape = shapeData[shapeIndex].shape;
            Debug.Log(shapeColor);
            nextSquare.sprite = shapeData[nextIndex].sprite;
            shapeColor = shapeData[nextIndex].color;
            shapeShape = shapeData[nextIndex].shape;          
        }
    }

    public Shape GetCurrentSelectedShape()//그리드스크립트에서 사용중
    {
        foreach (var shape in shapeList)
        {
            if (shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive())
                return shape;//움직이고있는 쉐이프 전달
        }
        Debug.LogError("There is no shape selected");
        return null;
    }

    private void RequestNewShapes()
    {
        foreach (var shape in shapeList)
        {
            Debug.Log(shapeColor);
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.RequestNewShape(shapeData[shapeIndex]);

            var nextShapeColor = shapeData[shapeIndex].color;
            var nextShapeShape = shapeData[shapeIndex].shape;

            GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
            if (contectShape != null)
            {
                GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
                spriteImage = squareImage.GetComponent<Image>();
            }
            GameObject contectNext = GameObject.FindGameObjectWithTag("NextSquare");
            if (contectNext != null)
            {
                nextSquare = contectNext.GetComponent<Image>();
            }
            spriteImage.sprite = nextSquare.sprite;
            shapeColor = nextShapeColor;
            shapeShape = nextShapeShape;
            nextSquare.sprite = shapeData[shapeIndex].sprite;         
        }
    }

    public void nextExchangeItem()
    {
        exchangeSquare.sprite = spriteImage.sprite;
        spriteImage.sprite= nextSquare.sprite;
        nextSquare.sprite = exchangeSquare.sprite;
    }

    public void ReloadItem()
    {
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.RequestNewShape(shapeData[shapeIndex]);

            GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
            if (contectShape != null)
            {
                GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
                spriteImage = squareImage.GetComponent<Image>();
            }
            GameObject contectNext = GameObject.FindGameObjectWithTag("NextSquare");
            if (contectNext != null)
            {
                nextSquare = contectNext.GetComponent<Image>();
            }
            spriteImage.sprite = nextSquare.sprite;
            nextSquare.sprite = shapeData[shapeIndex].sprite;
            shapeColor = shapeData[shapeIndex].color;
            shapeShape = shapeData[shapeIndex].shape;
        }
    }
}
