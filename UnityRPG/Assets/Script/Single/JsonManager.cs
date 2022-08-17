using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : ManagerClassBase<JsonManager>
{
    // Json 파일을 생성합니다.
    public void SaveToJson<T>(string path, T file)
    {
        string json = JsonUtility.ToJson(file, true);

        File.WriteAllText(Application.dataPath + "/Resources/Json/" + path + ".Json", json);
    }

    // Json 파일을 불러옵니다.
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
