using UnityEngine;

public class PlayerDataResetter : MonoBehaviour
{
    public PlayerData data;
    public int startingMoney = 0; // kasih modal di sini

    void Awake()
    {
        if (data != null)
        {
            data.ResetAll();
            data.AddMoney(startingMoney);
        }
    }
}