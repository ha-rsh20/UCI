﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="testApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div style="display:flex">
            <asp:Label ID="lblQueryName" runat="server" style="margin-right:10px;"></asp:Label>
            <asp:Label ID="lblQueryEmail" runat="server"></asp:Label>
            <asp:Label ID="lblHiddenVal" runat="server"></asp:Label>
            <asp:HiddenField ID="hfVal" runat="server" value="hidden"/>
        </div>
        <div style="display:flex">
            <asp:GridView ID="gvInfo" runat="server" AutoGenerateColumns="False" Width="1112px" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id" OnRowCancelingEdit="gvInfo_RowCancelingEdit" OnRowDeleting="gvInfo_RowDeleting" OnRowEditing="gvInfo_RowEditing" OnRowUpdating="gvInfo_RowUpdating">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                 <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGender" runat="server" Text='<%# Eval("gender") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblGender" runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOB">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDob" runat="server" Text='<%# Eval("dob") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDob" runat="server" Text='<%# Eval("dob") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address1">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Eval("address1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAddress1" runat="server" Text='<%# Eval("address1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone1">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPhone1" runat="server" Text='<%# Eval("phone1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPhone1" runat="server" Text='<%# Eval("phone1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("email") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" HeaderText="Action" />
                </Columns>
            </asp:GridView>
        </div>
    </main>

</asp:Content>
