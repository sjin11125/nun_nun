using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeStorage : MonoBehaviour
{
    int shapeIndex;
    public List<ShapeData> shapeData;
    public List<Shape> shapeList;

    private Image spriteImage;
    private Image nextSquare;
    public Image exchangeSquare;

    public string shapeColor;
    public string shapeShape;
    public string[] shapeAchieveId;
    public int currentIndexSave;

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
            int firstIndex = UnityEngine.Random.Range(0, shapeData.Count);//첫번째 쉐이프 인덱스
            currentIndexSave = firstIndex;//nextExchangeItem가 일어날수있으므로 저장
            shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);//넥스트 쉐이프 인덱스
            shape.CreateShape(shapeData[firstIndex]);

            GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
            if (contectShape != null)
            {
                GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
                spriteImage = squareImage.GetComponent<Image>();
                squareImage.transform.GetChild(0).gameObject.GetComponent<Shining>().ShinActive();//빤짝이 켜기
            }
            GameObject contectNext = GameObject.FindGameObjectWithTag("NextSquare");
            if (contectNext != null)
            {
                nextSquare = contectNext.GetComponent<Image>();
            }
            spriteImage.sprite = shapeData[firstIndex].sprite;
            shapeColor = shapeData[firstIndex].color;
            shapeShape = shapeData[firstIndex].shape;//첫턴에 첫번째 쉐입 정보가 담겨있다
            shapeAchieveId = shapeData[firstIndex].AchieveId;

            nextSquare.sprite = shapeData[shapeIndex].sprite;
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

    private void RequestNewShapes()//두번째 턴부터 계속 여기서 실행됨
    {
        foreach (var shape in shapeList)
        {
            shapeColor = shapeData[shapeIndex].color;//start에서 받은 정보로 전달
            shapeShape = shapeData[shapeIndex].shape;
            shapeAchieveId = shapeData[shapeIndex].AchieveId;

            currentIndexSave = shapeIndex;
            shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);//실질적으로 넥스트 쉐입정보를 담고있음
            shape.RequestNewShape(shapeData[shapeIndex]);

            GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
            if (contectShape != null)
            {
                GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
                spriteImage = squareImage.GetComponent<Image>();
                squareImage.transform.GetChild(0).gameObject.GetComponent<Shining>().ShinActive();//빤짝이 켜기
            }
            GameObject contectNext = GameObject.FindGameObjectWithTag("NextSquare");
            if (contectNext != null)
            {
                nextSquare = contectNext.GetComponent<Image>();
            }
            spriteImage.sprite = nextSquare.sprite;
            nextSquare.sprite = shapeData[shapeIndex].sprite;
        }
    }

    public void nextExchangeItem()//현재와 이후를 서로 자리교체
    {
        shapeColor = shapeData[shapeIndex].color;//shapeIndex가 RequestNewShapes에서 이미 이후 정보를 담고있으니 실행
        shapeShape = shapeData[shapeIndex].shape;
        shapeAchieveId = shapeData[shapeIndex].AchieveId;

        exchangeSquare.sprite = spriteImage.sprite;//스프라이트 교체
        spriteImage.sprite = nextSquare.sprite;
        nextSquare.sprite = exchangeSquare.sprite;

        int exchangeIndex = currentIndexSave;//인덱스 교체
        currentIndexSave = shapeIndex;
        shapeIndex = exchangeIndex;
    }

    public void ReloadItem()
    {
        foreach (var shape in shapeList)
        {
            shapeColor = shapeData[shapeIndex].color;
            shapeShape = shapeData[shapeIndex].shape;
            shapeAchieveId = shapeData[shapeIndex].AchieveId;

            shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.RequestNewShape(shapeData[shapeIndex]);

            GameObject contectShape = GameObject.FindGameObjectWithTag("Shape");
            if (contectShape != null)
            {
                GameObject squareImage = contectShape.transform.GetChild(0).gameObject;
                spriteImage = squareImage.GetComponent<Image>();
                squareImage.transform.GetChild(0).gameObject.GetComponent<Shining>().ShinActive();//빤짝이 켜기
            }
            GameObject contectNext = GameObject.FindGameObjectWithTag("NextSquare");
            if (contectNext != null)
            {
                nextSquare = contectNext.GetComponent<Image>();
            }
            spriteImage.sprite = nextSquare.sprite;
            nextSquare.sprite = shapeData[shapeIndex].sprite;
        }
    }
}
