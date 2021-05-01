using System;
using System.Text;
using TDL.Logic;

namespace TDL
{
    public partial class Confirm : System.Web.UI.Page
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string mode = Request.QueryString["mode"];
                    if (!(mode is null))
                    {
                        if (mode.Equals("Delete"))
                        {
                            LogicOfDetail LogicOfDetail = new LogicOfDetail();
                            LogicOfDetail.BindParameters(Request.QueryString["No"]);
                            Title_t.Text = LogicOfDetail.Title;
                            contents.Text = LogicOfDetail.Contents;
                            status.Text = LogicOfDetail.Status;
                            Genre.Text = LogicOfDetail.Genre;
                            YEAR.Text = LogicOfDetail.Nichizi.ToString("yyyy");
                            Month.Text = LogicOfDetail.Nichizi.ToString("MM");
                            Day.Text = LogicOfDetail.Nichizi.ToString("dd");
                            msg.Text = "下記内容を削除します。よろしければ「確定」ボタンを押してください。";

                        }
                        else if (mode.Equals("Insert") || mode.Equals("Update"))
                        {
                            if (mode.Equals("Insert"))
                            {
                                msg.Text = "下記内容で登録します。よろしければ「確定」を押してください。";
                            }
                            else
                            {
                                if (Request.QueryString["No"] is null)//エラー
                                {
                                    ReturnTop();
                                }
                                msg.Text = "下記内容で変更します。よろしければ「確定」ボタンを押してください。";
                            }
                            Title_t.Text = (string)Session["title"];
                            contents.Text = (string)Session["contents"];
                            status.Text = (string)Session["status"];
                            Genre.Text = (string)Session["Genre"];
                            YEAR.Text = DateTime.Parse((string)Session["nichizi"]).ToString("yyyy");
                            Month.Text = DateTime.Parse((string)Session["nichizi"]).ToString("MM");
                            Day.Text = DateTime.Parse((string)Session["nichizi"]).ToString("dd");
                        }
                        else{ReturnTop();}
                    }
                    else{ReturnTop();}
                }
                catch(Exception){ ReturnTop(); }
            }            
            
        }

        /// <summary>
        /// 戻るボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Return_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            StringBuilder sb = new StringBuilder("./detail.aspx?mode=");
            if (Request.QueryString["mode"].Equals("Update") || Request.QueryString["mode"].Equals("Delete")) {
                sb.Append("Update&No=" + Request.QueryString["No"]);
            }
            else 
            { 
                sb.Append("Insert");
            }
            Response.Redirect(sb.ToString());
        }

        /// <summary>
        /// 確定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Submit_Click(object sender, EventArgs e)
        {
                            
            try
            { 
                Session.Remove("msg");
                LogicOfConfirm LogicOfConfirm = new LogicOfConfirm
                {
                    Title = Title_t.Text,
                    Contents = contents.Text,
                    Nichizi = DateTime.Parse(YEAR.Text + "/" + Month.Text + "/" + Day.Text)
                };
                if (status.Text.Equals("新規"))
                {
                    LogicOfConfirm.Status = 0;
                }
                else if (status.Text.Equals("進行中"))
                {
                    LogicOfConfirm.Status = 1;
                }
                else if (status.Text.Equals("完了"))
                {
                    LogicOfConfirm.Status = 2;
                }
                else
                {
                    LogicOfConfirm.Status = 3;
                }

                if (Genre.Text.Equals("仕事"))
                {
                    LogicOfConfirm.Genre = 0;
                }
                else
                {
                    LogicOfConfirm.Genre = 1;
                }
                if (Request.QueryString["mode"].Equals("Delete") || Request.QueryString["mode"].Equals("Update"))
                {
                    LogicOfConfirm.No = Request.QueryString["No"];

                }
                Session["msg"] = LogicOfConfirm.CommitMessage(Request.QueryString["mode"]);
            }
            catch (Exception) 
            { 
                Session["msg"] = "予期せぬエラーが発生しました。";
            }
            
            Response.Redirect("./Top.aspx");
        }

        
        /// <summary>
        /// ページロード時に何等かの不正があった場合の処理
        /// </summary>
        protected void ReturnTop()
        {
            Session["msg"] = "予期せぬエラーが発生しました。";
            Response.Redirect("./Top.aspx");
        }
    }
}