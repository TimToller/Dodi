using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
   public static void saveDodiStats(FileManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath+"/data.dodi";

        FileStream stream = new FileStream(path, FileMode.Create);

        DodiData data = new DodiData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DodiData LoadData()
    {
        string path = Application.persistentDataPath + "/data.dodi";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DodiData data = formatter.Deserialize(stream) as DodiData;

            stream.Close();
            return data;

        }
        else
        {
            Debug.LogError("Dodi Data File not Found, "+path);
            return null;
        }
    }
}
