using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadSystem 
{
    public static void Save(ISavable savableClass, SaveData data)
    {
        string savePath = Application.persistentDataPath + savableClass.pathOffset;
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(savePath, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static T Load<T>(ISavable savableClass) where T : SaveData
    {
        string loadPath = Application.persistentDataPath + savableClass.pathOffset;

        if (File.Exists(loadPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(loadPath, FileMode.Open);
            T data = formatter.Deserialize(stream) as T;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }        
    }
}
