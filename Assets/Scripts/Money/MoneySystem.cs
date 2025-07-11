using Leopotam.Ecs;

public class MoneySystem : IEcsInitSystem, IEcsRunSystem
{
    EcsWorld _world;
    GameUI gameUI;
    EcsFilter<Money> _moneyFilter = null;
    EcsFilter<BusinessModel> _businessModelFilter = null;

    public void Init()
    {
        var money = _world.NewEntity();
        money.Get<Money>().moneyValue = SaveController.GetMoney();

        gameUI.UpdateMoney(_moneyFilter.Get1(0).moneyValue);
    }

    public void AddMoney(int count)
    {
        _moneyFilter.Get1(0).moneyValue += count;
        SaveController.AddMoney(count);
        gameUI.UpdateMoney(_moneyFilter.Get1(0).moneyValue);
    }

    public void SpendMoney(int count)
    {
        AddMoney(-count);
        SaveController.SpendMoney(count);
    }

    public void Run()
    {
        for (int i = 0; i < _businessModelFilter.GetEntitiesCount(); i++)
        {
            if (_businessModelFilter.Get1(i).income > 0)
            {
                AddMoney(_businessModelFilter.Get1(i).income);
                _businessModelFilter.Get1(i).income = 0;
            }
        }
    }
}
