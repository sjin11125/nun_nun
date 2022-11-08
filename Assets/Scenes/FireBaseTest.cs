using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
public class FireBaseTest : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Query docRef = db.Collection("user").Document("vicky").Collection("building");
        Dictionary<string, object> user = new Dictionary<string, object>
{
        { "First", "Ada" },
        { "Last", "gfdgh" },
        { "Born", 2000 },
};
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task=>
        {
            QuerySnapshot all = task.Result;
            foreach (DocumentSnapshot item in all.Documents)
            {
                Dictionary<string, object> building = item.ToDictionary();
                Debug.Log(item.ToString());
                foreach (KeyValuePair<string,object> items in building)
                {
                    Debug.Log(items.Key+ "    " + items.Value);
                }
            }
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
