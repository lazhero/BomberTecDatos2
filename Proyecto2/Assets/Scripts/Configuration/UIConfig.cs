
using System;
using DefaultNamespace.Configuration;
using UnityEngine;

public class UIConfig : MonoBehaviour
{
    private void Start()
    {
        MapConfig.Side = 10;

    }

    public void Play()
    {
        switch (MapConfig.Mode)
        {
            case 1 :
                LevelLoader.LoadLevel("Juego");
                break;
            case 2:
                LevelLoader.LoadLevel("Juego");
                break;
            case 3:
                Debug.Log("Modo Team" +MapConfig.ip);
                break;
        }
    }

    public void SetMode(int mode)
    {
        MapConfig.Mode = mode;
    }

    public void SetHumans(int humans)
    {
        MapConfig.Humans = humans;
    }

    public void SetIp(string ip)
    {
        MapConfig.ip = ip;
    }

    public void SetSide(int n)
    {
        MapConfig.Side = n;
    }
}
