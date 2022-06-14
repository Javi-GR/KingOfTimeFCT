using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
    private static string path = Application.persistentDataPath + "/playerposition.woojajjjxd";
    private static string path1 = Application.persistentDataPath + "/keydata.woojajjjxd";
    public static void SavePlayer(PlayerMovement player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in "+ path);
            return null;
        }
    }
    public static void SaveKeyPosition(KeyLeft keyLeft)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path1, FileMode.Create);

        KeysData data = new KeysData(keyLeft);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static KeysData LoadKeyPosition()
    {
         if(File.Exists(path1))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path1, FileMode.Open);

            KeysData data = formatter.Deserialize(stream) as KeysData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in "+ path);
            return null;
        }
    }

    
}
