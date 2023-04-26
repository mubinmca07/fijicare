using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using FijiCareApiCall;
using Fijicare.Helper;

public abstract class APIService
{
    static CancellationTokenSource CancellationTokenSource;
    public static string API_Name = null;
    static APIService()
    {
        CancellationTokenSource = new CancellationTokenSource();

    }

    static HttpClient Client
    {
        get
        {
            HttpClient client;

            if (Device.RuntimePlatform == Device.Android)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                client = new HttpClient(clientHandler)

                {
                    DefaultRequestHeaders = {
                    CacheControl = CacheControlHeaderValue.Parse("no-cache, no-store, must-revalidate"),
                    Pragma = { NameValueHeaderValue.Parse("no-cache") }
                    }
                };

                if (Settings.UserServiceAPI == "User")
                {
                    client.BaseAddress = new Uri(Constant.BASE_API_USER);

                }
                else if (Settings.UserServiceAPI == "Auth")
                {
                    client.BaseAddress = new Uri(Constant.BASE_API_AUTH);

                }
                else
                {
                    client.BaseAddress = new Uri(Constant.BASE_API_USER);

                }

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.CacheControl.NoCache = true;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (Settings.APISecure.Equals("1"))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Authorization);
#if DEBUG
                    Debug.WriteLine("Token ========= {0}", Settings.Authorization);
#endif

                }
                return client;

            }
            else
            {
                client = new HttpClient(new System.Net.Http.HttpClientHandler())
                {
                    DefaultRequestHeaders = {
                    CacheControl = CacheControlHeaderValue.Parse("no-cache, no-store, must-revalidate"),
                    Pragma = { NameValueHeaderValue.Parse("no-cache") }
                    }
                };
                if (Settings.UserServiceAPI == "User")
                {
                    client.BaseAddress = new Uri(Constant.BASE_API_USER);

                }
                else if (Settings.UserServiceAPI == "Auth")
                {
                    client.BaseAddress = new Uri(Constant.BASE_API_USER);

                }
                else
                {
                    client.BaseAddress = new Uri(Constant.BASE_API_USER);

                }
                //client.Timeout = TimeSpan.FromSeconds(Constant.API_TIMEOUT);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.CacheControl.NoCache = true;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (Settings.APISecure.Equals("1"))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Authorization);
#if DEBUG
                    Debug.WriteLine("Token ========= {0}", Settings.Authorization);
#endif

                }
                return client;
            }
        }
    }

    protected static async Task<TModel> POST<TModel>(string url,
     object param = null,
     bool canLoadFromCache = false,
     bool cachePriority = false)
    {

        var result = default(TModel);

        if (cachePriority)
        {
            if (CacheService.TryLoad(url, out result))
            {
                if (result != null)
                    return result;
            }
        }

        if (param == null)
            param = new object();


        var json = JsonConvert.SerializeObject(param);

        var objdata = new StringContent(json);

        objdata.Headers.ContentType = new MediaTypeHeaderValue("application/json");


        try
        {
            var response = await Client.PostAsync(url, objdata, CancellationTokenSource.Token);
            var content = await response.Content.ReadAsStringAsync();


#if DEBUG
            DebugHelper.ResponseData(json, content);
           
#endif
            JObject entity = null;

            try
            {
                entity = JObject.Parse(content);

            }
            catch (Exception ex)
            {
#if DEBUG
                DebugHelper.WriteLineError(ex);
#endif
                var rss = JObject.Parse(content);
                if (rss["code"].ToString() == "499")
                {

                    return result;
                }

            }

            if (response.IsSuccessStatusCode)
            {
                try
                {

                   // Debug.WriteLine("request ============ {0} response ======", url, content);
                    result = JsonConvert.DeserializeObject<TModel>(entity.ToString());

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("APIService" + " POST() ===> " + ex.Message + "\n" + ex.StackTrace, true);
                }
            }
            else
            {
                //CacheService.TryLoad(url, out result);

            }

            if (canLoadFromCache)
            {
                CacheService.Save(result, url);
            }
        }
        catch (OperationCanceledException excption)
        {
#if DEBUG
            DebugHelper.WriteLineError(excption);
#endif
            if (canLoadFromCache)
            {
                CacheService.TryLoad(url, out result);
#if DEBUG
                DebugHelper.WriteLineError(excption);
#endif
            }
        }
        catch (Exception excption)
        {
#if DEBUG
             DebugHelper.WriteLineError(excption);
#endif

            if (canLoadFromCache)
            {
                CacheService.TryLoad(url, out result);
            }
        }

        return result;


    }



    protected static async Task<TModel> GET<TModel>(string url,
        bool canLoadFromCache = true,
        bool cachePriority = false)
    {

        Debug.WriteLine("url ============ {0}", url);


        var result = default(TModel);
        if (cachePriority)
        {
            if (CacheService.TryLoad(url, out result))
            {
                if (result != null)
                {
                    return result;
                }
            }
        }

        try
        {
            var response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, CancellationTokenSource.Token);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                JObject rss = JObject.Parse(content);

                result = JsonConvert.DeserializeObject<TModel>(rss.ToString());
            }
            else
            {
                CacheService.TryLoad(url, out result);
            }

            if (canLoadFromCache)
            {
                CacheService.Save(result, url);
            }
        }
        catch (OperationCanceledException)
        {
            if (canLoadFromCache)
            {
                CacheService.TryLoad(url, out result);
            }
        }
        catch (Exception ex)
        {
            if (canLoadFromCache)
                CacheService.TryLoad(url, out result);
        }
        return result;
    }


    public static void CancelAll()
    {
        CancellationTokenSource.Cancel();
        CancellationTokenSource = new CancellationTokenSource();
    }

    



}
