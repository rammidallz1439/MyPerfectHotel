using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SaveData<T>(T content, string fileName)
    {
        string directoryPath = Application.persistentDataPath + "Demo";
        string filePath = Path.Combine(directoryPath, fileName);

        // Create directory if it doesn't exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fileStream, content);
        }
    }
    public T Load<T>(string fileName)
    {
        T data = default(T);
        string dathPath = Application.persistentDataPath + "Demo";
        string filePath = Path.Combine(dathPath, fileName);
        if (Directory.Exists(dathPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                FileStream fileStream = File.Open(filePath, FileMode.Open);
                data = (T)formatter.Deserialize(fileStream);
                fileStream.Close();
            }
    


        }
        else
        {
            Directory.CreateDirectory(dathPath);
            Debug.LogWarning("Save file not found. Using default values.");
        }
        return data;
    }

    public  T LoadJsonData<T>(string fileName) where T : class
    {
        string filePath = "Assets/Resources/" + fileName;
        

        if (filePath != null)
        { 
            
            string jsonText = System.IO.File.ReadAllText(filePath);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonText);
            return data;
        }
        else
        {
            Debug.LogWarning("Failed to load JSON file: " + fileName);
            return null;
        }
    }

}
