using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BOC.Models
{
    public class DataAPI
    {
        //Get List Object 
        //public async Task<List<T>> _GetListOjectAPI<T>(string uri,
        //                                              string Token,
        //                                              HttpMethod method,
        //                                              bool _ConfigureAwait,
        //                                              string _GetObject,
        //                                              params string[] _arrays) 
        //                                              where T : class
        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();

        //        //if parameters are even
        //        if (_arrays.Length % 2 == 0)
        //        {
        //            //if there are more 2 pairs of parameters
        //            if (_arrays.Length > 2)
        //            {
        //                for (int i = 1; i <= _arrays.Length / 2; i++)
        //                {
        //                    _parameters.Add(new KeyValuePair<string, string>(_arrays[i], _arrays[i + 1]));

        //                }
        //            }
        //            else
        //            {
        //                _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
        //            }
        //        }
        //        var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
        //        var Data = JObject.Parse(_Content);
        //        //var ConvertToObject = JsonConvert.SerializeObject(Data);
        //        System.Collections.Generic.List<T> deserializeData = JsonConvert.DeserializeObject<List<T>>(Data[_GetObject].ToString());
        //        return deserializeData;
        //    }
        //}
        //Get Object



        public async Task<List<T>> GetListOjectAPI<T>(string uri,
                                                     string Token,
                                                     HttpMethod method,
                                                     bool _ConfigureAwait,
                                                     string _GetObject,
                                                     params string[] _arrays)
                                                     where T : class
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
                var _parameters = new List<KeyValuePair<string, string>>();

                //if parameters are even
                if (_arrays.Length % 2 == 0)
                {
                    //if there are more 2 pairs of parameters
                    if (_arrays.Length > 2)
                    {
                        for (int i = 0; i < _arrays.Length; i++)
                        {
                            if (i % 2 != 0 && i != 0)
                            {
                                _parameters.Add(new KeyValuePair<string, string>(_arrays[i - 1], _arrays[i]));
                            }
                       }

                    }
                    else
                    {
                        _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
                    }
                }
                var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
                string _Content;
                HttpResponseMessage res;
                res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
                _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
                var Data = JObject.Parse(_Content);
                //var ConvertToObject = JsonConvert.SerializeObject(Data);
                System.Collections.Generic.List<T> deserializeData = JsonConvert.DeserializeObject<List<T>>(Data[_GetObject].ToString());
                return deserializeData;
            }
        }

        //public async Task<T> GetObjectAPI<T>(string uri,
        //                                     string Token,
        //                                     HttpMethod method,
        //                                     bool _ConfigureAwait,
        //                                     string _GetObject,
        //                                     params string[] _arrays) 
        //                                     where T : class
        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        //if parameters are even
        //        if (_arrays.Length % 2 == 0)
        //        {
        //            //if there are more 2 pairs of parameters
        //            if (_arrays.Length > 2)
        //            {
        //                for (int i = 1; i <= _arrays.Length / 2; i++)
        //                {
        //                    _parameters.Add(new KeyValuePair<string, string>(_arrays[i], _arrays[i + 1]));

        //                }
        //            }
        //            else
        //            {
        //                _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
        //            }
        //        }
        //        var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
        //        var Data = JObject.Parse(_Content);
        //        //var ConvertToObject = JsonConvert.SerializeObject(Data);
        //        T deserializeData = JsonConvert.DeserializeObject<T>(Data[_GetObject].ToString());
        //        return deserializeData;
        //    }
        //}


        public async Task<T> GetObjectAPI<T>(string uri,
                                            string Token,
                                            HttpMethod method,
                                            bool _ConfigureAwait,
                                            string _GetObject,
                                            params string[] _arrays)
                                            where T : class
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
                var _parameters = new List<KeyValuePair<string, string>>();
                //if parameters are even
                if (_arrays.Length % 2 == 0)
                {
                    //if there are more 2 pairs of parameters
                    if (_arrays.Length > 2)
                    {
                        for (int i = 0; i < _arrays.Length; i++)
                        {
                            if (i % 2 != 0 && i != 0)
                            {
                                _parameters.Add(new KeyValuePair<string, string>(_arrays[i - 1], _arrays[i]));
                            }
                        }
                    }
                    else
                    {
                        _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
                    }
                }
                var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
                string _Content;
                HttpResponseMessage res;
                res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
                _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
                var Data = JObject.Parse(_Content);
                //var ConvertToObject = JsonConvert.SerializeObject(Data);
                T deserializeData = JsonConvert.DeserializeObject<T>(Data[_GetObject].ToString());
                return deserializeData;
            }
        }




        //public async Task<T> _GetObjectAPI<T>(string uri,
        //                                   string Token,
        //                                   HttpMethod method,
        //                                   bool _ConfigureAwait,
        //                                   string _GetObject,
        //                                   params object[] _arrays)
        //                                   where T : class
        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //       var _parameters = new List<KeyValuePair<object, object>>();
        //        //if parameters are even
        //        if (_arrays.Length % 2 == 0)
        //        {
        //            //if there are more 2 pairs of parameters
        //            if (_arrays.Length > 2)
        //            {
        //                for (int i = 0; i < _arrays.Length; i++)
        //                {
        //                    if (i % 2 != 0 && i != 0)
        //                    {
        //                        _parameters.Add(new KeyValuePair<object, object>(_arrays[i - 1], _arrays[i]));
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                _parameters.Add(new KeyValuePair<object, object>(_arrays[0], _arrays[1]));
        //            }
        //        }
        //        var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
        //        var Data = JObject.Parse(_Content);
        //        //var ConvertToObject = JsonConvert.SerializeObject(Data);
        //        T deserializeData = JsonConvert.DeserializeObject<T>(Data[_GetObject].ToString());
        //        return deserializeData;
        //    }
        //}







        ////Get Object
        //public async Task<eLib_Comment_New> GetStringAPI(string uri,
        //                                     string Token,
        //                                     HttpMethod method,
        //                                     bool _ConfigureAwait,
        //                                     params string[] _arrays)

        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        //if parameters are even
        //        if (_arrays.Length % 2 == 0)
        //        {
        //            //if there are more 2 pairs of parameters
        //            if (_arrays.Length > 2)
        //            {
        //                for (int i = 1; i <= _arrays.Length / 2; i++)
        //                {
        //                    _parameters.Add(new KeyValuePair<string, string>(_arrays[i], _arrays[i + 1]));

        //                }
        //            }
        //            else
        //            {
        //                _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
        //            }
        //        }
        //        var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
        //        var Data = JObject.Parse(_Content);
        //        //var ConvertToObject = JsonConvert.SerializeObject(Data);
        //        eLib_Comment_New deserializeData = JsonConvert.DeserializeObject<eLib_Comment_New>(Data.ToString());

        //        return deserializeData;
        //    }
        //}

        //public async Task<eLib_Comment_New> _GetStringAPI(string uri,
        //                                   string Token,
        //                                   HttpMethod method,
        //                                   bool _ConfigureAwait,
        //                                   List<KeyValuePair<string, string>> lst)

        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        //var _parameters = new List<KeyValuePair<string, string>>();
        //        //if parameters are even

        //        //_parameters.Add(new KeyValuePair<string, string>("DocProfileID", "5"));
        //        //_parameters.Add(new KeyValuePair<string, string>("Comment", "qewqeddqdq"));
        //        //if parameters are even
        //        //if (_arrays.Length % 2 == 0)
        //        //{
        //        //    //if there are more 2 pairs of parameters
        //        //    if (_arrays.Length > 2)
        //        //    {
        //        //        for (int i = 0; i < _arrays.Length / 2; i++)
        //        //        {
        //        //            _parameters.Add(new KeyValuePair<string, string>(_arrays[i], _arrays[i + 1]));

        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
        //        //    }
        //        //}


        //        var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(lst) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
        //        var Data = JObject.Parse(_Content);
        //        //var ConvertToObject = JsonConvert.SerializeObject(Data);
        //        eLib_Comment_New deserializeData = JsonConvert.DeserializeObject<eLib_Comment_New>(Data.ToString());

        //        return deserializeData;
        //    }
        //}


        //public async Task<eLib_Comment_New> GetStringAPI(string uri,
        //                                 string Token,
        //                                 HttpMethod method,
        //                                 bool _ConfigureAwait,
        //                                 params string[] _arrays)

        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        //if parameters are even

        //        //_parameters.Add(new KeyValuePair<string, string>("DocProfileID", "5"));
        //        //_parameters.Add(new KeyValuePair<string, string>("Comment", "qewqeddqdq"));
        //        //if parameters are even
        //        if (_arrays.Length % 2 == 0)
        //        {
        //            //if there are more 2 pairs of parameters
        //            if (_arrays.Length > 2)
        //            {
        //                for (int i = 0; i < _arrays.Length ; i++)
        //                {
        //                    if (i % 2 != 0 && i != 0)
        //                    {
        //                        _parameters.Add(new KeyValuePair<string, string>(_arrays[i-1], _arrays[i]));
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
        //            }
        //        }
        //        var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
        //        var Data = JObject.Parse(_Content);
        //        //var ConvertToObject = JsonConvert.SerializeObject(Data);
        //        eLib_Comment_New deserializeData = JsonConvert.DeserializeObject<eLib_Comment_New>(Data.ToString());
        //        return deserializeData;
        //    }
        //}

        public async Task<eLib_Comment_New> GetStringAPI(string uri,
                                      string Token,
                                      HttpMethod method,
                                      bool _ConfigureAwait,
                                      params string[] _arrays)

        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
                var _parameters = new List<KeyValuePair<string, string>>();
                //if parameters are even

                //_parameters.Add(new KeyValuePair<string, string>("DocProfileID", "5"));
                //_parameters.Add(new KeyValuePair<string, string>("Comment", "qewqeddqdq"));
                //if parameters are even
                if (_arrays.Length % 2 == 0)
                {
                    //if there are more 2 pairs of parameters
                    if (_arrays.Length > 2)
                    {
                        for (int i = 0; i < _arrays.Length; i++)
                        {
                            if (i % 2 != 0 && i != 0)
                            {
                                _parameters.Add(new KeyValuePair<string, string>(_arrays[i - 1], _arrays[i]));
                            }
                        }
                    }
                    else
                    {
                        _parameters.Add(new KeyValuePair<string, string>(_arrays[0], _arrays[1]));
                    }
                }
                var req = new HttpRequestMessage(method, uri) { Content = new FormUrlEncodedContent(_parameters) };
                string _Content;
                HttpResponseMessage res;
                res = await _httpClient.SendAsync(req).ConfigureAwait(_ConfigureAwait);
                _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(_ConfigureAwait);
                var Data = JObject.Parse(_Content);
                //var ConvertToObject = JsonConvert.SerializeObject(Data);
                eLib_Comment_New deserializeData = JsonConvert.DeserializeObject<eLib_Comment_New>(Data.ToString());
                return deserializeData;
            }
        }






        //Get List Object Without Parameters
        public async Task<List<T>> GetListAPIWithoutParams<T>(string uri,
                                                             string Token,
                                                             HttpMethod method,
                                                             bool _ConfigureAwait,
                                                             string _GetObject)
                                                             where T : class
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
                var req = new HttpRequestMessage(method, uri);
                string _content;
                HttpResponseMessage res;
                res = await _httpClient.SendAsync(req).ConfigureAwait(false);
                _content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                var Data = JObject.Parse(_content);
                //var ConvertToObject = JsonConvert.SerializeObject(Data);
                List<T> deserializeData = JsonConvert.DeserializeObject<List<T>>(Data[_GetObject].ToString());
                return deserializeData;
            }
        }
        //Get Object Without Parameters
        public async Task<T> GetObjectAPIWithoutParams<T>(string uri,
                                                         string Token,
                                                         HttpMethod method, 
                                                         bool _ConfigureAwait,
                                                         string _GetObject)
                                                         where T : class
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
                var req = new HttpRequestMessage(method, uri);
                string _content;
                HttpResponseMessage res;
                res = await _httpClient.SendAsync(req).ConfigureAwait(false);
                _content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                var Data = JObject.Parse(_content);
                //var ConvertToObject = JsonConvert.SerializeObject(Data);
                T deserializeData = JsonConvert.DeserializeObject<T>(Data["Data"].ToString());
                return deserializeData;
            }
        }




        public async Task<T> _GetObjecReturn<T>(string uri,
                                             string Token,
                                             string param1,
                                             string _param1) where T : class
        {
            using (HttpClient _httpclient = new HttpClient())
            {
                _httpclient.DefaultRequestHeaders.Add("Authorization", Token);
                var _parameters = new List<KeyValuePair<string, string>>();
                _parameters.Add(new KeyValuePair<string, string>(param1, _param1));
                var req = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(_parameters) };
                string _content;
                HttpResponseMessage res;
                res = await _httpclient.SendAsync(req).ConfigureAwait(false);
                _content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                var oData = JObject.Parse(_content);
                var ConvertToObject = JsonConvert.SerializeObject(oData);
                T deserializeData = JsonConvert.DeserializeObject<T>(oData.ToString());
                return deserializeData;
            }
        }




















    }
}
