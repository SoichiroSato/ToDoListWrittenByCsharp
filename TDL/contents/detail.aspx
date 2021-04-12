<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="TDL.Detail" %>

<!DOCTYPE html>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<link rel="stylesheet" type="text/css" href="../css/style.css"/>
		<title>詳細画面</title>
	</head>
	<body>
		<form id="form1" runat="server">
			<br/>
			<div class="class1">
				<div class="class2">
					<h1>ToDo List</h1>
				</div>
			</div>
			<asp:ScriptManager ID="SM_detail" runat="server"></asp:ScriptManager>
			<asp:UpdatePanel ID ="detail_form" runat ="server" UpdateMode="Conditional">
				<ContentTemplate>
					<br/>
					<div class="class3">
						<asp:label ID ="msg" runat="server"></asp:label>
						<br />
						<asp:label ID ="erm" runat="server" ForeColor="Red"></asp:label>
						<div align="center">
        					<span class="spanclass1">
								<asp:Button ID="return" runat="server" OnClick="Return_Click" Text="戻る" />
							</span>
						</div>	
						<div style="padding: 10px; margin-bottom: 10px; border: 1px ;">
							<div class="class4">
								<label for="janle">ジャンル</label>
								<span style="padding-left:4em;">
									<asp:DropDownList ID="drp_st" runat="server" DataSourceID="drp_Genre"
										DataTextField="Text_1"    Height="31px" Width="105px" />
								</span>
							</div>
							<div class="class4">
								<label for="kensakujoken">期限</label>
								<span style="padding-left:6em;">
									<asp:TextBox ID="YMD" runat="server"  AutoComplete="off" Width="100px"/>
									<ajaxToolkit:CalendarExtender ID="YMD_calender" runat="server" DaysModeTitleFormat="yyyy/MM/dd" 
										Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" TargetControlID="YMD" />
								</span>
							</div>
							<div class="class4">
								<label for="title">タイトル</label>
								<span style="padding-left:4em;">
									<asp:TextBox ID="Title_t" runat="server"  size="50" Width="349px"></asp:TextBox>
								</span>
							</div>
							<div class="class4">
								<label for="ststus">ステータス</label>
								<span style="padding-left:3em;">
									<asp:RadioButtonList ID="Status" runat="server" 
										RepeatLayout="Flow" RepeatDirection="Horizontal">
										<asp:ListItem Text="新規" Value="0" Selected="true"></asp:ListItem>
										<asp:ListItem Text="進行中" Value="1" ></asp:ListItem>
										<asp:ListItem Text="完了" Value="2" ></asp:ListItem>
										<asp:ListItem Text="再着手" Value="3" ></asp:ListItem>
									</asp:RadioButtonList>
								</span>
							</div>
							<div class="class4">
								<label for="contents">内容</label>
								<span style="padding-left:6em;">
									<asp:textbox ID="contents_t" runat="server" TextMode="multiline" rows="10" 
										cols="100" style="vertical-align:top" Width="560px">
									</asp:textbox>
								</span>
							</div>
							<div class="class6">
								<div align="right">
									<asp:Button ID="IU" runat="server"  OnClick="IU_Click" />
									<asp:Button ID="DLT" runat="server"  OnClick="DLT_Click" Text="削除" />
								</div>
							</div>
						</div>
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
			<asp:SqlDataSource ID="drp_Genre" runat="server"
				ConnectionString="<%$ ConnectionStrings:Strs %>"
				SelectCommand="SELECT No, Text_1 FROM M_General Where Code = 101 Order By No">
			</asp:SqlDataSource>
		</form>
	</body>
</html>
