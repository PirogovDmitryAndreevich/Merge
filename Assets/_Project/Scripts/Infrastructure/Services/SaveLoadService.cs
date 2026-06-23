using YG;

public class SaveLoadService : ISaveLoadService
{
    private readonly IProgressService _progress;

    public SaveLoadService(IProgressService progress) =>
        _progress = progress;

    public void SaveProgress() =>
        YandexGamesSave();

    public SaveData LoadProgress() => 
    YG2.saves.Saves;

    private void YandexGamesSave()
    {
        YG2.saves.Saves = _progress.Progress;
        YG2.SaveProgress();
    }
}