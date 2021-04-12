using System;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;


namespace TDL.Logic
{
    public class LogicOfDetail
    {
        private readonly string StrS = ConfigurationManager.ConnectionStrings["StrS"].ConnectionString;
        public string Title { get; set; }
        public DateTime Nichizi { get; set; }
        public string Genre { get; set; }
        public string Contents { get; set; }
        public string Status { get; set; }

        public void BindParameters(string no)
        {
            using (SqlConnection cn = new SqlConnection(StrS))
            {
                cn.Open();
                StringBuilder sb = new StringBuilder("Select Case Genre When 0 then '仕事' when 1 then 'プライベート' end as Genre,");
                sb.Append("nichizi,title,Contents, case [status] when 0 then '新規' when 1 then '進行中' when 2 then '完了' ");
                sb.Append("when 3 then '再着手' end as [status] ,contents From ToDoList Where No = @No");
                SqlCommand Cmd = new SqlCommand(sb.ToString(), cn);
                Cmd.Parameters.AddWithValue("@No", no);

                using (SqlDataReader reader = Cmd.ExecuteReader())
                {
                    reader.Read();
                    Nichizi = (DateTime)reader["nichizi"];
                    Genre = (string)reader["Genre"];
                    Title = (string)reader["title"];
                    Contents = (string)reader["contents"];
                    Status = (string)reader["status"];
                };
            };
        }
        public static string ThrowErrorMessage(string title, string contents, string ymd)
        {
            if (Utilty.CheckStringLength(title, 50))
            {
                return "タイトルは全角50文字までで入力してください。";
            }
            if (Utilty.CheckStringLength(contents, 500))
            {
                return "コンテンツは全角500文字までで入力してください。";
            }
            return Utilty.DatetimeOrNot(ymd);

        }

    }
}