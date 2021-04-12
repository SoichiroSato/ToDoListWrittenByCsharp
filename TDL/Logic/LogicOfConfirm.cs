using System;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
namespace TDL.App_Code
{
    public class LogicOfConfirm
    {
        private readonly string StrS = ConfigurationManager.ConnectionStrings["StrS"].ConnectionString;

        public string Title { get; set; }

        public string Contents { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }

        public DateTime Nichizi { get; set; }

        public int IntGenre { get; set; }
        public int IntStatus { get; set; }

        public string No { get; set; }

        public static string ReturnSql(String mode)
        {
            String sql;
            if (mode.Equals("Insert"))
            {
                sql = "Insert Into ToDoList (Genre,nichizi,title,contents,status) Values(@Genre,@nichizi,@title,@contents,@status)";
            }
            else if (mode.Equals("Update"))
            {
                sql = "Update ToDoList Set Genre = @Genre,nichizi = @nichizi,title = @title,contents = @contents,status = @status where No = @No";
            }
            else if (mode.Equals("Delete"))
            {
                sql = "delete from ToDoList Where No = @No";
            }
            else
            {
                throw new Exception("モードを変更しないでください");
            }
            return sql;

        }
        public void BindParameters(string no)
        {
            using (SqlConnection cn = new SqlConnection(StrS))
            {
                cn.Open();
                StringBuilder sb = new StringBuilder("Select Case Genre When 0 then '仕事' when 1 then 'プライベート' end as Genre,");
                sb.Append("nichizi,title,Contents, case [status] when 0 then '新規' when 1 then '進行中' when 2 then '完了' ");
                sb.Append("when 3 then '再着手' end as [status] ,contents From ToDoList Where No = @No");
                SqlCommand Cmd = new SqlCommand(sb.ToString(), cn);
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@No", no);
                using (SqlDataReader reader = Cmd.ExecuteReader())
                {
                    reader.Read();
                    Title = (string)reader["title"];
                    Contents = (string)reader["contents"];
                    Status = (string)reader["status"];
                    Genre = (string)reader["Genre"];
                    Nichizi = (DateTime)reader["nichizi"];

                };
            };
        }
        public string CommitMessage(string mode)
        {
            if (mode.Equals("Delete"))
            {
                CommitOn(mode);
                return "削除が完了しました。";
            }
            else if (mode.Equals("Insert"))
            {
                CommitOn(mode);
                return "登録が完了しました。";
            }
            else
            {
                CommitOn(mode);
                return "更新が完了しました。";
            }
        }
        public void CommitOn(string mode)
        {
            using (SqlConnection cn = new SqlConnection(StrS))
            {


                cn.Open();

                SqlCommand Cmd = new SqlCommand(ReturnSql(mode), cn);
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@title", Title);
                Cmd.Parameters.AddWithValue("@contents", Contents);
                Cmd.Parameters.AddWithValue("@nichizi", Nichizi);
                Cmd.Parameters.AddWithValue("@status", IntStatus);
                Cmd.Parameters.AddWithValue("@Genre", IntGenre);
                if (mode.Equals("Delete") || mode.Equals("Update"))
                {
                    Cmd.Parameters.AddWithValue("@No", No);

                }
                Cmd.ExecuteNonQuery();
            };
        }
    }
}