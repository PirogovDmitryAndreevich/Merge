using System.Collections.Generic;

public interface IEnemyItemsSelector : IService
{
    List<ItemData> SelectEnemyItems();
}