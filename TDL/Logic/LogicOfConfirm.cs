using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace TDL.Logic
{
    public class LogicOfConfirm
    {
        private readonly string StrS = ConfigurationManager.ConnectionStrings["StrS"].ConnectionString;

        public string Title { get; set; }

        public string Contents { get; set; }
       
        public DateTime Nichizi { get; set; }

        public int Genre { get; set; }
        public int Status { get; set; }

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
        private void CommitOn(string mode)
        {
            using (SqlConnection cn = new SqlConnection(StrS))
            {


                cn.Open();

                SqlCommand Cmd = new SqlCommand(ReturnSql(mode), cn);
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@title", Title);
                Cmd.Parameters.AddWithValue("@contents", Contents);
                Cmd.Parameters.AddWithValue("@nichizi", Nichizi);
                Cmd.Parameters.AddWithValue("@status", Status);
                Cmd.Parameters.AddWithValue("@Genre", Genre);
                if (mode.Equals("Delete") || mode.Equals("Update"))
                {
                    Cmd.Parameters.AddWithValue("@No", No);

                }
                Cmd.ExecuteNonQuery();
            };
        }
    }
}