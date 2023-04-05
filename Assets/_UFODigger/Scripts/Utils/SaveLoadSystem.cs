using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utils
{
    public static class SaveLoadSystem
    {
        public static bool CheckKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static int LoadInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public static float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public static string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public static bool LoadBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1 ? true : false;
        }

        public static void Save(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static void Save(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public static void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public static void Save(string key, bool value)
        {
            var intFromBool = value ? 1 : 0;
            PlayerPrefs.SetInt(key, intFromBool);
            PlayerPrefs.Save();
        }
    }

    public class Serializer
    {
        public static T Load<T>(string filename) where T : class
        {
            if (File.Exists(filename))
            {
                try
                {
                    using (Stream stream = File.OpenRead(filename))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        return formatter.Deserialize(stream) as T;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }

            return default(T);
        }

        public static void Save<T>(string filename, T data) where T : class
        {
            using (Stream stream = File.OpenWrite(filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }
    }
}