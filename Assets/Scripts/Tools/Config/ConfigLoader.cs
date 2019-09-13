using System;
using UnityEngine;

namespace Tools.Config
{
    public class ConfigLoader : IConfigLoader
    {
        public T LoadConfig<T>(string path)
        {
            try
            {
                var file = Resources.Load<TextAsset>(path);
                return JsonUtility.FromJson<T>(file.text);
            }
            catch (Exception)
            {
                Debug.LogWarning($"File {path} doesn't exist");
                return default;
            }
        }
    }
}