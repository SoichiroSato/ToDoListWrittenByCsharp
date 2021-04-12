<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="TDL.Top" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<link rel="stylesheet" type="text/css" href="../css/style.css"/>
		<script type="text/javascript" src="../js/top.js"></script>
		<title>Top画面</title>
	</head>
	<body>
		<form id="form1" runat="server">
			<br />
			<div class="class1">
				<div class="class2">
					<h1>ToDo List</h1>
					<br />
					<div>やりたいことを何でも登録！</div>
						<br />
						<asp:Button ID="Insert" runat="server" OnClick="Insert_Click" Text="新規登録" />
					</div>
					<br />
				</div>
			<br />
			<asp:ScriptManager ID="SM_Top" runat="server"></asp:ScriptManager>
			<asp:UpdatePanel ID ="select_form" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="class3">
						<asp:Label ID="msg" runat="server" ForeColor="Red" ></asp:Label>
					</div>
			
					<div class="class3">
						<div style="padding: 10px; margin-bottom: 0px; border: 1px solid #333333; border-bottom:none; background-color:red;">
						<div>検索条件</div>
					</div>
						<div style="padding: 10px; margin-bottom: 10px; border: 1px solid #333333;">
							<div class="class4">
								<asp:label ID ="erm" runat="server"></asp:label>
							</div>
							<div class="class4">
								<label>ジャンル</label>
								<span style="padding-left:4em;">
									<asp:DropDownList ID="drp_st" runat="server" DataSourceID="drp_Genre" DataTextField="Text_1" 
										DataValueField="No" Height="30px" Width="100px" /> 
									</span>
							</div>
							<div class="class4">
								<label>日時</label>
								<span style="padding-left:6em;">
									<asp:TextBox ID="YMD" runat="server"  AutoComplete="off" Width="100px"/>
									<ajaxToolkit:CalendarExtender ID="YMD_calender" runat="server" DaysModeTitleFormat="yyyy/MM/dd"
										Format="yyyy/MM/dd" TodaysDateFormat="yyyy/MM/dd" TargetControlID="YMD" />
								</span>
							</div>
							<div class="class4">
								<label >タイトル</label>
								<span style="padding-left:4em;">
									<asp:TextBox ID="Title_t" runat="server"  size="50" Width="349px"></asp:TextBox>
								</span>
							</div>
							<div class="class4">
								<label>ステータス</label>
								<span style="padding-left:3em;">
									<asp:RadioButtonList ID="Status" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
										<asp:ListItem Text="新規" Value="0" Selected="true"></asp:ListItem>
										<asp:ListItem Text="進行中" Value="1" ></asp:ListItem>
										<asp:ListItem Text="完了" Value="2" ></asp:ListItem>
										<asp:ListItem Text="再着手" Value="3" ></asp:ListItem>
									</asp:RadioButtonList>
								</span>
							</div>
							<div style="text-align: center">
								<span class="spanclass2">
									<asp:Button ID="SELECT" runat="server" onClick="SELECT_Click"   Text="検索"/>
								</span>
							</div>
						</div>
					</div>
					<br />
				</ContentTemplate>
			</asp:UpdatePanel>
			<asp:updatepanel ID ="Select_result" runat ="server" UpdateMode="Conditional">
				<ContentTemplate>
					<div class="class5">
						<asp:Repeater ID="RST" runat="server" OnItemDataBound="RST_ItemDataBound"   >  
							<HeaderTemplate>
								<table id="ST"  border="1" align="center" cellpadding="3" style="border-color: #333333; 
									width: 1024px; border-spacing: 0px;">                  
									<tr style="background-color: #FF0000">
										<th ></th>
										<th >タイトル・内容</th>
										<th >ジャンル</th>
										<th >日時</th>
										<th >ステータス</th>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
									<tr style="text-align: center">
										<td rowspan="2" >
											<asp:radiobutton  id="SRB"  runat="server"  />
											<asp:Label ID ="TNO" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "No") %>' Visible="false"></asp:Label>
										</td>
										<td ><%# DataBinder.Eval(Container.DataItem, "title") %></td>
										<td ><%# DataBinder.Eval(Container.DataItem, "Genre") %></td>
										<td ><%# DataBinder.Eval(Container.DataItem, "nichizi", "{0:yyyy-MM-dd}") %></td>
										<td ><%# DataBinder.Eval(Container.DataItem, "Status") %></td>
									</tr>
									<tr >
										<td colspan="5" style="text-align: center">
											<%# DataBinder.Eval(Container.DataItem, "Contents") %>
										</td>
									</tr>
							</ItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>             
					</div>
					<div class="class6">
						<div style="text-align: center">
							<span class="spanclass1">
								<asp:Button ID="UPDB" runat="server"  OnClick="UPDATE_Click" text="更新" Visible="false"/>
							</span>
						</div>
					</div>
				</ContentTemplate>
			</asp:updatepanel>
		</form>
		<asp:SqlDataSource ID="drp_Genre" runat="server"
			ConnectionString="<%$ ConnectionStrings:Strs %>"
			SelectCommand="SELECT No, Text_1 FROM M_General Where Code = 101 Order By No">
		</asp:SqlDataSource>
	</body>
</html>
