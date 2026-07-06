public class FailWindow : EndBattleWindow
{
    protected override void Subscribes() => 
        BattleRunner.EnemyWon += Show;
    protected override void Unsubscribes() => 
        BattleRunner.EnemyWon -= Show;
}
