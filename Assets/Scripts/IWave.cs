public interface IWave
{
    public void RunWave();
    public bool IsWaveCompleted();

    public EnemyWave Wave { get; set; }
}