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
        GUILayout.Label("Time");
        EditorGUILayout.EndHorizontal();

        for (int i = 1; i <= data.ItemsCount; i++)
        {
            int profit = data.Items[i - 1].Profit;
            int price = data.Items[i - 1].Price; 
            float time = CalculateTime(i);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(i.ToString(), GUILayout.Width(40));
            GUILayout.Label(profit.ToString(), GUILayout.Width(80));
            GUILayout.Label(price.ToString(), GUILayout.Width(80));
            GUILayout.Label(time.ToString("0.0"));
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

        int itemIndex = 0;

        foreach (var sprite in sprites)
        {
            itemIndex++;
            float time = CalculateTime(itemIndex);
            int profit = CalculateProfit(itemIndex, time);
            int price = CalculatePrice(itemIndex, profit, time);

            ItemData item = new ItemData(itemIndex, sprite, profit,price);
            data.Items.Add(item);
        }
    }

    private int CalculatePrice(int index, int profit, float time)
    {
        float expectedIncome =
        profit * data.AverageComboMultiplier;

        return Mathf.Max(1, Mathf.RoundToInt(expectedIncome * time));
    }

    private int CalculateProfit(int index, float time)
    {
        float normalizedTime =
        time / Mathf.Max(0.1f, data.StartUnlockTime);

        float growth = Mathf.Pow(data.ProfitGrowth, index - 1);

        float value =
            (1f + index * 0.3f) * growth * (1f + normalizedTime);

        return Mathf.Max(1, Mathf.RoundToInt(value));
    }

    private float CalculateTime(int index)
    {
        float t = (float)(index - 1) / Mathf.Max(1, data.ItemsCount - 1);

        float curve = data.DifficultyCurve.Evaluate(t);

        return data.StartUnlockTime * curve;
    }
}
