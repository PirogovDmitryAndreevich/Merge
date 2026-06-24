using UnityEngine;

public interface IAssetsProvider : IService
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Transform parent);
}