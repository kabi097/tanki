using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int smallTanksInThisLevel, fastTanksInThisLevel, bigTanksInThisLevel, armoredTanksInThisLevel, stageNumber;
    public static int smallTanks, fastTanks, bigTanks, armoredTanks;
    private void Awake()
    {
        MasterTracker.stageNumber = stageNumber;
        smallTanks = smallTanksInThisLevel;
        fastTanks = fastTanksInThisLevel;
        bigTanks = bigTanksInThisLevel;
        armoredTanks = armoredTanksInThisLevel;
    }
}