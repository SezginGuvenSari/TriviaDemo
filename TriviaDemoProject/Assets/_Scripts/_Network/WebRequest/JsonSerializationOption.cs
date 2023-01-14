using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSerializationOption : ISerializationOption
{
    #region Properties
    public string ContentType => "application/json";

    #endregion

    public  T Deserialize<T>(string text)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(text);
            Console.WriteLine($"Success: {text}");
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError($"{this}.Failed {e.Message}");
            return default;
        }
    }
}
