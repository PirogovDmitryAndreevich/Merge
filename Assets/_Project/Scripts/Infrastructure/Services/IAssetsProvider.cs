using UnityEngine;

public interface IAssetsProvider : IService
{
    GameObject Instantiate(string path);
}