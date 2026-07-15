public interface IAwardHandler : IService
{
    bool HaveAvailable { get; }
    bool IsReached { get; }

    void CheckReached();
    void GiveOutReward();
}