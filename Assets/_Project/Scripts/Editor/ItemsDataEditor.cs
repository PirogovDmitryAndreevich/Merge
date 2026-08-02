using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemsData))]
public class ItemsDataEditor : Editor
{
    private ItemsData data;

    private void OnEnable()
    {
        data = (ItemsData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(10);
        if (GUILayout.Button("BUILD ECONOMY"))
        {
            BuildEconomy();
            EditorUtility.SetDirty(data);
        }

        DrawTable(data);
    }

    private void DrawTable(ItemsData data)
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID", GUILayout.Width(40));
        GUILayout.Label("Profit", GUILayout.Width(80));
        GUILayout.Label("Price", GUILayout.Width(80));
        GUILayout.Label("Luck", GUILayout.Width(80));
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < data.ItemsCount; i++)
        {
            var itemData = data.Items[i];

            int id = itemData.ID;
            int profit = itemData.Profit;
            int price = itemData.Price;
            float luck = itemData.Luck;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(id.ToString(), GUILayout.Width(40));
            GUILayout.Label(profit.ToString(), GUILayout.Width(80));
            GUILayout.Label(price.ToString(), GUILayout.Width(80));
            GUILayout.Label(luck.ToString(), GUILayout.Width(80));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }

    private void BuildEconomy()
    {
        data.Items = new List<ItemData>();
        List<Sprite> sprites = new List<Sprite>();

        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/_Project/Sprites/Items" });

        Array.Sort(guids, (a, b) =>
        {
            string pathA = AssetDatabase.GUIDToAssetPath(a);
            string pathB = AssetDatabase.GUIDToAssetPath(b);

            return string.Compare(pathA, pathB, StringComparison.Ordinal);
        });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);

            sprites.Add(sprite);
        }

        data.ItemsCount = sprites.Count;

        int itemID = 0;

        foreach (var sprite in sprites)
        {
            itemID++;
            int profit = CalculateProfit(itemID);
            int price = CalculatePrice(itemID, profit);
            int health = CalculateHealth(itemID);
            int damage = CalculateDamage(itemID);
            float luck = CalculateLuck(itemID);

            ItemData item = new ItemData(itemID, sprite, profit, price, health, damage, luck);
            data.Items.Add(item);
        }
    }

    private int CalculatePrice(int index, int profit)
    {
        GetPackData(
            index,
            out int packIndex,
            out int packSize,
            out int itemInPack);


        float packProgress =
            packSize <= 1
                ? 0f
                : (float)(itemInPack - 1) /
                  (packSize - 1);


        float curveValue =
            data.PriceMultiplierCurve.Evaluate(
                packProgress);


        float packMultiplier =
            Mathf.Pow(
                data.PackPriceGrowth,
                packIndex);


        float priceMultiplier =
            data.StartPriceMultiplier *
            packMultiplier;


        float finalMultiplier =
            priceMultiplier *
            Mathf.Lerp(
                1f,
                2f,
                curveValue);


        float price =
            profit *
            finalMultiplier;


        return Mathf.Max(
            1,
            Mathf.RoundToInt(price));
    }

    private int CalculateProfit(int index)
    {
        int firstPackSize =
        data.FirstPackProfits.Count;

        if (index <= firstPackSize)
        {
            return data.FirstPackProfits[index - 1];
        }

        int lastProfit =
            data.FirstPackProfits[
                firstPackSize - 1];

        int itemsAfterFirstPack = index - firstPackSize;

        float growth =
            Mathf.Pow(
                data.ProfitGrowth,
                itemsAfterFirstPack);


        float value =
            lastProfit *
            growth;


        return Mathf.Max(
            1,
            Mathf.RoundToInt(value));
    }

    private void GetPackData(int itemIndex, out int packIndex,
        out int packSize, out int itemInPack)
    {
        int currentItem = 0;

        packIndex = 0;

        while (true)
        {
            packSize = GetPackSize(packIndex);

            int packStart = currentItem + 1;


            int packEnd = currentItem + packSize;


            if (itemIndex <= packEnd)
            {
                itemInPack =itemIndex - packStart + 1;
                return;
            }

            currentItem += packSize;

            packIndex++;
        }
    }

    private int GetPackSize(int packIndex)
    {
        int firstPackSize =
        data.FirstPackProfits.Count;

        float size =
            firstPackSize *
            Mathf.Pow(
                data.PackSizeGrowth,
                packIndex);

        return Mathf.Max(
            1,
            Mathf.RoundToInt(size));
    }

    private int CalculateHealth(int index)
    {
        if (index == 1)
            return data.StartHealth;

        float step = data.StartHealth / 5f;
        float health = data.StartHealth + (index - 1) * step;

        return Mathf.RoundToInt(health);
    }

    private int CalculateDamage(int index)
    {
        float step = data.StartHealth / 5f;
        float startDamage = data.StartHealth / 2f - step;
        float damage = startDamage + (index - 1) * step;

        return Mathf.Max(1, Mathf.RoundToInt(damage));
    }

    private float CalculateLuck(int index)
    {
        if (index <= 3)
            return 10f;

        float t = Mathf.InverseLerp(data.MinLuck, data.ItemsCount, index);

        return Mathf.Lerp(data.MaxLuck - 1, data.MinLuck, t);
    }
}
