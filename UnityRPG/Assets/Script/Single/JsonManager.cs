using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : ManagerClassBase<JsonManager>
{
    // Json ������ �����մϴ�.
    public void SaveToJson<T>(string path, T file)
    {
        string json = JsonUtility.ToJson(file, true);

        File.WriteAllText(Application.dataPath + "/Resources/Json/" + path + ".Json", json);
    }

    // Json ������ �ҷ��ɴϴ�.
    public T LoadFromJson<T>(string path)
    {
        string json = File.ReadAllText(Application.dataPath + "/Resources/Json/" + path + ".Json");

        T load = JsonUtility.FromJson<T>(json);

        return load;
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> list;
    }      

}
