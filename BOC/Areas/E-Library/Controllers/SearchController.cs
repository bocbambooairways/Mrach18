using BOC.Areas.E_Library.Models;
using BOC.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;



namespace BOC.Areas.E_Library.Controllers
{
    [Area("E-Library")]//Declare Area
    public class SearchController : Controller
    {
        //
        public UriConfig UriConfig { get; }
        //List<SearchResult> _SearchData;
        public SearchController(Microsoft.Extensions.Options.IOptions<UriConfig> _UriConfig)
        {
            UriConfig = _UriConfig.Value;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NoSearchResult()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Search _model)
        {

            return View();
        }
        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult DocumentList()
        {

            string DocProfileID = HttpContext.Request.Query["DocProfileID"].ToString();
            ViewData["DocProfileID"] = DocProfileID;
            string uri_GetSearch_eLib_Profile_Detail = UriConfig.uri_GetSearch_eLib_Profile_Detail;
            
            //Get List document List
            ProfileDetail GetSearch_eLib_Issue_Department_List = new DataAPI().GetObjectAPI<ProfileDetail>(uri_GetSearch_eLib_Profile_Detail,
                                      HttpContext.Session.GetString("Token"),HttpMethod.Post,false,"Data",
                                     "DocProfileID",
                                     DocProfileID ).Result;
            if(GetSearch_eLib_Issue_Department_List!=null)
            if(GetSearch_eLib_Issue_Department_List.Attached_Files != null)
            foreach (var items in GetSearch_eLib_Issue_Department_List.Attached_Files)
            {
                var item = items.OriginalFileName;
            }
            //GetSearch_eLib_Issue_Department_List.ReadStatus = "unread";
            //Get List Comment API
            string uri_GetSearch_eLib_Comment_Get = UriConfig.uri_GetSearch_eLib_Comment_Get;
            List<eLib_Comment_Get> Get_List_eLib_Comment_Get = new DataAPI().
            GetListOjectAPI<eLib_Comment_Get>(uri_GetSearch_eLib_Comment_Get,
            HttpContext.Session.GetString("Token"),
            HttpMethod.Post,
            false,
            "Data",
            "DocProfileID",
            DocProfileID).Result;


            if (Get_List_eLib_Comment_Get != null) 
            ViewData["List_eLib_Comment_Get"] = Get_List_eLib_Comment_Get;



            string DocCommentID = HttpContext.Request.Query["DocCommentID"].ToString();
            string Comment = HttpContext.Request.Query["Comment"].ToString();
            //if (DocCommentID != string.Empty)
            //{


            return View(GetSearch_eLib_Issue_Department_List);
        }
        [HttpPost]
        public IActionResult DocumentList(int id,string DocProfileID,string comment,string comment_new,string[] comment_reply,string DocCommentID)
        {
            string _comment_reply = string.Empty;
            foreach(string item in comment_reply)
            {
                if(item != null)
                {
                    _comment_reply = item;
                }
            }
            // API DocumentList         
            string uri_GetSearch_eLib_Profile_Detail = UriConfig.uri_GetSearch_eLib_Profile_Detail;
            ProfileDetail GetSearch_eLib_Issue_Department_List = new DataAPI().GetObjectAPI<ProfileDetail>(uri_GetSearch_eLib_Profile_Detail,
                                     HttpContext.Session.GetString("Token"), HttpMethod.Post, false, "Data",
                                    "DocProfileID",
                                    DocProfileID).Result;


            if (int.TryParse(comment, out int value))
            {
              
                        string uri_GetSearch_eLib_Comment_Reply = UriConfig.uri_GetSearch_eLib_Comment_Reply;
                eLib_Comment_New Get_eLib_Comment_Reply = new DataAPI().GetStringAPI(uri_GetSearch_eLib_Comment_Reply,
                                                                                 HttpContext.Session.GetString("Token"),
                                                                                 HttpMethod.Post,
                                                                                 false,
                                                                                 "DocCommentID",
                                                                                  comment,
                                                                                 "Comment",
                                                                                 _comment_reply).Result;
            }

            else
            {

                if (comment == "New")
                {

                    string uri_GetSearch_eLib_Comment_New = UriConfig.uri_GetSearch_eLib_Comment_New;
                    eLib_Comment_New Get_eLib_Comment_New = new DataAPI().GetStringAPI(uri_GetSearch_eLib_Comment_New,
                                                                                     HttpContext.Session.GetString("Token"),
                                                                                     HttpMethod.Post,
                                                                                     false,
                                                                                     "DocProfileID",
                                                                                     DocProfileID,
                                                                                     "Comment",
                                                                                     comment_new).Result;
                }
                if(comment == "QA")
                {
                    //return RedirectToAction("QA", "Search", new { area = "E-Library" });
                    return Redirect("/E-Library/Search/QA?DocProfileID=" + DocProfileID);
                       
                }
                
            }

            string uri_GetSearch_eLib_Comment_Get = UriConfig.uri_GetSearch_eLib_Comment_Get;
            List<eLib_Comment_Get> Get_List_eLib_Comment_Get = new DataAPI().
            GetListOjectAPI<eLib_Comment_Get>(uri_GetSearch_eLib_Comment_Get,
            HttpContext.Session.GetString("Token"),
            HttpMethod.Post,
            false,
            "Data",
            "DocProfileID",
            DocProfileID).Result;

            if (Get_List_eLib_Comment_Get != null)
                ViewData["List_eLib_Comment_Get"] = Get_List_eLib_Comment_Get;

            return View(GetSearch_eLib_Issue_Department_List);
        }
      [HttpGet]
        public IActionResult QA(string DocProfileID)
        {
       string uri_eLib_Confirm_read_Understand = UriConfig.uri_eLib_Confirm_read_Understand;
       eLib_Confirm_read_Understand  Get_List_eLib_Comment_Get = new DataAPI().GetObjectAPI<eLib_Confirm_read_Understand>
                       (uri_eLib_Confirm_read_Understand,
                       HttpContext.Session.GetString("Token"),
                       HttpMethod.Post,
                       false,
                       "Data",
                       "DocProfileID",
                       DocProfileID).Result;
            //QA Deleted dATA null
            if (Get_List_eLib_Comment_Get== null)
            {

                return Redirect("/E-Library/Search/DocumentList?DocProfileID=" + DocProfileID);
            }
            //Ls_QA null
            else
            {
                if (Get_List_eLib_Comment_Get.ls_QA.Count == 0)
                    return Redirect("/E-Library/Search/DocumentList?DocProfileID=" + DocProfileID);
            }
          
            return View(Get_List_eLib_Comment_Get);
        }
        [HttpPost]
        public IActionResult QA(string[] CheckAnswer,string DocProfileID)
        {
            string _CheckAnswer = string.Empty;
            string _Answer = string.Empty;
            string _TotalAnswer = string.Empty;
            string _check = string.Empty;
            string _so = string.Empty;
            string _pattern = "A_1";
            string question1 = string.Empty;



            string uri_eLib_Confirm_read_Understand = UriConfig.uri_eLib_Confirm_read_Understand;
            eLib_Confirm_read_Understand Get_List_eLib_Comment_Get = new DataAPI().GetObjectAPI<eLib_Confirm_read_Understand>
                            (uri_eLib_Confirm_read_Understand,
                            HttpContext.Session.GetString("Token"),
                            HttpMethod.Post,
                            false,
                            "Data",
                            "DocProfileID",
                            DocProfileID).Result;


            List<ls_QA> Data = new List<ls_QA>();
           
            string[] answerquestion = new string[Get_List_eLib_Comment_Get.ls_QA.Count];

            //question
            for (int z = 0; z < Get_List_eLib_Comment_Get.ls_QA.Count; z++)
            {



                //from user answer
                for (int n = 0; n < CheckAnswer.Length; n++)
                {

                    if (ReturnValueFromString(CheckAnswer[n]).Equals((z+1).ToString()))
                    {

                        if (CheckAnswer[n].Contains("A"))
                        {
                            _CheckAnswer = _CheckAnswer + "A";
                        }
                        else if (CheckAnswer[n].Contains("B"))
                        {
                            _CheckAnswer = _CheckAnswer + "B";
                        }
                        else if (CheckAnswer[n].Contains("C"))
                        {
                            _CheckAnswer = _CheckAnswer + "C";
                        }
                        else if (CheckAnswer[n].Contains("D"))
                        {
                            _CheckAnswer = _CheckAnswer + "D";
                        }


                    }

                    _Answer = _CheckAnswer;
                }

                if (_Answer != string.Empty)
                {
                    //bring in array to result
                    for (int i = 1; i <= 4; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                if (_Answer.Contains("A"))
                                {
                                    _TotalAnswer = _TotalAnswer + "A";
                                }
                                else
                                {
                                    _TotalAnswer = _TotalAnswer + "0";

                                }
                                break;


                            case 2:
                                if (_Answer.Contains("B"))
                                {
                                    _TotalAnswer = _TotalAnswer + "B";
                                }
                                else
                                {
                                    _TotalAnswer = _TotalAnswer + "0";

                                }
                                break;


                            case 3:
                                if (_Answer.Contains("C"))
                                {
                                    _TotalAnswer = _TotalAnswer + "C";
                                }
                                else
                                {
                                    _TotalAnswer = _TotalAnswer + "0";

                                }
                                break;


                            case 4:
                                if (_Answer.Contains("D"))
                                {
                                    _TotalAnswer = _TotalAnswer + "D";
                                }
                                else
                                {
                                    _TotalAnswer = _TotalAnswer + "0";

                                }
                                break;
                        }

                    }

                    answerquestion[z] = _TotalAnswer;
                    _CheckAnswer = string.Empty;
                    _TotalAnswer = string.Empty;
                }
                else
                {
                    answerquestion[z] = "0000";

                }


                //string t = Get_List_eLib_Comment_Get.ls_QA[z].A_Answer.ToString();
                Data.Add(new ls_QA
                {
                    DocProfileID = DocProfileID,
                    QADetailID = Get_List_eLib_Comment_Get.ls_QA[z].QADetailID,
                    UserID = Get_List_eLib_Comment_Get.ls_QA[z].UserID,
                    User_Answer = answerquestion[z]
                });

            }

            var _JsonObjectAnswer = JsonConvert.SerializeObject(Data);

            string uri_eLib_QuestionAnswer_Update = UriConfig.uri_eLib_QuestionAnswer_Update;
            eLib_Confirm_read_Understand Get_eLib_QuestionAnswer_Update = new DataAPI()._GetObjecReturn<eLib_Confirm_read_Understand>
                           (uri_eLib_QuestionAnswer_Update,
                           HttpContext.Session.GetString("Token"),
                           "Data",
                           _JsonObjectAnswer).Result;


            ViewBag.Result = Get_eLib_QuestionAnswer_Update.Data;
            return View(Get_List_eLib_Comment_Get);
        }
        public string ReturnValueFromString(string _pattern)
        {
            string result = string.Empty;
            if(_pattern!=null)
            for (int i = 0; i < _pattern.Length; i++)
            {
                if (_pattern[i].ToString() == "_")
                {
                    result = _pattern.Substring(i + 1, _pattern.Length - (i + 1)).ToString();
                }
            }
            return result;  
        }
        [HttpGet]
        public IActionResult SearchResult(int id, Search model)
        {
            //string test = model.isdn;
            //string test1 = model.KeySearch;
            //bool Unread = model.CheckRead;
            //bool Newst = model.CheckNews;
            //string KeySearch = model.KeySearch;
            //string Author = model.Author;
            //string Author = model.isdn;
            //ViewData["SearchResult"] = "Display";
            //ViewData["KeySearch"] = model.KeySearch;
            //ViewData["Author"] = model.Author;
            //ViewData["ISBN"] = model.isdn;
            //ViewData["DocDivID"] = model.DocDivID;
            //ViewData["PublishID"] = model.PublishID;
            //ViewData["UnRead"] = (model.CheckRead == true ? "YES" : "NO");
            //ViewData["Newest"] = (model.CheckNews == true ? "YES" : "NO");
            //ViewData["FromDate"] = model.ReceivedDate;
            //ViewData["ToDate"] = model.PublishDate;
            //string uri_GetSearch_eLib_Search = UriConfig.uri_GetSearch_eLib_Search;
            //IEnumerable<SearchResult>  GetSearch_eLib_Division_List = GetData1(uri_GetSearch_eLib_Search,
            //                          HttpContext.Session.GetString("Token"),
            //                         "UnRead",
            //                         "NO",
            //                         "Newest",
            //                         "YES").Result;
            //HttpContext.Session.SetComplexData("Session_GetSearch_eLib_Division_List", model);



            //More
            string uri_GetSearch_eLib_Search = UriConfig.uri_GetSearch_eLib_Search;
            //List<dynamic> GetSearch_eLib_Division_List = GetDataForGrid(uri_GetSearch_eLib_Search,
            //                          HttpContext.Session.GetString("Token"),
            //                         "UnRead",
            //                         "NO",
            //                         "Newest",
            //                         "YES",
            //                         "KeySearch",
            //                         model.KeySearch,
            //                         "Author",
            //                         model.Author,
            //                         "ISBN",
            //                         model.isdn,
            //                         "DocDivID",
            //                         model.DocDivID,
            //                         "PublishID",
            //                         model.PublishID,
            //                         "FromDate",
            //                        model.ReceivedDate,
            //                         "ToDate",
            //                         model.PublishDate).Result;


            List<SearchResult> GetSearch_eLib_Division_List = new DataAPI().GetListOjectAPI<SearchResult>(uri_GetSearch_eLib_Search,
                                                            HttpContext.Session.GetString("Token"),
                                                            HttpMethod.Post,false,"Data",
                                                            "UnRead",
                                                            (model.CheckRead == true ? "YES" : "NO"),
                                                            "Newest",
                                                            (model.CheckNews == true ? "YES" : "NO"),
                                                            "KeySearch",
                                                             model.KeySearch,
                                                             "Author",
                                                              model.Author,
                                                              "ISBN",
                                                              model.isdn,
                                                              "DocDivID",
                                                               model.DocDivID,
                                                              "PublishID",
                                                               model.PublishID,
                                                              "FromDate",
                                                              model.ReceivedDate,
                                                              "ToDate",
                                                               model.PublishDate).Result;


            int _count = int.Parse((GetSearch_eLib_Division_List).Count.ToString());

            int i = 0;
            foreach (var item in GetSearch_eLib_Division_List)
            {
                i += 1;
                item.ID = i;
            }
            HttpContext.Session.SetListData("SearchResult", GetSearch_eLib_Division_List);
            //TempData["Search"] = GetSearch_eLib_Division_List as List<SearchResult>;
            //if (_count > 0)
            if (GetSearch_eLib_Division_List != null)
                return View();
            return RedirectToAction("NoSearchResult",
                                     "Search",
                                     new
                                     {
                                         area = "E-Library"
                                     });




        }

        [HttpPost]
        public IActionResult SearchResult(Search model)
        {
            //string test = model.isdn;
            //string test1 = model.KeySearch;
            //bool Unread = model.CheckRead;
            //bool Newst = model.CheckNews;
            //string KeySearch = model.KeySearch;
            //string Author = model.Author;
            //string Author = model.isdn;
            //ViewData["SearchResult"] = "Display";
            ViewData["KeySearch"] = model.KeySearch;
            ViewData["Author"] = model.Author;
            ViewData["ISBN"] = model.isdn;
            ViewData["DocDivID"] = model.DocDivID;
            ViewData["PublishID"] = model.PublishID;
            ViewData["UnRead"] = (model.CheckRead == true ? "YES" : "NO");
            ViewData["Newest"] = (model.CheckNews == true ? "YES" : "NO");
            ViewData["FromDate"] = model.ReceivedDate;
            ViewData["ToDate"] = model.PublishDate;
            //string uri_GetSearch_eLib_Search = UriConfig.uri_GetSearch_eLib_Search;
            //IEnumerable<SearchResult>  GetSearch_eLib_Division_List = GetData1(uri_GetSearch_eLib_Search,
            //                          HttpContext.Session.GetString("Token"),
            //                         "UnRead",
            //                         "NO",
            //                         "Newest",
            //                         "YES").Result;
            //HttpContext.Session.SetComplexData("Session_GetSearch_eLib_Division_List", model);



            //More
            string uri_GetSearch_eLib_Search = UriConfig.uri_GetSearch_eLib_Search;
            //List<dynamic> GetSearch_eLib_Division_List = GetDataForGrid(uri_GetSearch_eLib_Search,
            //                          HttpContext.Session.GetString("Token"),
            //                         "UnRead",
            //                         "NO",
            //                         "Newest",
            //                         "YES",
            //                         "KeySearch",
            //                         model.KeySearch,
            //                         "Author",
            //                         model.Author,
            //                         "ISBN",
            //                         model.isdn,
            //                         "DocDivID",
            //                         model.DocDivID,
            //                         "PublishID",
            //                         model.PublishID,
            //                         "FromDate",
            //                        model.ReceivedDate,
            //                         "ToDate",
            //                         model.PublishDate).Result;

            List<SearchResult> GetSearch_eLib_Division_List = new DataAPI().GetListOjectAPI<SearchResult>(uri_GetSearch_eLib_Search,
                                                            HttpContext.Session.GetString("Token"),
                                                            HttpMethod.Post,
                                                            false,
                                                            "Data",
                                                            "UnRead",
                                                            "NO",
                                                            "Newest",
                                                           "YES",
                                                           "KeySearch",
                                                            model.KeySearch,
                                                            "Author",
                                                            model.Author,
                                                            "ISBN",
                                                            model.isdn,
                                                            "DocDivID",
                                                            model.DocDivID,
                                                           "PublishID",
                                                            model.PublishID,
                                                           "FromDate",
                                                           model.ReceivedDate,
                                                           "ToDate",
                                                           model.PublishDate).Result;


            //int _count = int.Parse((GetSearch_eLib_Division_List).Count.ToString());

            int i = 0;
            foreach (var item in GetSearch_eLib_Division_List)
            {
                i += 1;
                item.ID = i;
            }
            HttpContext.Session.SetListData("SearchResult", GetSearch_eLib_Division_List);
            //TempData["Search"] = GetSearch_eLib_Division_List as List<SearchResult>;
            //if (_count > 0)
                if (GetSearch_eLib_Division_List != null)
                    return View();
            return RedirectToAction("NoSearchResult",
                                     "Search",
                                     new
                                     {
                                         area = "E-Library"
                                     });


            //string uri_GetSearch_eLib_Profile_Detail = UriConfig.uri_GetSearch_eLib_Profile_Detail;
            //IEnumerable<Search> GetSearch_eLib_Division_List = GetData(uri_GetSearch_eLib_Profile_Detail,
            //                          HttpContext.Session.GetString("Token"),
            //                         string.Empty,
            //                         string.Empty).Result;

        }
        //[HttpGet]
        //public object Get_eLib_Search1(DataSourceLoadOptions loadOptions)
        //{
        //    //var _model = @HttpContext.Session.GetComplexData<List<Search>>("Session_GetSearch_eLib_Division_List");
        //    string uri_GetSearch_eLib_Search = UriConfig.uri_GetSearch_eLib_Search;
        //    IEnumerable<SearchResult> GetSearch_eLib_Division_List = GetData1(uri_GetSearch_eLib_Search,
        //                              HttpContext.Session.GetString("Token"),
        //                             "UnRead",
        //                             "NO",
        //                             "Newest",
        //                             "YES").Result;

        //    return DataSourceLoader.Load (GetSearch_eLib_Division_List, loadOptions);
        //}
        //[HttpGet]
        //public object Get_eLib_Search(DataSourceLoadOptions loadOptions, string UnRead,
        //                                                                 string Newest,
        //                                                                 string KeySearch,
        //                                                                 string Author,
        //                                                                 string ISBN,
        //                                                                 string DocDivID,
        //                                                                 string PublishID,
        //                                                                 string FromDate,
        //                                                                 string ToDate)
        //{
        //    string uri_GetSearch_eLib_Search = UriConfig.uri_GetSearch_eLib_Search;
        //    IEnumerable<SearchResult> GetSearch_eLib_Division_List = GetDataForGrid(uri_GetSearch_eLib_Search,
        //                              HttpContext.Session.GetString("Token"),
        //                             "UnRead",
        //                             UnRead,
        //                             "Newest",
        //                             Newest,
        //                             "KeySearch",
        //                             KeySearch, 
        //                             "Author", 
        //                             Author, 
        //                             "ISBN", 
        //                             ISBN,
        //                             "DocDivID",
        //                             DocDivID,
        //                             "PublishID",
        //                             PublishID,
        //                             "FromDate", 
        //                             FromDate,
        //                             "ToDate",
        //                             ToDate).Result;
        //    int i = 0;
        //    foreach(var item in GetSearch_eLib_Division_List)
        //    {
        //        i += 1;
        //        item.ID = i;
        //    }
        //    HttpContext.Session.SetComplexData("loggerUser", GetSearch_eLib_Division_List);
        //    //ViewData["SearchResult"] = "NoDisplay";
        //    ViewBag.Menu1 = @HttpContext.Session.GetComplexData<List<SearchResult>>("loggerUser");
        //    var test  = @HttpContext.Session.GetComplexData<List<SearchResult>>("loggerUser");

        //    return DataSourceLoader.Load(GetSearch_eLib_Division_List, loadOptions);
        //}
        
        [HttpGet]
        public IActionResult GetSearchFolder()
        {
            string uri_GetSearch_eLib_Of_Folder = UriConfig.uri_GetSearch_eLib_Of_Folder;

            List<SearchResult> GetSearch_eLib_Of_Folder = new DataAPI().GetListOjectAPI<SearchResult>(UriConfig.uri_GetSearch_eLib_Of_Folder,
                                                            HttpContext.Session.GetString("Token"),
                                                            HttpMethod.Post,
                                                            false,
                                                            "Data",
                                                            "FolderID",
                                                            "1").Result;

            int i = 0;
            foreach (var item in GetSearch_eLib_Of_Folder)
            {
                i += 1;
                item.ID = i;
            }
            HttpContext.Session.SetListData("SearchOfFolder", GetSearch_eLib_Of_Folder);
            //TempData["Search"] = GetSearch_eLib_Division_List as List<SearchResult>;
            //if (_count > 0)
            if (GetSearch_eLib_Of_Folder != null)
                return View();


            return View(GetSearch_eLib_Of_Folder);  
        }
              
        [HttpGet]
        public object Get_eLib_Search(DataSourceLoadOptions loadOptions)
        {
            //if (TempData.ContainsKey("Search"))bbb
            //{
            //    this. _SearchData = (List<SearchResult>)TempData["Search"];

            //}
            return DataSourceLoader.Load(@HttpContext.Session.GetListData<List<SearchResult>>("SearchResult"), loadOptions);
            //return DataSourceLoader.Load(_SearchData, loadOptions);

        }


        [HttpGet]
        public object Get_eLib_Of_Folderh(DataSourceLoadOptions loadOptions)
        {
            //if (TempData.ContainsKey("Search"))bbb
            //{
            //    this. _SearchData = (List<SearchResult>)TempData["Search"];

            //}
            return DataSourceLoader.Load(@HttpContext.Session.GetListData<List<SearchResult>>("SearchOfFolder"), loadOptions);
            //return DataSourceLoader.Load(_SearchData, loadOptions);

        }
        //[HttpGet]
        //public object GetData(DataSourceLoadOptions loadOptions)
        //{
        //    string uri_GetSearch_eLib_Division_List = UriConfig.uri_GetSearch_eLib_Division_List;
        //    IEnumerable<Search> GetSearch_eLib_Division_List = GetData<Search>(uri_GetSearch_eLib_Division_List,
        //                              HttpContext.Session.GetString("Token"),
        //                             string.Empty,
        //                             string.Empty).Result;
        //    return DataSourceLoader.Load(GetSearch_eLib_Division_List, loadOptions);
        //}

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            string uri_GetSearch_eLib_Division_List = UriConfig.uri_GetSearch_eLib_Division_List;
            //IEnumerable<Search> GetSearch_GetSearch_eLib_Division_List = GetData<Search>(uri_GetSearch_eLib_Division_List,
            //                          HttpContext.Session.GetString("Token"),
            //                         string.Empty,
            //                         string.Empty).Result;

            //IEnumerable<Search> GetSearch_GetSearch_eLib_Division_List = new DataAPI().GetWebAPI<Search>(uri_GetSearch_eLib_Division_List,
            //                         HttpContext.Session.GetString("Token"), new[] { string.Empty,
            //                        string.Empty}
            //                       ).Result;
            IEnumerable<Search> GetSearch_GetSearch_eLib_Division_List = new DataAPI().GetListAPIWithoutParams<Search>(uri_GetSearch_eLib_Division_List,
                                                                      HttpContext.Session.GetString("Token"),HttpMethod.Post,false,"Data").Result;
            return DataSourceLoader.Load(GetSearch_GetSearch_eLib_Division_List, loadOptions);
        }

        [HttpGet]
        public object GetIssue(DataSourceLoadOptions loadOptions)
        {
            string uri_GetSearch_eLib_Issue_Department_List = UriConfig.uri_GetSearch_eLib_Issue_Department_List;
            //List<Search> GetSearch_eLib_Issue_Department_List = GetData<Search>(uri_GetSearch_eLib_Issue_Department_List,
            //                          HttpContext.Session.GetString("Token"),
            //                         string.Empty,
            //                         string.Empty).Result as List<Search>;
            IEnumerable<Search> GetSearch_eLib_Issue_Department_List = new DataAPI().GetListAPIWithoutParams<Search>(uri_GetSearch_eLib_Issue_Department_List,
                                                                     HttpContext.Session.GetString("Token"),
                                                                     HttpMethod.Post,
                                                                     false,
                                                                     "Data").Result;
            return DataSourceLoader.Load(GetSearch_eLib_Issue_Department_List, loadOptions);
        }


        [HttpGet]
        public IActionResult DownloadFile([FromQuery] string ServerID,string Folder, string FileName)
        {
           
            var net = new System.Net.WebClient();
            var data = net.DownloadData("http://172.19.21.64");
            var content = new System.IO.MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            var fileName = "elib_22_03_11_99181768925515673.xlsx";
            return File(content, contentType, fileName);
        }

        //public async Task<List<Search>> GetData(string uri,
        //                                  string Token,
        //                                  string param1,
        //                                  string _param1)
        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        _parameters.Add(new KeyValuePair<string, string>(param1, _param1));

        //        var req = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(false);
        //        _content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
        //        var oData = JObject.Parse(_content);
        //        var ConvertToObject = JsonConvert.SerializeObject(oData);
        //        List<Search> deserializeData = JsonConvert.DeserializeObject<List<Search>>(oData["Data"].ToString());
        //        return deserializeData;
        //    }
        //}

        //public async Task<List<T>> GetData<T>(string uri,
        //                                 string Token,
        //                                 string param1,
        //                                 string _param1) where T : class
        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        _parameters.Add(new KeyValuePair<string, string>(param1, _param1));

        //        var req = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(false);
        //        _content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
        //        var oData = JObject.Parse(_content);
        //        var ConvertToObject = JsonConvert.SerializeObject(oData);
        //        List<T> deserializeData = JsonConvert.DeserializeObject<List<T>>(oData["Data"].ToString());
        //        return deserializeData;
        //    }
        //}
        //public async Task<ProfileDetail> Get_eLib_Profile_Detail(string uri,
        //                                 string Token,
        //                                 string param1,
        //                                 string _param1) 
        //{
        //    using (HttpClient _httpclient = new HttpClient())
        //    {
        //        _httpclient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        _parameters.Add(new KeyValuePair<string, string>(param1, _param1));

        //        var req = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _content;
        //        HttpResponseMessage res;
        //        res = await _httpclient.SendAsync(req).ConfigureAwait(false);
        //        _content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
        //        var oData = JObject.Parse(_content);
        //        var ConvertToObject = JsonConvert.SerializeObject(oData);
        //        ProfileDetail deserializeData = JsonConvert.DeserializeObject<ProfileDetail>(oData["Data"].ToString());
        //        return deserializeData;
        //    }
        //}
        //public async Task<List<dynamic>> GetDataForGrid(string uri,
        //                                string Token,
        //                                string param1,
        //                                string _param1,
        //                                string param2,
        //                                string _param2,
        //                                string param3,
        //                                string _param3,
        //                                string param4,
        //                                string _param4,
        //                                string param5,
        //                                string _param5,
        //                                string param6,
        //                                string _param6, 
        //                                string param7,
        //                                string _param7,
        //                                 string param8,
        //                                string _param8,
        //                                 string param9,
        //                                string _param9)
        //{
        //    using (HttpClient _httpClient = new HttpClient())
        //    {
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", Token);
        //        var _parameters = new List<KeyValuePair<string, string>>();
        //        _parameters.Add(new KeyValuePair<string, string>(param1, _param1));
        //        _parameters.Add(new KeyValuePair<string, string>(param2, _param2));
        //        _parameters.Add(new KeyValuePair<string, string>(param3, _param3));
        //        _parameters.Add(new KeyValuePair<string, string>(param4, _param4));
        //        _parameters.Add(new KeyValuePair<string, string>(param5, _param5));
        //        _parameters.Add(new KeyValuePair<string, string>(param6, _param6));
        //        _parameters.Add(new KeyValuePair<string, string>(param7, _param7));
        //        _parameters.Add(new KeyValuePair<string, string>(param8, _param8));
        //        _parameters.Add(new KeyValuePair<string, string>(param9, _param9));

        //        var req = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(_parameters) };
        //        string _Content;
        //        HttpResponseMessage res;
        //        res = await _httpClient.SendAsync(req).ConfigureAwait(false);
        //        _Content = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
        //        var oData = JObject.Parse(_Content);
        //        var ConvertToObject = JsonConvert.SerializeObject(oData);
        //        List<dynamic> deserializeData = JsonConvert.DeserializeObject<List<dynamic>>(oData["Data"].ToString());
        //        return deserializeData;
        //    }
        //}




    }
}
