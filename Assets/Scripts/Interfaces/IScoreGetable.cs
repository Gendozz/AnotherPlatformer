public interface IScoreGetable 
{
    public void AddScore(int scoresAmountToAdd);

    public int CurrentScoreAmount { get; }
}
