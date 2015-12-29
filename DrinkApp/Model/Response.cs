using System;
using System.Collections.Generic;
using System.Text;
using DrinkApp.Utils;

namespace DrinkApp.Model
{
    public class Pager {
        public int Records_Per_Page { get; set; }
        public int Total_Record_Count { get; set; }
        public int Current_Page_Record_Count { get; set; }
        public bool Is_First_Page { get; set; }
        public bool Is_Final_Page { get; set; }
        public int Current_Page { get; set; }
        public string Current_Page_Path { get; set; }
        public int Next_Page { get; set; }
        public string Next_Page_Path { get; set; }
        public int? Previous_Page { get; set; }
        public string Previous_Page_Path { get; set; }
        public int Total_Pages { get; set; }
        public string Total_Pages_Path { get; set; }
    }

    public class Response {
        public Status Status { get; set; }
        public string Message { get; set; }
        public Pager Pager { get; private set; }
        public string Suggestion { get; set; }
        public Product Product { get; set; }
        public dynamic Result { get; set; }
    }
}
