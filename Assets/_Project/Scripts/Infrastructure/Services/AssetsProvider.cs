using UnityEngine;

public class AssetsProvider : IAssetsProvider
{
    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }
}
