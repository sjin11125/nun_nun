using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
public class PlayerInfo : MonoBehaviour                 //플레이어 프로필 스크립트
{
    public static string Id;            //플레이어 아이디
    public static string NickName;      //플레이어 닉네임
    public static string SheetsNum;     //플레이어 건물 정보 들어있는 스프레드 시트 id
    public static string Info;          //상태메세지

    public Text[] Profile;      //닉넴, 상태메세지

    string[] Friends;       //친구 목록(닉네임)

    public GameObject NuniImages,Canvas;

    public Image ProfileImage;      //내 프로필 이미지
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Profile")
        {
            Profile[0].text = GameManager.NickName;
            Profile[1].text = GameManager.StateMessage;

            for (int i = 0; i < GameManager.CharacterList.Count; i++)
            {
                GameObject image = Instantiate(NuniImages, Canvas.transform);
                Image Nuniimage = image.GetComponent<Image>();
                Nuniimage.sprite = GameManager.CharacterList[i].Image;
            }
        }
       
    }

    public void ImageEnroll()       //프로필 이미지 등록
    {
        if (gameObject.GetComponent<Image>().sprite == null)
        {
            Debug.Log("이미지 널");
        }
        else
        {
            GameManager.ProfileImage = gameObject.transform.parent.GetComponent<Image>().sprite;
            Debug.Log("image: "+ GameManager.ProfileImage.name);
            WWWForm form1 = new WWWForm();
            form1.AddField("order", "setProfileImage");
            form1.AddField("player_nickname", GameManager.NickName);
            form1.AddField("profile_image", GameManager.ProfileImage.name);


            StartCoroutine(ImagePost(form1));                        //구글 스크립트로 초기화했는지 물어볼때까지 대기

        }

        //구글 스크립트에 업데이트
    }

    IEnumerator ImagePost(WWWForm form)
    {
        Debug.Log("ImagePost");
        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
            //Debug.Log(www.downloadHandler.text);
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag=="Profile")
        {
            gameObject.GetComponent<Image>().sprite = GameManager.ProfileImage;
        }
        if (gameObject.tag == "Profile_Image")
        {
            gameObject.GetComponent<Image>().sprite = GameManager.ProfileImage;
            /*  for (int i = 0; i < GameManager.CharacterList.Count; i++)
              {
                  if (GameManager.ProfileImage.name== GameManager.CharacterList[i].Image.name)
                  {
                      profile_image.sprite = GameManager.CharacterList[i].Image;
                  }
              }*/
        }
    }
}
