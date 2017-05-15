public static class ScoreKeeper
{
    public static int wavesSurvived = 0;
    public static int difficultyLevelReached = 0;

    static void OnNextDifficultyLevel()
    {
        difficultyLevelReached++;
    }
    static void OnWaveCleared()
    {
        wavesSurvived++;
    }
}
