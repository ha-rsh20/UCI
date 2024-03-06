<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="testApp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div style="display:flex;flex-direction:column">
            <asp:Label ID="lblStudentName" runat="server" Text="Name"></asp:Label>
            <asp:TextBox ID="txtStudentName" runat="server" required></asp:TextBox>
            <asp:Label ID="lblStudentGender" runat="server" Text="Gender"></asp:Label>
            <asp:TextBox ID="txtStudentGender" runat="server" required></asp:TextBox>
            <asp:Label ID="lblStudentDob" runat="server" Text="DOB"></asp:Label>
            <asp:TextBox ID="txtStudentDob" runat="server" type="date" required></asp:TextBox>

            <div style="display:flex;flex-direction:row;margin:10px">
                <asp:DropDownList ID="ddlCountries" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlStates" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlDistricts" runat="server"></asp:DropDownList>
            </div>

            <asp:Label ID="lblStudentAddress1" runat="server" Text="Address1"></asp:Label>
            <asp:TextBox ID="txtStudentAddress1" runat="server" required></asp:TextBox>
            <asp:Label ID="lblStudentAddress2" runat="server" Text="Address2"></asp:Label>
            <asp:TextBox ID="txtStudentAddress2" runat="server"></asp:TextBox>
            <asp:Label ID="lblStudentPhone1" runat="server" Text="Phone1"></asp:Label>
            <asp:TextBox ID="txtStudentPhone1" runat="server" required></asp:TextBox>
            <asp:Label ID="lblStudentPhone2" runat="server" Text="Phone2"></asp:Label>
            <asp:TextBox ID="txtStudentPhone2" runat="server"></asp:TextBox>
            <asp:Label ID="lblStudentEmail" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtStudentEmail" runat="server" type="email" required></asp:TextBox>
            <asp:Label ID="lblStudentPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtStudentPassword" runat="server" type="password" required></asp:TextBox>
        </div>
        
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" type="submit" OnClick="btnSubmit_Click" style="margin:10px"/>
    </main>
</asp:Content>
