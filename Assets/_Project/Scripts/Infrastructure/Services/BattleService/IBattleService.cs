using System.Collections.Generic;

public interface IBattleService : IService
{
    List<ItemData> SelectedItems { get;}
    List<ItemData> EnemyItems { get;}

    void Cleanup();
    void DeselectItem(ItemData item);
    void SelectEnemyItems();
    void SelectItem(ItemData item);
}