using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public PersistentData persistent;
    public float autoSaveTimer = 0f;

    private void Start()
    {
        persistent = new PersistentData("Save");
        Debug.Log("I wanted to see this " + persistent.ToString());

        Debug.Log(persistent.CheckPool("timestamp"));
        if (persistent.CheckPool("timestamp"))
        {
            LoadAll();
        }
    }

    private void Update()
    {
        autoSaveTimer += Time.deltaTime;
        if (autoSaveTimer >= 5f)
        {
            SaveAll();
            autoSaveTimer = 0;
        }
    }

    public void SaveAll()
    {
        foreach (ISaveAble item in gameObject.GetComponents<ISaveAble>())
        {
            item.Save();
        }
        persistent.Save();
    }

    public void LoadAll()
    {
        persistent.Load();
        foreach (ISaveAble item in gameObject.GetComponents<ISaveAble>())
        {
            item.Load();
        }
    }

    public void ResetAll()
    {
        persistent.WipeSave();
        SceneManager.LoadScene(0);
    }

    private void OnApplicationPause(bool pause)
    {
        SaveAll();
    }

    public void QuitGame()
    {
        SaveAll();
        Application.Quit();
    }
}