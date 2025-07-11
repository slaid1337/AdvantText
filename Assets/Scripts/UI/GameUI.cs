using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TMP_Text moneyText;

    public GameObject businessCardPrefab;
    public Transform buisinessCardContainer;

    public Button addMoneyButton;

    private List<BusinessCard> _businessCards = new List<BusinessCard>();

    public void UpdateMoney(int count)
    {
        moneyText.text = count.ToString() + "$";
    }

    public void AddBusinessCard(string name, int lvl, int income, int lvlUpCost, float incomeProgress, BusinessSystem businessSystem, bool isLvlMax = false)
    {
        BusinessCard card =  Instantiate(businessCardPrefab, buisinessCardContainer).GetComponent<BusinessCard>();

        card.UpdateData(name, lvl, income, lvlUpCost, incomeProgress, isLvlMax);

        card.bonus1Button.onClick.AddListener(delegate
        {
            businessSystem.BuyBonus1(card.businessName);
        });

        card.bonus2Button.onClick.AddListener(delegate
        {
            businessSystem.BuyBonus2(card.businessName);
        });

        card.lvlUpButton.onClick.AddListener(delegate
        {
            businessSystem.LvlUp(card.businessName);
        });

        _businessCards.Add(card);
    }

    public void UpdateBusinessCardButtons(string name,bool isBuyed1, string name1, int income1, int cost1, bool isBuyed2, string name2, int income2, int cost2)
    {
        BusinessCard card = _businessCards.Where(x => x.businessName == name).First();

        card.bonusButton1.UpdateData(name1, income1, cost1, isBuyed1);
        card.bonusButton2.UpdateData(name2, income2, cost2, isBuyed2);
    }

    public void UpdateBusinessCard(string name, int lvl, int income, int lvlUpCost, float incomeProgress, bool isLvlMax = false)
    {
        BusinessCard card = _businessCards.Where(x => x.businessName == name).First();

        card.UpdateData(name, lvl, income, lvlUpCost, incomeProgress, isLvlMax);
    }

    public void UpdateProgressBusinessCard(string name, float progress)
    {
        BusinessCard card = _businessCards.Where(x => x.businessName == name).First();

        card.UpdateProgress(progress);
    }
}
