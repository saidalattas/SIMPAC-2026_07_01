using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VRFarm/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int money = 0;
    public Dictionary<string, int> inventory = new();
    public event Action OnDataChanged;

    //XP & Level 
    public int xp = 0;      // XP saat ini
    public int xpMax = 100; // XP maksimum untuk naik level ini
    public int level = 1;

    public void AddXp(int amount)
    {
        xp += amount;

        // Cek naik level
        while (xp >= xpMax)
        {
            xp -= xpMax;
            level++;

            // Biar makin susah tiap level
            xpMax += 50;
        }

        OnDataChanged?.Invoke();
    }

    //UANG 
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log($"AddMoney dipanggil, total uang sekarang: Rp {money}");
        OnDataChanged?.Invoke();
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount) return false;
        money -= amount;
        OnDataChanged?.Invoke();
        return true;
    }

    //INVENTORY 
    public void AddItem(string id, int qty = 1)
    {
        if (!inventory.ContainsKey(id)) inventory[id] = 0;
        inventory[id] += qty;
        OnDataChanged?.Invoke();
    }

    public bool RemoveItem(string id, int qty = 1)
    {
        if (!inventory.ContainsKey(id) || inventory[id] < qty) return false;
        inventory[id] -= qty;
        OnDataChanged?.Invoke();
        return true;
    }

    public void ResetAll()
    {
        money = 0;
        xp = 0;
        level = 1;

        inventory.Clear();

        OnDataChanged?.Invoke();
    }
}
