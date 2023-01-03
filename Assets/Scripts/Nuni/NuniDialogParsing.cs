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
        if (gameObject.tag .Equals( "Dialog"))
        {
            nunis = GameObject.Find("nunis");
        }
    }
    public void Parse_character()                //¥©¥œ ¡§∫∏ ∫“∑Øø» 
    {
        List<Card> CharacterList = new List<Card>();

        csvData = Resources.Load<TextAsset>("GameData/Cha_Dialogue");    //csv∆ƒ¿œ ∞°¡Æø»



        string[] data = csvData.text.Split(new char[] { '\n' });    //ø£≈Õ ±‚¡ÿ¿∏∑Œ ¬…∞∑.



        for (int i = 1; i < data.Length;)
        {

            NuniDialog nuniDialog = new NuniDialog();
            string[] pro_data = data[i].Split(',');
            nuniDialog.Nuni = pro_data[0];
            List<string> Dialog = new List<string>();
            string[] pre_data;

            do
            {
                if (i >= data.Length - 1 || data[i].Split(',')[0] .Equals( "end"))
                {
                    return;
                }
                else
                {
                    pre_data = data[i].Split(',');
                    Dialog.Add(pre_data[1]);

                    i++;
                }

            } while (data[i].Split(',')[0] .Equals(""));
            nuniDialog.Dialog = Dialog.ToArray();



            GameManager.NuniDialog.Add(nuniDialog);



        }








    }
    void Update()
    {

        if (gameObject.tag != "Dialog")
        {
            if (GameManager.nuniDialogParse .Equals( false))
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
            if (Nunis[i].gameObject.Equals( nuniObject))
            {
                MoveSpot = Nunis[i].gameObject.GetComponentsInChildren<Transform>()[5].gameObject;



                RectTransform rect = gameObject.GetComponent<RectTransform>();
                rect.transform.position = MoveSpot.transform.position;
               // Invoke(nameof(Destroy_self), 3f);
                
            }
        }
    }
    void Destroy_self()
    {
        isMove = false;
        Destroy(gameObject);
    }
}
