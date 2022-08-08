using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PersistentData
{
    private Dictionary<string, object> persistentHolder = new Dictionary<string, object>();
    private string filePath;

    /// <summary>
    /// Create an object to manage a save file.
    /// </summary>
    /// <param name="name">Name of the file</param>
    /// <param name="fileExt">Custom file extension that you want the file to have. Make sure to add . to it</param>
    public PersistentData(string name, string fileExt = ".baran")
    {
        filePath = Application.persistentDataPath + "/" + name + fileExt;
        Load();
        Debug.Log(filePath);
    }

    public PersistentData(string fullpath, bool inPersistent, string fileExt = ".baran")
    {
        if (inPersistent)
        {
            filePath = Application.persistentDataPath + "/" + fullpath + fileExt;
        }
        else
        {
            filePath = fullpath + fileExt;
        }
    }

    /// <summary>
    /// Read the data in the disk
    /// </summary>
    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        if (stream.Length != 0)
        {
            PersistentData data = formatter.Deserialize(stream) as PersistentData;
            persistentHolder = data.GetAll();
        }
        stream.Close();
    }

    /// <summary>
    /// Check if the object has a key stored
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckPool(string key)
    {
        try
        {
            return persistentHolder.ContainsKey(key);
        }
        catch
        {
            Debug.LogError(key + " Gave an error");
            return false;
        }
    }

    /// <summary>
    /// Add a key or overwrrite a key if it already exists
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddToPool(string key, object value)
    {
        persistentHolder[key] = value;
    }

    /// <summary>
    /// Will return the value stored in the object if it exists null if it does not.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object GetFromPool(string key)
    {
        return persistentHolder[key];
    }

    /// <summary>
    /// Remove an entry from the pool
    /// </summary>
    /// <param name="key"></param>
    public void RemoveAt(string key)
    {
        persistentHolder.Remove(key);
    }

    /// <summary>
    /// Write the data to the disk. You can find the file at Application.persistentDataPath
    /// </summary>
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, this);
        stream.Close();
    }

    /// <summary>
    /// You can use this to overwrite a save with another.
    /// </summary>
    /// <param name="obj">Another persistent data object</param>
    public void Save(PersistentData obj)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, obj);
        stream.Close();
    }

    /// <summary>
    /// Get everything stored
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, object> GetAll()
    {
        return persistentHolder;
    }

    /// <summary>
    /// Will delete everything inside the file
    /// </summary>
    public void WipeSave()
    {
        persistentHolder = new Dictionary<string, object>();
        Save();
    }
}