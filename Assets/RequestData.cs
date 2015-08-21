using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text;
namespace Skillborn {
public static class RequestData {
    public static string[] response { get; set; }

    public static string[] Pull(string url, NameValueCollection postfields)
    {
        WebClient webclient = new WebClient();
        byte[] responsebytes = webclient.UploadValues("http://"+url, "POST", postfields);
        string responsefromserver = Encoding.UTF8.GetString(responsebytes);
        return SplitString(responsefromserver);
    }
    public static string[] SplitString(string s)
    {
        string[] strings = s.Split(':');
        return strings;
    }
}
}