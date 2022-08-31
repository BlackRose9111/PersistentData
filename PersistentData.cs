using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PersistentData
{
    private Dictionary<string, object> persistentHolder = new Dictionary<string, object>();
    private Dictionary<string, bool> persistentBoolHolder = new Dictionary<string, bool>();
    private Dictionary<string, int> persistentIntHolder = new Dictionary<string, int>();
    private Dictionary<string, string> persistentStringHolder = new Dictionary<string, string>();
    private Dictionary<string, float> persistentFloatHolder = new Dictionary<string, float>();
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
            persistentHolder = data.GetAllObjects();
            persistentFloatHolder = data.GetAllFloats();
            persistentIntHolder = data.GetAllInts();
            persistentBoolHolder = data.GetAllBooleans();
            persistentStringHolder = data.GetAllStrings();
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
    /// Add a serializable object to the object pool
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddObjectToPool(string key, object value)
    {
        persistentHolder.Add(key, value);
    }

    /// <summary>
    /// Add an int to the int pool
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddIntToPool(string key, int value)
    {
        persistentIntHolder.Add(key, value);
    }

    /// <summary>
    /// Add a string to the string pool
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddStringToPool(string key, string value)
    {
        persistentStringHolder.Add(key, value);
    }

    /// <summary>
    /// Add a bool to the bool pool
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddBoolToPool(string key, bool value)
    {
        persistentBoolHolder.Add(key, value);
    }

    /// <summary>
    /// Add a float to the float pool
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddFloatToPool(string key, float value)
    {
        persistentFloatHolder.Add(key, value);
    }

    /// <summary>
    /// Get an object from the object pool or the default value if it does not exist
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public object GetObjectFromPool(string key, object defaultValue = null)
    {
        return persistentHolder.GetValueOrDefault(key, defaultValue);
    }

    /// <summary>
    /// Get an int from the int pool or the default value if it does not exist
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public int GetIntFromPool(string key, int defaultValue = 0)
    {
        return persistentIntHolder.GetValueOrDefault(key, defaultValue);
    }

    /// <summary>
    /// Get a string from the string pool or the default value if it does not exist
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public string GetStringFromPool(string key, string defaultValue = null)
    {
        return persistentStringHolder.GetValueOrDefault(key, defaultValue);
    }

    /// <summary>
    /// Get a bool from the bool pool or a default value if it does not exist
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public bool GetBoolFromPool(string key, bool defaultValue = false)
    {
        return persistentBoolHolder.GetValueOrDefault(key, defaultValue);
    }

    /// <summary>
    /// Get a float from the float pool or a default value if it does not exist
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public float GetFloatFromPool(string key, float defaultValue = 0f)
    {
        return persistentFloatHolder.GetValueOrDefault(key, defaultValue);
    }

    /// <summary>
    /// Remove an object from the object pool
    /// </summary>
    /// <param name="key"></param>
    /// <returns>false if the object does not exsit</returns>
    public bool RemoveObjectAt(string key)
    {
        try
        {
            persistentHolder.Remove(key);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Remove an int from the int pool
    /// </summary>
    /// <param name="key"></param>
    /// <returns>false if the object does not exsit</returns>
    public bool RemoveIntAt(string key)
    {
        try
        {
            persistentIntHolder.Remove(key);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Remove an string from the string pool
    /// </summary>
    /// <param name="key"></param>
    /// <returns>false if the object does not exsit</returns>
    public bool RemoveStringAt(string key)
    {
        try
        {
            persistentStringHolder.Remove(key);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Remove a bool from the bool pool
    /// </summary>
    /// <param name="key"></param>
    /// <returns>false if the object does not exsit</returns>
    public bool RemoveBoolAt(string key)
    {
        try
        {
            persistentBoolHolder.Remove(key);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Remove an float from the float pool
    /// </summary>
    /// <param name="key"></param>
    /// <returns>false if the object does not exsit</returns>
    public bool RemoveFloatAt(string key)
    {
        try
        {
            persistentIntHolder.Remove(key);
            return true;
        }
        catch
        {
            return false;
        }
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

    public Dictionary<string, object> GetAllObjects()
    {
        return persistentHolder;
    }

    public Dictionary<string, int> GetAllInts()
    {
        return persistentIntHolder;
    }

    public Dictionary<string, float> GetAllFloats()
    {
        return persistentFloatHolder;
    }

    public Dictionary<string, bool> GetAllBooleans()
    {
        return persistentBoolHolder;
    }

    public Dictionary<string, string> GetAllStrings()
    {
        return persistentStringHolder;
    }

    /// <summary>
    /// Will delete everything inside the file. The Save file itself will remain in the filepath.
    /// </summary>
    public void WipeSave()
    {
        persistentHolder = new Dictionary<string, object>();
        persistentBoolHolder = new();
        persistentFloatHolder = new();
        persistentIntHolder = new();
        persistentStringHolder = new();
        Save();
    }
}