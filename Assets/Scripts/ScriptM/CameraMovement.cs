using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{ // 여기 일단 스킵
    public float zoomOutMin = 1;
    public float zoomOutMax = 5; // 이거 5로해야 안튀어나감
    public static bool isTouch = false;
    //터치가 되지 않을까 해서 적어보는 코드
   // Vector3 touchStart;

    [SerializeField]
    private Camera cam; // 카메라 생성

    /*[SerializeField]
    private float zoomStep, minCamSize, maxCamSize;
    */

    private Vector3 dragOrigin;

    //0114 시작
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.2f;
  

    //[SerializeField]
    //Text text;


    

    [SerializeField]
    private SpriteRenderer mapRenderer; // 패널생성
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    //

    private void Awake() // 카메라 최대, 최소 결정
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }



    // Update is called once per frame
    void Update()
    {
       
       if (GridBuildingSystem.IsPointerOverGameObject())      //UI를 클릭했냐
            isTouch = true;
     
        //업데이트에 줌을 적어보자     if (GameManager.isMoveLock==false)

        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                cam.orthographicSize += zoomModifier;
            if (touchesPrevPosDifference < touchesCurPosDifference)
                cam.orthographicSize -= zoomModifier;

        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
        }
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 2f, 8f); // 축소, 확대 배율

        if (isTouch == false||GridBuildingSystem.isEditing.Value)       // UI를 클릭안했거나 건축모드일 때 이동가능
        {

            PanCamera();
        }

        //touch code



        // zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    private void PanCamera() //이게 이동
    {

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            //print("origin" + dragOrigin + "newPoisition" + cam.ScreenToWorldPoint(Input.mousePosition) + "=difference" + difference);

            cam.transform.position =new Vector3 ( ClampCamera(cam.transform.position + difference).x,
                ClampCamera(cam.transform.position + difference).y,
                -10f);








        }
    }

    // 줌아웃을 할때 판넬보다 크면은 판넬 크기에 고정되어야함.  
    
}