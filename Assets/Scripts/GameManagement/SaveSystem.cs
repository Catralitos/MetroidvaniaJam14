using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayer(PlayerEntity playerEntity)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.steve";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerEntity);
        
        formatter.Serialize(stream, data);
        
        stream.Close();
        
        Debug.Log("Save file created at " + path);
    }
    
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.steve";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }
    
    public static void DeletePlayer()
    {
        string path = Application.persistentDataPath + "/player.steve";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
