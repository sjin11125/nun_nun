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
    void Start()
    {
        if (gameObject.tag == "Dialog")
        {
            nunis = GameObject.Find("nunis");
        }
    }
    public void Parse_character()                //¥©¥œ ¡§∫∏ ∫“∑Øø» 
    {
        List<Card> CharacterList = new List<Card>();
       
            csvData = Resources.Load<TextAsset>("Cha_Dialogue");    //csv∆ƒ¿œ ∞°¡Æø»
    
        

        string[] data = csvData.text.Split(new char[] { '\n' });    //ø£≈Õ ±‚¡ÿ¿∏∑Œ ¬…∞∑.
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

                    Debug.Log(pre_data[1]);
                    i++;
                    Debug.Log("i: " + i);
                }

            } while (data[i].Split(',')[0] == "");
            nuniDialog.Dialog = Dialog.ToArray();
            Debug.Log(nuniDialog.Nuni);
           
            
            
            GameManager.NuniDialog.Add(nuniDialog);



        }
        
        






    }
    void Update()
    {
       
        if (gameObject.tag!="Dialog")
        {
            if (GameManager.nuniDialogParse == false)
            {
                GameManager.nuniDialogParse = true;
                Parse_character();

                for (int j = 0; j < GameManager.NuniDialog.Count; j++)
                {
                    Debug.Log("Nunis: " + GameManager.NuniDialog[j].Nuni);
                }
            }
        }
        else
        {
            nunis = GameObject.Find("nunis");
            Transform[] Nunis = nunis.GetComponentsInChildren<Transform>();
            for (int i = 1; i < Nunis.Length; i++)
            {
                if (Nunis[i].name==nuni.cardImage+ "(Clone)")
                {
                    Debug.Log("Nunis[i].GetComponentsInChildren<Transform>()[5].gameObject:   "+ Nunis[i].GetComponentsInChildren<Transform>()[2].gameObject.transform.position);
                    MoveSpot = Nunis[i].GetComponentsInChildren<Transform>()[5].gameObject;


                    
                       RectTransform rect = gameObject.GetComponent<RectTransform>();
                    rect.transform.position = Camera.main.WorldToScreenPoint(MoveSpot.transform.position);
                    Invoke("Destroy_self",3f);

                }
            }
            
        }
        
    }

    void Destroy_self()
    {
        Destroy(gameObject);
    }
}
