using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HttpClient
{
    #region References

    private readonly ISerializationOption _iSerializationOption;

    #endregion

    #region Constructor
    public HttpClient(ISerializationOption iSerializationOption) => _iSerializationOption = iSerializationOption;

    #endregion

    #region Const

    private readonly string _contentType = "Content-type";

    #endregion

    public async Task<TResultType> GetRequest<TResultType>(string url)
    {
        try
        {
            using var www = UnityWebRequest.Get(url);
            www.SetRequestHeader(_contentType, _iSerializationOption.ContentType);

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed" + www.error);
            }
            var result = _iSerializationOption.Deserialize<TResultType>(www.downloadHandler.text);
            Console.WriteLine($"Success: {www.downloadHandler.text}");
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"{nameof(GetRequest)} failed. {e.Message}");
            return default;
        }
    }
}

