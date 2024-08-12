
using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    [SerializeField] AsyncLoader loader;

    #region Settings when difficulty is easy
    public void Easy()
    {
        DifficultyElements.maxEnergy = 5000f;
        DifficultyElements.timeForCaveCollapse = 720f;
        loader.LoadLevel(1);
    }
    #endregion

    #region Settings when difficulty is medium
    public void Medium()
    {
        DifficultyElements.maxEnergy = 1000f;
        DifficultyElements.timeForCaveCollapse = 300f;
        loader.LoadLevel(1);
    }
    #endregion

    #region Settings when difficulty is Hard
    public void Hard()
    {
        DifficultyElements.maxEnergy = 250f;
        DifficultyElements.timeForCaveCollapse = 120f;
        loader.LoadLevel(1);
    }
    #endregion

    #region Settings when difficulty is Insane
    public void Insane()
    {
        DifficultyElements.maxEnergy = 100f;
        DifficultyElements.timeForCaveCollapse = 75f;
        loader.LoadLevel(1);
    }
    #endregion

}
