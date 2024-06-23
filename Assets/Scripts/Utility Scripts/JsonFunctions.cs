using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class JsonFunctions
{
    public static void SaveScriptableObjects<T>(List<T> scriptableObjectList, string filePath) where T : ScriptableObject
    {
        // Convert ScriptableObject instances to JSON strings
        List<string> jsonList = new List<string>();
        foreach (T scriptableObject in scriptableObjectList)
        {
            jsonList.Add(JsonUtility.ToJson(scriptableObject));
        }

        // Combine the persistent data path with the file path
        string fullPath = Path.Combine(Application.persistentDataPath, filePath);

        Debug.Log($"Persistent Data Path: {Application.persistentDataPath}");
        Debug.Log($"Saving to: {fullPath}");

        // Write the JSON strings to the file
        File.WriteAllLines(fullPath, jsonList);

        Debug.Log($"Saved ScriptableObjects to: {fullPath}");
    }

    public static List<T> LoadScriptableObjects<T>(string filePath) where T : ScriptableObject
    {
        List<T> loadedList = new List<T>();

        // Combine the persistent data path with the file path
        string fullPath = Path.Combine(Application.persistentDataPath, filePath);

        // Check if the file exists
        if (File.Exists(fullPath))
        {
            string[] jsonLines = File.ReadAllLines(fullPath);

            foreach (string jsonLine in jsonLines)
            {
                T scriptableObject = ScriptableObject.CreateInstance<T>();
                JsonUtility.FromJsonOverwrite(jsonLine, scriptableObject);
                loadedList.Add(scriptableObject);
            }

        }
        else
        {
            Debug.LogWarning($"File not found: {fullPath}");
        }
        setSprites(loadedList);
        return loadedList;
    }

    private static void setSprites<T>(List<T> list) where T : ScriptableObject
    {
        foreach (var item in list)
        {
            if (ScriptableObjectContainsKey(item, "imagePath"))
            {
                FieldInfo field = item.GetType().GetField("imagePath", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    object value = field.GetValue(item);
                    string stringValue = value.ToString();
                    Texture2D texture = LoadTexture(stringValue);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    FieldInfo field2 = item.GetType().GetField("itemImage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field2 != null)
                    {
                        field2.SetValue(item, sprite);
                    }
                }
            }
        }
    }


    private static Texture2D LoadTexture(string path)
    {
        // Load the texture from the image file
        byte[] fileData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2); // Create new texture
        texture.LoadImage(fileData); // Load image byte data into the texture

        return texture;
    }

    private static bool ScriptableObjectContainsKey(ScriptableObject scriptableObject, string key)
    {
        SerializedObject serializedObject = new SerializedObject(scriptableObject);
        SerializedProperty property = serializedObject.FindProperty(key);
        return property != null;
    }
}
