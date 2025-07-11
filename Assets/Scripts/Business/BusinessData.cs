using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessData", menuName = "Config/BusinessData")]
public class BusinessData : ScriptableObject
{
    public Business[] businesses;
}

[Serializable]
public struct Business
{
    public string name;
    public int incomeDelay;
    public int cost;
    public int income;
    public BusinessBonus businessBonus1;
    public BusinessBonus businessBonus2;
}

[Serializable]
public struct BusinessBonus
{
    public string name;
    public int cost;
    public int incomeBonus;
}