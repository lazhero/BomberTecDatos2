using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Configuration;
using TMPro;
using UnityEngine;

public class ValueBetweenButtons : MonoBehaviour
{

    public int value;
    public int maxLimit;
    public int minLimit;
    
    public TextMeshProUGUI text;
    public string type;
        
    public void Add(int v)
    {
        value += v;
        if (value > maxLimit)
            value = maxLimit;
        if (value < minLimit)
            value = minLimit;
        
        if(type.CompareTo("Humans")==0)
            MapConfig.Humans = value;
        if(type.CompareTo("Side")==0)
            MapConfig.Side = value;

        text.text = value.ToString();
    }


}

