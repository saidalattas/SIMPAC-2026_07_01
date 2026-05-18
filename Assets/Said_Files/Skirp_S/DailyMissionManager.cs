using UnityEngine;
using TMPro;

public class DailyMissionManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text missionText;
    public TMP_Text progressText;

    [Header("Mission Data (Dynamic)")]
    public MissionData currentMission;

    [Header("Mission List (2 Missions)")]
    public MissionData mission1;
    public MissionData mission2;

    [Header("Reward System")]
    public LevelManager levelManager;

    private void Start()
    {
        // Set default mission ke mission1
        currentMission = mission1;
        UpdateMission(currentMission);
    }

    // Dipanggil dari Harvestable.cs setelah player panen
    public void AddProgress(string itemId)
    {
        if (currentMission == null) return;
        if (currentMission.isCompleted) return;

        // cek apakah item sesuai misi
        if (itemId == currentMission.targetItem)
        {
            currentMission.progress++;

            // update UI langsung
            UpdateMission(currentMission);

            // cek apakah misi selesai
            if (currentMission.progress >= currentMission.required)
            {
                CompleteMission();
            }
        }

        XPBarUI xpUI = FindObjectOfType<XPBarUI>();
        if (xpUI != null) xpUI.UpdateUI();
    }

    private void CompleteMission()
    {
        currentMission.isCompleted = true;

        missionText.text = "Mission Complete!";
        progressText.text = "";

        // Berikan reward XP
        if (levelManager != null)
        {
            levelManager.AddXP(currentMission.rewardXP);
        }

        Debug.Log("Daily mission finished!");

        // Pindah ke mission selanjutnya
        LoadNextMission();
    }

    private void LoadNextMission()
    {
        if (currentMission == mission1)
        {
            currentMission = mission2;

            currentMission.progress = 0;
            currentMission.isCompleted = false;

            UpdateMission(currentMission);

            Debug.Log("Mission 2 dimulai!");
        }
        else
        {
            Debug.Log("Semua misi harian selesai!");
        }
    }

    public void UpdateMission(MissionData mission)
    {
        missionText.text = "Mission: Harvest & Sell to Market " + mission.required + " " + mission.targetItem;
        progressText.text = "Progress: " + mission.progress + "/" + mission.required;
    }
}
