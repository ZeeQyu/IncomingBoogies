using UnityEngine;
using System.Collections;

public class ScoreDisplayBehaviour : MonoBehaviour
{
    [TextArea][SerializeField] private string displayTextTemplate = "The struggle is over.\nYou survived {0} waves and reached difficulty level {1}.\nPress space to start over.";

    private int savedWavesSurvived;
    private int savedDifficultyReached;

    void Start()
    {
        savedWavesSurvived = ScoreKeeper.wavesSurvived;
        savedDifficultyReached = ScoreKeeper.difficultyLevelReached;

        ScoreKeeper.wavesSurvived = 0;
        ScoreKeeper.difficultyLevelReached = 0;
    }
    
    void OnGUI()
    {
        GetComponent<UnityEngine.UI.Text>().text = string.Format(displayTextTemplate, savedWavesSurvived, savedDifficultyReached);
    }
}