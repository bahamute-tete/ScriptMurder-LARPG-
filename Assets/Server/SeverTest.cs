using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;


public class SeverTest : MonoBehaviour
{


    [SerializeField]Button btn;
    [SerializeField] TextMeshProUGUI content;

    public string url = "http://localhost:2048/display_data";
    public class Data
    {
        public string name;
        public int age;
    }

    [System.Serializable]
    public class Vertex
    {
        public float x;
        public float y;
        public float z;
    }

    [Serializable]
    public class Student
    {
        public int id;
        public string name;
        public int age;
    }

    [Serializable]
    public class Table
    {
        public Student[] students;
    }

    // A wrapper class for an array of vertices (needed for JsonUtility)
    [System.Serializable]
    public class VertexArray
    {
        public Vertex[] vertices;
    }

    [Serializable]
    public class Story
    {
        public string story;
    }

    // Start is called before the first frame update
    void Start()
    {

        Data data = new Data();
        data.name = "Alice";
        data.age = 25;

        string json = JsonUtility.ToJson(data);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

        btn.onClick.AddListener(delegate
        {
            StartCoroutine(GetStory());
        });
       
    }

    IEnumerator UnityDatabase()
    {
        string url = "http://localhost:2048/unitydatabase";



        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                var json = request.downloadHandler.text;
                Debug.Log(json);
               
                Table table = JsonUtility.FromJson<Table>(json);

                string message = null;
                foreach (var item in table.students)
                {
                    var str = $"{item.id.ToString()},{item.name},{item.age.ToString()}";
                    message += str + "\n";
                }

                showResult(message);
            }
        }
    }

    IEnumerator Post()
    {
        string url = "http://localhost:2048/formdata";

        WWWForm form = new WWWForm();
        form.AddField("name", "alice");
        form.AddField("age", 12);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {

                showResult(request.downloadHandler.text);
            }
        }
    }

    IEnumerator Get()
    {
        float a = 1.5f;
        float b = 3.2f;
        string url = "http://localhost:2048/add?a="+ a + "&b=" + b;



        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {

                showResult(request.downloadHandler.text);
            }
        }
    }

    IEnumerator PostJson(byte[] bytes)
    {

        string url = "http://localhost:2048/process";



        using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
        {
            request.uploadHandler = new UploadHandlerRaw(bytes);
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {

                var response = request.downloadHandler.text;

                Data result = JsonUtility.FromJson<Data>(response);

                var message = "The processed name is " + result.name + "\n" +
                              "The processed age is " + result.age;
                showResult(message);
               
            }
        }
    }

    IEnumerator GetVertices()
    {

        string url = "http://localhost:2048/vertices";



        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
           
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {

                var json = request.downloadHandler.text;
                Debug.Log(json);
                json = "{\"vertices\":" + json + "}";
                Debug.Log(json);
                VertexArray result = JsonUtility.FromJson<VertexArray>(json);

                foreach (Vertex vertex in result.vertices)
                {

                    showResult($"({vertex.x}, {vertex.y}, {vertex.z})");
                }



            }
        }
    }
    IEnumerator GetStory()
    {

        string url = "http://localhost:2048/script_murder";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                var res = request.downloadHandler.text;
                showResult($"{res}");
            }
        }
    }

    IEnumerator UploadForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "John");
        form.AddField("age", "30");

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Form upload successful!");
        }
        else
        {
            Debug.Log("Form upload failed: " + request.error);
        }
    }



    private void showResult(string text)
    {
        content.alignment = TextAlignmentOptions.Left;
        content.text = text;
        
    }
}
