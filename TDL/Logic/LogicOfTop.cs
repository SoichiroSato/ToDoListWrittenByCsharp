using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace TDL.Logic
{
    public class LogicOfTop
    {
        private static readonly string StrS = ConfigurationManager.ConnectionStrings["StrS"].ConnectionString;
        public static SqlCommand BindSelectedData(SqlConnection cn ,string nichizi,string title,string genre,string status)
        {
            
                SqlCommand Cmd = new SqlCommand
                {
                    Connection = cn
                };
                Cmd.Parameters.Clear();
                StringBuilder sb = new StringBuilder("Select No,Case Genre When 0 then '仕事' when 1 then 'プライベート' end as Genre,");
                sb.Append("nichizi,title,Contents, case [status] when 0 then '新規' when 1 then '進行中' when 2 then '完了' ");
                sb.Append("when 3 then '再着手' end as [status] From ToDoList Where Genre = @Genre and status = @status ");

                if (!nichizi.Equals(""))
                {
                    sb.Append("and nichizi = @nichizi ");
                    Cmd.Parameters.AddWithValue("@nichizi", nichizi);
                }

                if (!title.Equals(""))
                {
                    sb.Append("and Title = @Title ");
                    Cmd.Parameters.AddWithValue("@Title", title);
                }

                Cmd.Parameters.AddWithValue("@Genre", genre);
                Cmd.Parameters.AddWithValue("@status", status);
                Cmd.CommandText = sb.ToString();
                return Cmd;
           
        }
    }
}
