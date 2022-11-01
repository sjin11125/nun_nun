using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;

public class NunNunTool : EditorWindow
{
    [MenuItem("Tools/NunNun Setting")]
    // Start is called before the first frame update
    static void InitWindow()
    {
        NunNunTool w = (NunNunTool)EditorWindow.GetWindow(typeof(NunNunTool));
        w.Show();
    }
    private void OnGUI()
    {

        GUILayout.Label("\n¾À È­¸é ÀüÈ¯", EditorStyles.boldLabel);
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("·Î±×ÀÎ¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/Login.unity");
            }
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("¸ÞÀÎ¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
            }
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("°ÔÀÓ¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");

            }
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Ä£±¸¸¶À»¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/FriendMain.unity");
            }
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("»óÁ¡¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/Shop.unity");
            }
        }
        //GUILayout.Space( 1f );
        //using (new EditorGUILayout.HorizontalScope())
        //{
        //    GUILayout.Space( 3f );
        //    if (GUILayout.Button( "·Î±×ÀÎ" ))
        //    {
        //        if (EditorSceneManager.GetActiveScene().isDirty)
        //        {
        //            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        //        }
        //        EditorSceneManager.OpenScene( "Assets/Scenes/LoginScene.unity" );
        //    }
        //}
        /*GUILayout.Space(1f);
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Space(3f);
            if (GUILayout.Button("¹èÆ²¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/BattleScene.unity");
            }
            GUILayout.Space(3f);
            if (GUILayout.Button("´øÀü¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/DungeonScene.unity");
            }
            GUILayout.Space(3f);
            if (GUILayout.Button("ÀºÇà¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/DungeonSubScene/BankSubScene.unity");
            }
            GUILayout.Space(3f);
        }

        GUILayout.Space(1f);
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Space(3f);
            if (GUILayout.Button("È¥·É¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/DungeonSubScene/SoulRuinSubScene.unity");
            }
            GUILayout.Space(3f);
            if (GUILayout.Button("½ºÅ³¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/DungeonSubScene/SkillRuinSubScene.unity");
            }
            GUILayout.Space(3f);
            if (GUILayout.Button("¿ùµåº¸½º¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/WorldBossScene.unity");
            }
            GUILayout.Space(3f);
        }

        GUILayout.Space(1f);
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Space(3f);
            if (GUILayout.Button("Ä³½Ã»èÁ¦¾À"))
            {
                if (EditorSceneManager.GetActiveScene().isDirty)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                }
                EditorSceneManager.OpenScene("Assets/Scenes/TestScene.unity");
            }
        }

        GUILayout.Space(1f);
        */
      
    }
}
