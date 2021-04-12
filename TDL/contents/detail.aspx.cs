using System;
using System.Web.UI.WebControls;
using TDL.Logic;


namespace TDL
{
    public partial class Detail : System.Web.UI.Page
    {
        protected void Page_Load(Object sender, EventArgs e) 
        {
            if (!IsPostBack)
            {
                string mode = Request.QueryString["mode"];
                if (!(mode is null))
                {
                    if (mode.Equals("Update"))
                    {//更新モードのときはDBからデータをセレクトする
                        if ((Request.QueryString["No"] is null)){ ReturnTop();}
                        LogicOfDetail LogicOfDetail = new LogicOfDetail();
                        LogicOfDetail.BindParameters(Request.QueryString["No"]);
                        YMD.Text = LogicOfDetail.Nichizi.ToString("yyyy/MM/dd");
                        drp_st.Text = LogicOfDetail.Genre;
                        Title_t.Text = LogicOfDetail.Title;
                        contents_t.Text = LogicOfDetail.Contents;
                        foreach (ListItem st in Status.Items){
                            if (LogicOfDetail.Status.Equals(st.Text)){st.Selected = true;}
                        }
                        msg.Text = "入力項目に更新する内容を入力後【変更】、もしくは、【削除】ボタンを押下してください。";
                        IU.Text = "更新";
                    }
                    else if (mode.Equals("Insert"))
                    { //新規のとき
                        msg.Text = "入力項目に新規登録する内容を入力してください。入力後は【登録】ボタンを押下してください。";
                        IU.Text = "登録";
                        DLT.Visible = false;
                    }
                    else{ReturnTop();}
                }
                else{ReturnTop();}                          
            }
            else{ erm.Text = "";}
        }

        /// <summary>
        /// 戻るボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Return_Click(Object sender, EventArgs e)
        {
            Response.Redirect("./Top.aspx");
        }
                
        /// <summary>
        /// 削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DLT_Click(Object sender, EventArgs e)
        {
            Response.Redirect("./Confirm.aspx?mode=Delete&No=" + Request.QueryString["No"]);
        }

        /// <summary>
        /// 登録・更新ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IU_Click(Object sender, EventArgs e)
        {
            erm.Text = LogicOfDetail.ThrowErrorMessage(Title_t.Text, contents_t.Text, YMD.Text);
            if (erm.Text.Equals("")){ 
                Session.Clear();
                Session["nichizi"] = YMD.Text;
                Session["Title"] = Title_t.Text;
                Session["status"] = Status.SelectedItem.Text;
                Session["contents"] = contents_t.Text;
                Session["Genre"] = drp_st.SelectedValue;
                if (IU.Text.Equals("登録")) {Response.Redirect("./Confirm.aspx?mode=Insert");}
                else{Response.Redirect("./Confirm.aspx?mode=Update&No=" + Request.QueryString["No"]);}
            }
            else { return; }

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