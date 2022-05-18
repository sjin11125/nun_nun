using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuniDialog : MonoBehaviour
{
   public  string Nuni = "";
   public string[] Dialog;
}
public class NuniDialogParsing : MonoBehaviour
{
    public static Character[] Mal;
    TextAsset csvData;

    public GameObject MoveSpot;
    public Card nuni;
    GameObject nunis;
    Vector2 ve;

    public bool isMove;
    public GameObject nuniObject;

    void Start()
    {
        if (gameObject.tag == "Dialog")
        {
            nunis = GameObject.Find("nunis");
        }
    }
    public void Parse_character()                //누니 정보 불러옴 
    {
        List<Card> CharacterList = new List<Card>();

        csvData = Resources.Load<TextAsset>("Cha_Dialogue");    //csv파일 가져옴



        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.
        Debug.Log("data.Length" + data.Length);


        for (int i = 1; i < data.Length;)
        {

            NuniDialog nuniDialog = new NuniDialog();
            string[] pro_data = data[i].Split(',');
            nuniDialog.Nuni = pro_data[0];
            List<string> Dialog = new List<string>();
            string[] pre_data;

            do
            {
                if (i >= data.Length - 1 || data[i].Split(',')[0] == "end")
                {
                    return;
                }
                else
                {
                    pre_data = data[i].Split(',');
                    Dialog.Add(pre_data[1]);

                    i++;
                }

            } while (data[i].Split(',')[0] == "");
            nuniDialog.Dialog = Dialog.ToArray();



            GameManager.NuniDialog.Add(nuniDialog);



        }








    }
    void Update()
    {

        if (gameObject.tag != "Dialog")
        {
            if (GameManager.nuniDialogParse == false)
            {
                GameManager.nuniDialogParse = true;
                Parse_character();


            }
        }
        else
        {
            if (isMove)
            {
                
                Nuni_movespot(nuniObject);



            }

        }
      
    }
    public void Nuni_movespot(GameObject nuniObject)
    {
        nunis = GameObject.Find("nunis");
        Transform[] Nunis = nunis.GetComponentsInChildren<Transform>();
        for (int i = 1; i < Nunis.Length; i++)
        {
            if (Nunis[i].gameObject== nuniObject)
            {
                Debug.Log("Nunis[i].GetComponentsInChildren<Transform>()[5].gameObject:   " + Nunis[i].GetComponentsInChildren<Transform>()[2].gameObject.transform.position);
                MoveSpot = Nunis[i].gameObject.GetComponentsInChildren<Transform>()[5].gameObject;



                RectTransform rect = gameObject.GetComponent<RectTransform>();
                rect.transform.position = Camera.main.WorldToScreenPoint(MoveSpot.transform.position);
                Invoke("Destroy_self", 3f);
                
            }
            else
            {
                Debug.Log("다르다");
            }
        }
    }
    void Destroy_self()
    {
        isMove = false;
        Destroy(gameObject);
    }
}
