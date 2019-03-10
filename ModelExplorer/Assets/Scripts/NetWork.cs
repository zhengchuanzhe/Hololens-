using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetWork : MonoBehaviour
{



    public Transform parentObject;



    public GameObject taskItem;

    // Use this for initialization
    public static NetWork instance;

    [System.Serializable]
    public class tagObject
    {
        public int id;
        public string title;
        public string task_detail;
        public int order_id;
        public string mark;
    }


    [System.Serializable]
    public class AnalysedObject
    {
        public tagObject[] tag;
        public string requestId;
        public object metadata;
    }

    /// <summary>
    /// The analysis result text
    /// </summary>
    private TextMesh labelText;

    private void Awake()
    {
        instance = this;
        TOKEN = getAccessToken();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


   
    public IEnumerator GetTask()
    {
        string url = "http://101.201.67.210/DatangAR/DatangAR/public/admin/Getdetail/getDetail";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();
            if (www.responseCode == 200)//200表示接受成功
            {
                Debug.Log(www.downloadHandler.text);

                string jsonResponse = null;
                jsonResponse = www.downloadHandler.text;

                // The response will be in Json format
                // therefore it needs to be deserialized into the classes AnalysedObject and TagData
                AnalysedObject analysedObject = JsonUtility.FromJson<AnalysedObject>(jsonResponse);
                if (analysedObject.tag == null)
                {
                    Debug.Log("analysedObject.tagData is null");
                }
                else
                {
                    List<tagObject> listName = new List<tagObject>();
                    foreach (tagObject td in analysedObject.tag)
                    {
                        listName.Add(td);
                    }
                    CreateLabel(listName);
                }
            }
            else
            {
                if (www.error != null)
                {
                    Debug.Log(www.error);
                }
            }
        }
    }



    /// <summary>
    /// Spawns cursor for the Main Camera
    /// </summary>
    private void CreateLabel(List<tagObject> listName)
    {
        foreach (Transform child in parentObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        float yPos = 23f;
        float hight = 42f;
        float parentHight = (hight + 2) * listName.Count;
        parentHight = parentHight > 300 ? parentHight : 300;
        parentObject.GetComponent<RectTransform>().sizeDelta = new Vector2(265f, parentHight);
        for (int i = 0; i < listName.Count; i++)
        {
            GameObject go = Instantiate(taskItem,parentObject);
            go.transform.parent = parentObject.transform;
            Vector2 size = go.GetComponent<RectTransform>().rect.size;
            //go.GetComponent<RectTransform>().localScale= new Vector3(1,1,1);
            //go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
            //go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
            go.GetComponent<RectTransform>().sizeDelta = new Vector2(250f, hight);
            go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-8f, -yPos - i * (hight + 2f), 0f);
            Text tt = go.transform.GetChild(0).GetComponent<Text>();
            tt.text = listName[i].title;
            TaskItemManager item = go.GetComponent<TaskItemManager>();
            item.taskDetail = listName[i].task_detail;

        }
    }


    // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
    // 返回token示例
    private String TOKEN;

    // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
    private String clientId = "rwOVja8cR2LiQZ3pM9uZxGkl";
    // 百度云中开通对应服务应用的 Secret Key
    private String clientSecret = "qisd2MqQiDuSlZKOvVsg1deHHk1bUX3c";

    public string imagePath;

    [System.Serializable]
    public class TokenModel
    {
        public string refresh_token;
        public string expires_in;
        public string scope;
        public string session_key;
        public string access_token;
        public string session_secret;
    }

    public String getAccessToken()
    {
        String authHost = "https://aip.baidubce.com/oauth/2.0/token";
        HttpClient client = new HttpClient();
        List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
        paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
        paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
        paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

        HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
        String result = response.Content.ReadAsStringAsync().Result;
        TokenModel tokenModel= JsonUtility.FromJson<TokenModel>(result);
        return tokenModel.access_token!=null? tokenModel.access_token:"";
    }
    /// <summary>
    /// Returns the contents of the specified image file as a byte array.
    /// </summary>
    private byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

        BinaryReader binaryReader = new BinaryReader(fileStream);

        return binaryReader.ReadBytes((int)fileStream.Length);
    }
    /// <summary>
    /// Bite array of the image to submit for analysis
    /// </summary>
    [HideInInspector] public byte[] imageBytes;




    public string UrlEncode(string str)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
        for (int i = 0; i < byStr.Length; i++)
        {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }
        return (sb.ToString());
    }

    [System.Serializable]
    public class ImageShibie
    {
        public string log_id;
        public int result_num;
        public ImageShibieResult[] result;
    }


    [System.Serializable]
    public class ImageShibieResult
    {
        public double score;
        public string root;
        public BaikeInfo baike_info;
        public string keyword;
    }

    [System.Serializable]
    public class BaikeInfo
    {
        public string baike_url;
        public string image_url;
        public string description;
    }




    // 图像主体检测
    public IEnumerator AnalyseLastImageCaptured()
    {
        Debug.Log("Analyzing...");

        WWWForm webForm = new WWWForm();
        string host = "https://aip.baidubce.com/rest/2.0/image-classify/v2/advanced_general";
        imageBytes = GetImageAsByteArray(imagePath);
        webForm.AddField("access_token", TOKEN);
        webForm.AddField("image", Convert.ToBase64String(imageBytes));
        webForm.AddField("baike_num", "10");
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(host, webForm))
        {
            // Gets a byte array out of the saved image

            unityWebRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();


            // Send the request
            yield return unityWebRequest.SendWebRequest();

            string jsonResponse = unityWebRequest.downloadHandler.text;
            ImageShibie imageShibie= JsonUtility.FromJson<ImageShibie>(jsonResponse);
            Debug.Log("response: " + jsonResponse);
            if (imageShibie!=null&&imageShibie.result_num>0)
            {
                ResultsLabel.instance.SetTagsToLastLabel(imageShibie.result[0].keyword, "");
            }
            else
            {
                ResultsLabel.instance.SetTagsToLastLabel("未识别出结果", "");
            }
           
        }
    }
}
