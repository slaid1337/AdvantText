using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class BusinessSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
{
    EcsWorld _world;
    BusinessData businessData;
    GameUI gameUI;
    MoneySystem moneySystem;

    EcsFilter<BusinessModel> businessFilter;
    EcsFilter<Money> moneyFilter;

    public void Destroy()
    {
        for (int i = 0; i < businessFilter.GetEntitiesCount(); i++)
        {
            ref BusinessModel model = ref businessFilter.Get1(i);

            BusinessSave save = new BusinessSave();
            save.name = model.name;
            save.lvl = model.lvl;
            save.incomeValue = model.income;
            save.incomeTime = (int) model.incomeTimer;
            save.isBuyedBonus1 = model.isBuyedBonus1;
            save.isBuyedBonus2 = model.isBuyedBonus2;
            SaveController.SetBusinessSave(save);
        }
    }

    public void Init()
    {
        gameUI.addMoneyButton.onClick.AddListener(delegate
        {
            moneySystem.AddMoney(1000);
        });

        List<BusinessModel> models = new List<BusinessModel>();

        foreach (var item in businessData.businesses)
        {
            var business = _world.NewEntity();
            ref BusinessModel model = ref business.Get<BusinessModel>();

            BusinessSave save = SaveController.GetBusinessSave(item.name);

            model.name = item.name;
            model.incomeDelay = item.incomeDelay;
            model.cost = item.cost;
            model.incomeBase = item.income;

            model.lvl = save.lvl;
            model.incomeTimer = save.incomeTime;
            model.isBuyedBonus1 = save.isBuyedBonus1;
            model.isBuyedBonus2 = save.isBuyedBonus2;
            model.income = save.incomeValue;

            model.nameBonus1 = item.businessBonus1.name;
            model.incomeBonus1 = item.businessBonus1.incomeBonus;
            model.costBonus1 = item.businessBonus1.cost;

            model.nameBonus2 = item.businessBonus2.name;
            model.incomeBonus2 = item.businessBonus2.incomeBonus;
            model.costBonus2 = item.businessBonus2.cost;

            models.Add(model);
        }

        foreach (var item in models)
        {
            float bonusMultiplyer = 0;

            float incomeProgress = item.incomeTimer / item.incomeDelay;

            if (item.isBuyedBonus1) bonusMultiplyer += ((float)item.incomeBonus1 / 100);
            if (item.isBuyedBonus2) bonusMultiplyer += ((float)item.incomeBonus2 / 100);

            gameUI.AddBusinessCard(item.name, item.lvl, (item.lvl * item.incomeBase * (1 + (int)bonusMultiplyer)), (item.lvl + 1) * item.cost, incomeProgress, this);
            gameUI.UpdateBusinessCardButtons(item.name, item.isBuyedBonus1, item.nameBonus1, item.incomeBonus1, item.costBonus1, item.isBuyedBonus2, item.nameBonus2, item.incomeBonus2, item.costBonus2);
        }
    }

    public void Run()
    {
        for (int i = 0; i < businessFilter.GetEntitiesCount(); i++)
        {
            ref BusinessModel model = ref businessFilter.Get1(i);

            if (model.lvl == 0) continue;

            model.incomeTimer += Time.deltaTime;

            float incomeProgress = model.incomeTimer / model.incomeDelay;

            if (model.incomeTimer >= model.incomeDelay)
            {
                model.incomeTimer = 0;


                float bonusMultiplyer = 0;

                if (model.isBuyedBonus1) bonusMultiplyer += ((float)model.incomeBonus1 / 100);
                if (model.isBuyedBonus2) bonusMultiplyer += ((float)model.incomeBonus2 / 100);

                model.income = (int) ( model.lvl * model.incomeBase * (1 + bonusMultiplyer));

                gameUI.UpdateBusinessCard(model.name, model.lvl, (model.lvl * model.incomeBase * (1 + (int)bonusMultiplyer)), (model.lvl + 1) * model.cost, incomeProgress);
                gameUI.UpdateBusinessCardButtons(model.name, model.isBuyedBonus1, model.nameBonus1, model.incomeBonus1, model.costBonus1, model.isBuyedBonus2, model.nameBonus2, model.incomeBonus2, model.costBonus2);
            }

            gameUI.UpdateProgressBusinessCard(model.name, incomeProgress);
        }
    }

    public void LvlUp(string name)
    {
        for (int i = 0; i < businessFilter.GetEntitiesCount(); i++)
        {
            ref BusinessModel model = ref businessFilter.Get1(i);

            if (model.name != name) continue;

            int cost = (model.lvl + 1) * model.cost;

            if (moneyFilter.Get1(0).moneyValue >= cost)
            {
                model.lvl += 1;
                moneySystem.SpendMoney(cost);

                UpdateUI();
            }

            break;
        }
    }

    public void BuyBonus1(string name)
    {
        for (int i = 0; i < businessFilter.GetEntitiesCount(); i++)
        {
            ref BusinessModel model = ref businessFilter.Get1(i);

            if (model.name != name) continue;

            if (model.isBuyedBonus1) return;

            if (moneyFilter.Get1(0).moneyValue >= model.costBonus1)
            {
                model.isBuyedBonus1 = true;
                moneySystem.SpendMoney(model.costBonus1);

                UpdateUI();
            }

            break;
        }
    }

    public void BuyBonus2(string name)
    {
        for (int i = 0; i < businessFilter.GetEntitiesCount(); i++)
        {
            ref BusinessModel model = ref businessFilter.Get1(i);

            if (model.name != name) continue;

            if (model.isBuyedBonus2) return;

            if (moneyFilter.Get1(0).moneyValue >= model.costBonus2)
            {
                model.isBuyedBonus2 = true;
                moneySystem.SpendMoney(model.costBonus2);

                UpdateUI();
            }

            break;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < businessFilter.GetEntitiesCount(); i++)
        {
            ref BusinessModel model = ref businessFilter.Get1(i);

            if (model.lvl == 0) continue;

            float incomeProgress = model.incomeTimer / model.incomeDelay;


            float bonusMultiplyer = 0;

            if (model.isBuyedBonus1) bonusMultiplyer += ((float)model.incomeBonus1 / 100);
            if (model.isBuyedBonus2) bonusMultiplyer += ((float)model.incomeBonus2 / 100);

            gameUI.UpdateBusinessCard(model.name, model.lvl, (model.lvl * model.incomeBase * (1 + (int)bonusMultiplyer)), (model.lvl + 1) * model.cost, incomeProgress);
            gameUI.UpdateBusinessCardButtons(model.name, model.isBuyedBonus1, model.nameBonus1, model.incomeBonus1, model.costBonus1, model.isBuyedBonus2, model.nameBonus2, model.incomeBonus2, model.costBonus2);
        }
    }
}
