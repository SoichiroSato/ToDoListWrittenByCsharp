using System;
using System.Text;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using TDL.App_Code;

namespace TDL
{

    public partial class Top : System.Web.UI.Page
    {
        private readonly string StrS = ConfigurationManager.ConnectionStrings["StrS"].ConnectionString;
        private Boolean ck_flg = true;

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["msg"] != null)
                {  //確認画面から戻ってきたとき
                    msg.Text = (string)Session["msg"];
                    Session.RemoveAll();
                }
            }
            else
            {///ポストバック時にメッセージを初期化
                erm.Text = "";
                msg.Text = "";
            }
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SELECT_Click(Object sender, EventArgs e)
        { 
            erm.Text = Utilty.DatetimeOrNot(YMD.Text,true);
            if (erm.Text.Equals(""))
            {
                using (SqlConnection cn = new SqlConnection(StrS))
                {
                    cn.Open();
                    SqlCommand cmd = LogicOfTop.BindSelectedData(cn,YMD.Text, Title_t.Text, drp_st.SelectedValue, Status.SelectedValue);
                    Boolean hasrows;
                    using (var reader = cmd.ExecuteReader())
                    {
                        hasrows = reader.HasRows;
                    };
                        if (hasrows)
                        {

                            RST.DataSource = cmd.ExecuteReader();
                            RST.DataBind();
                            Select_result.Update();
                            UPDB.Visible = true;
                        }
                        else
                        {
                            erm.Text = "検索結果は0件です。";
                            RST.DataBind();
                            Select_result.Update();
                            UPDB.Visible = false;
                        }
                    
                };
            }
            else { return; }
        }

        /// <summary>
        /// 更新ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UPDATE_Click(Object sender, EventArgs e)
        {
            foreach (RepeaterItem item in RST.Items)
            {
                RadioButton ck = (RadioButton)item.FindControl("SRB");
                if (ck.Checked)
                {
                    Label TNO = (Label)item.FindControl("TNO");
                    Response.Redirect("detail.aspx?mode=Update&No=" + TNO.Text);
                }
            }
        }

        /// <summary>
        /// 新規登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Insert_Click(Object sender, EventArgs e)
        {
            Response.Redirect("detail.aspx?mode=Insert");
        }

        /// <summary>
        /// Jsを明示的に付与
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RST_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButton ck = (RadioButton)e.Item.FindControl("SRB");
                if (ck_flg)
                {
                    ck.Checked = true;
                    ck_flg = false;
                }
                string script = "setExclusiveRadioButton('RST.*[RadioButton_GroupName]', this)";
                ck.Attributes.Add("onclick", script);
            }
        }

    }
}