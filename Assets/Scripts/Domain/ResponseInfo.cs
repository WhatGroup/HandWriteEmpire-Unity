using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ResponseInfo
{
    public const string REQUEST_SUCCESS = "success";
    public const string REQUEST_ERROR = "error";
    public const string REQUEST_FAIL = "fail";
    public string type;
    public string message;
    public string attach;
}
