using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuniDialogParsing : MonoBehaviour
{
    public static Character[] Mal;
    TextAsset csvData;
    void Start()
    {
        if (GameManager.nuniDialogParse == false)
        {
            GameManager.nuniDialogParse = true;
        }
    }
    public Card[] Parse_character(int index)                //누니 정보 불러옴 
    {
        List<Card> CharacterList = new List<Card>();
        if (index == 1)
        {
            csvData = Resources.Load<TextAsset>("Cha_Dialogue");    //csv파일 가져옴

        }
        string[] data = csvData.text.Split(new char[] { '\n' });    //엔터 기준으로 쪼갬.
        //string[] pro_data;
        Debug.Log(data.Length);


        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] pro_data = data[i].Split(',');

            if (pro_data[0] == "end")
            {
                break;
            }
            Card character = new Card(pro_data[1], pro_data[2], pro_data[3], pro_data[4], pro_data[5], pro_data[6],
                                       pro_data[7], pro_data[8], pro_data[9], pro_data[10], pro_data[11], pro_data[12]);
            //잠금    /   이름  /  아이템 /   이미지 /  가격  /  레벨  /  별   /  게이지 /  설명  / 보유효과  / 건물  / 골드 획득량
            character.SetChaImage(GameManager.CharacterImageData[pro_data[4]]);
            CharacterList.Add(character);
        }

        for (int i = 0; i < CharacterList.Count; i++)
        {
            string ImageName = CharacterList[i].cardImage;
            if (GameManager.CharacterImageData.ContainsKey(ImageName))
            {
                CharacterList[i].SetChaImage(GameManager.CharacterImageData[ImageName]);     //누니 이미지 넣기     

            }
        }
        return CharacterList.ToArray();

    }
}
