using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Fijicare.Helpers;

namespace Fijicare.Utility
{
   public  class ServiceRequestHelper
    {
        public class WebServiceResult
        {
            public string Result { get; set; }
            public int status { get; set; }
            public string message { get; set; }
        }
        public bool IsInternet()
        {
            if (CrossConnectivity.Current.IsConnected)           
                return true;
            else
            return false;
          
        }
        public async Task<WebServiceResult> GetDataHttpWebRequest(string url)
        {
            WebServiceResult res = new WebServiceResult();

            if (IsInternet())
            {
                try
                {


                   // System.Diagnostics.Debug.WriteLine("url is Status  : {0} ", url);
                    using (HttpClient client = new HttpClient())
                    {

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ContentType", "application/json");
                        try
                        {
                            Uri tempurl = new Uri(url);
                            if ((Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)))
                            {
                                HttpResponseMessage response = await client.GetAsync(new Uri(url));
                                string resultstring = response.Content.ReadAsStringAsync().Result;
                                //System.Diagnostics.Debug.WriteLine("{0} is Status  : {1} ", url, response.StatusCode);
                               // System.Diagnostics.Debug.WriteLine("{0} is Status  : {1} ", url, resultstring);

                                res.Result = resultstring;
                                res.status = Convert.ToInt32(response.StatusCode);
                            }
                        }
                        catch (Exception expestion)
                        {
                            //System.Diagnostics.Debug.WriteLine("Error {0}", expestion.Message);
                        }
                    }


                 


                }
                catch (Exception ex)
                {
                    //System.Diagnostics.Debug.WriteLine("Error {0}", ex.Message);
                   

                }              
                return res;
            }
            else
            {
                string msg= "Network unavailable, Please try again later.";
                XFToast.ShortMessage(msg);
               res.status = 0;
                //  MessageDialog dlg = new MessageDialog(res.Result);
                // await dlg.ShowAsync();
                return res;
            }

        }

        public string ComputeMD5(string str)
        {
            //var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            //IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            //var hashed = alg.HashData(buff);
            //var res = CryptographicBuffer.EncodeToHexString(hashed);
            //return res;
            //StringBuilder hash = new StringBuilder();
            //MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            //byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(str));

            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    hash.Append(bytes[i].ToString("x2"));
            //}
            //return hash.ToString();

            string theString =str;

            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5.ComputeHash(Encoding.UTF8.GetBytes(theString))
                ).Replace("-", String.Empty);
            }

            return hash.ToString().ToLower();
        }

        public async Task<WebServiceResult> SendJsonDataPostRequest(string url, Dictionary<string, object> postData)
        {
            WebServiceResult res = new WebServiceResult();          
            if (IsInternet())
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(postData);
                    System.Diagnostics.Debug.WriteLine("Post Paramenters  : {0} ", jsonString);
                    System.Diagnostics.Debug.WriteLine("url is Status  : {0} ", url);
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ContentType", "application/json");
                    HttpResponseMessage response = await client.PostAsync(new Uri(url), content);
                    string resultstring = response.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine("{0} is Status  : {1} ", url, response.StatusCode);
                    System.Diagnostics.Debug.WriteLine("{0} is Result  : {1} ", url, resultstring);
                    res.Result = resultstring;
                    res.status = Convert.ToInt32(response.StatusCode);                 
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("mubin"+ex.Message);
                    res.status = 0;

                }
                return res;
            }
            else
            {
                string msg = "Network unavailable, Please try again later.";
                XFToast.ShortMessage(msg);
                //res.Result = "Network unavailable, Please try again later.";
                res.status = 0;
                //  MessageDialog dlg = new MessageDialog(res.Result);
                // await dlg.ShowAsync();
                return res;
            }



        }

        public async Task<WebServiceResult> SendJsonDataPostRequest(string url, object postData)
        {
            WebServiceResult res = new WebServiceResult();
            if (IsInternet())
            {
                try
                {
                    string jsonString = JsonConvert.SerializeObject(postData);
                    System.Diagnostics.Debug.WriteLine("Post Paramenters  : {0} ", jsonString);
                    System.Diagnostics.Debug.WriteLine("url is Status  : {0} ", url);
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ContentType", "application/json");
                    HttpResponseMessage response = await client.PostAsync(new Uri(url), content);
                    string resultstring = response.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine("{0} is Status  : {1} ", url, response.StatusCode);
                    System.Diagnostics.Debug.WriteLine("{0} is Result  : {1} ", url, resultstring);
                    res.Result = resultstring;
                    res.status = Convert.ToInt32(response.StatusCode);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    res.status = 0;

                }
                return res;
            }
            else
            {
                string msg = "Network unavailable, Please try again later.";
                XFToast.ShortMessage(msg);
                //res.Result = "Network unavailable, Please try again later.";
                res.status = 0;
                //  MessageDialog dlg = new MessageDialog(res.Result);
                // await dlg.ShowAsync();
                return res;
            }

        }

    }
}
