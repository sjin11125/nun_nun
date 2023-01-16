using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FriendPlusUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button PlusBtn;
    public Text FriendName;
    public Text FriendMessage;
    public Image FriendImage;

    public Text PlusTxt;
    public Text AddTxt;
    public Text AddedTxt;


    void Start()
    {
        PlusBtn.OnClickAsObservable().Subscribe(_=>{
            FirebaseLogin.Instance.PlusFriend(gameObject.name).ContinueWith((task)=> {
                Debug.Log("task: "+ task.Result);
                UnityMainThreadDispatcher.Instance().Enqueue(() => {


                    if (task.Result=="fail")
                {
                    PlusTxt.gameObject.SetActive(false);
                    AddedTxt.gameObject.SetActive(true);
                }
                else
                {
                    PlusTxt.gameObject.SetActive(false);
                    AddTxt.gameObject.SetActive(true);
                }
                });
            });
        });
    }

    public void SetFriendInfo(FriendInfo friendInfo)
    {
        FriendName.text = friendInfo.FriendName;
        //FriendImage.sprite=GameManager.Instance.ima            //이미지 넣기
        FriendMessage.text = friendInfo.FriendMessage;
    }
}
