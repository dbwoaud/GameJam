using UnityEngine;

[CreateAssetMenu(fileName = "SequnceDataSO", menuName = "GamePlay/SequnceDataSO", order = 1)]
public class SequenceDataSO : ScriptableObject
{
    public void StartSequence()
    {
        var ui = UIManager.Instance.Show<TutorialUI>();
        ui.Show();
    }
}
