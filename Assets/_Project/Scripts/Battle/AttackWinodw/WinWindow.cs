public class WinWindow : EndBattleWindow
{
    protected override void Subscribes() => 
        BattleRunner.PlayerWon += Show;

    protected override void Unsubscribes() => 
        BattleRunner.PlayerWon -= Show;
}
