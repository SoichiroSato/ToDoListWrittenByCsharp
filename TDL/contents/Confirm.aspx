<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="TDL.Confirm" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<link rel="stylesheet" type="text/css" href="../css/style.css"/>
		<title>確認画面</title>
	</head>
	<body>
		<form id="form1" runat="server">
			<br/>
			<div class="class1">
				<div class="class2">
					<h1>ToDo List</h1>
				</div>
			</div>
			<br/>
			<div align = "center">
				<span class="spanclass1">
					<asp:Button ID="return" runat="server" OnClick="Return_Click" Text="戻る" />
				</span>
			</div>		
			<br/>
			<div class="class3">
				<div class="class4">
					<asp:label ID ="msg" runat="server"></asp:label>
				</div>	
				<div class="class4">
					ジャンル
					<span style="padding-left:4em;">
						<asp:Label ID="Genre" runat="server" />				
					</span>
				</div>
				<div class="class4">
					期限
					<span style="padding-left:6em;">
						<asp:Label ID="YEAR" runat="server" />年
						<asp:Label ID="Month" runat="server" />月
						<asp:Label ID="Day" runat="server" />日
					</span>
				</div>
				<div class="class4">
					タイトル
					<span style="padding-left:4em;">
						<asp:Label ID="Title_t" runat="server" />
					</span>
				</div>
				<div class="class4">
					ステータス
					<span style="padding-left:3em;">
						<asp:Label ID="status" runat="server" />
					</span>
				</div>
				<div class="class4">
					内容
					<span style="padding-left:6em;">
						<asp:Label ID="contents" runat="server" TextMode="multiline" rows="10" cols="100" 
							style="vertical-align:top" Width="560px"/>
					</span>
				</div>
				<div class="class6" align="right">
					<asp:Button ID="submit" runat="server"  OnClick="Submit_Click" Text="確定" />
				</div>
			</div>
			<br/>
			<br/>		
		</form>
	</body>
</html>
