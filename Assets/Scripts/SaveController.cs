using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveController
{
    public static void SetBusinessSave(BusinessSave save)
    {
        PlayerPrefs.SetInt(save.name + "lvl", save.lvl);
        PlayerPrefs.SetInt(save.name + "incomeTimer", save.incomeTime);
        PlayerPrefs.SetInt(save.name + "incomeValue", save.incomeValue);
        PlayerPrefs.SetInt(save.name + "Bonus1", save.isBuyedBonus1 == true ? 1 : 0);
        PlayerPrefs.SetInt(save.name + "Bonus2", save.isBuyedBonus2 == true ? 1 : 0);
    }

    public static BusinessSave GetBusinessSave(string name)
    {
        BusinessSave save = new BusinessSave();

        save.name = name;

        save.lvl = PlayerPrefs.GetInt(name + "lvl", 0);
        save.incomeTime = PlayerPrefs.GetInt(name + "incomeTimer", 0);
        save.incomeValue = PlayerPrefs.GetInt(name + "incomeValue", 0);
        save.isBuyedBonus1 = PlayerPrefs.GetInt(name + "Bonus1", 0) == 1;
        save.isBuyedBonus2 = PlayerPrefs.GetInt(name + "Bonus2", 0) == 1;

        return save;
    }

    public static void SetDefaultData(GameConfig config)
    {
        PlayerPrefs.SetInt(config.defaultBusinessName + "lvl", 1);
    }

    public static void SetFirstStart()
    {
        PlayerPrefs.SetInt("IsFirstStart", 1);
    }

    public static bool IsFirstStart()
    {
        return PlayerPrefs.GetInt("IsFirstStart", 0) == 0;
    }

    public static void AddMoney(int count)
    {
        int money = GetMoney() + count;

        PlayerPrefs.SetInt("Money", money);
    }

    public static void SpendMoney(int count)
    {
        AddMoney(-count);
    }

    public static int GetMoney()
    {
        return PlayerPrefs.GetInt("Money", 0);
    }
}

[Serializable]
public class BusinessSave
{
    public string name;
    public int lvl;
    public int incomeTime;
    public int incomeValue;
    public bool isBuyedBonus1;
    public bool isBuyedBonus2;
}