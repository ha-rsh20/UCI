<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmployeeImageUpload._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <hr />
    <div>
    <div style="font-size: x-large; text-align: center; color: red;"><strong>Add Employee</strong></div>
    </div>
    <br />
    <table class="w-100" style="border-collapse: collapse; width: 100%;">
    <tr>
        <td style="padding: 8px; color: #333333;"><span style="font-weight: bold; color: #333333;">First Name</span></td>
        <td><asp:TextBox ID="textFirstname" runat="server" required="" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 8px; color: #333333;"><span style="font-weight: bold; color: #333333;">Last Name</span></td>
        <td><asp:TextBox ID="textLastname" runat="server" required="" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 8px;"><strong><asp:Label ID="labelDob" runat="server" Text="Data of Birth" style="color: #333333;"></asp:Label></strong></td>
        <td><asp:TextBox ID="textDob" required="" TextMode="Date" runat="server" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 5px;"><strong><asp:Label ID="labelGender" runat="server" Text="Gender" style="color: #333333;"></asp:Label></strong></td>
        <td style="padding: 5px;">
            <asp:RadioButtonList ID="radioGender" runat="server" RepeatDirection="Horizontal" DataTextField="gender" DataValueField="gender">
                <asp:ListItem>Female</asp:ListItem>
                <asp:ListItem>Male</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td style="padding: 8px;"><strong><asp:Label ID="labelContact" runat="server" Text="Contact" style="color: #333333;"></asp:Label></strong></td>
        <td><asp:TextBox ID="textContact" TextMode="Phone" required=""  runat="server" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 8px;"><strong><asp:Label ID="labelAddress" runat="server" Text="Address" style="color: #333333;"></asp:Label></strong></td>
        <td><asp:TextBox ID="textAddress" required="" runat="server" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 8px;"><strong><asp:Label ID="labelEmail" runat="server" Text="Email" style="color: #333333;"></asp:Label></strong></td>
        <td><asp:TextBox ID="textEmail" required=""  runat="server" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 8px;"><strong><asp:Label ID="labelUsername" runat="server" Text="Username" style="color: #333;"></asp:Label></strong></td>
        <td><asp:TextBox ID="textUsername" runat="server" required="" style="width: 100%;"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding: 8px;"><strong><asp:Label ID="labelImage" runat="server" Text="Photo" style="color: #333;"></asp:Label></strong></td>
        <td><asp:FileUpload ID="fileImage" runat="server" required="" style="width: 100%;" /></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td><asp:Button ID="buttonInsert" runat="server" Text="Insert" OnClick="buttonInsert_Click" style="background-color: #007bff; color: #fff; border: none; padding: 8px 16px; cursor: pointer;" Width="174px"/></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td><strong><asp:Label ID="labelMessage" runat="server" Text="" style="color: #333;"></asp:Label></strong></td>
    </tr>
</table>


    <br />
    <div class="col-lg-8" style="margin-top: 30px">
        <asp:DataList ID="DataList" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" GridLines="Both" OnEditCommand="DataList_EditCommand" OnDeleteCommand="DataList_DeleteCommand">
         <HeaderStyle BackColor="Black" Font-Bold="true" Font-Size="Large" ForeColor="White" HorizontalAlign="Center" CssClass="header-style" />
            <HeaderTemplate>
                <div style="background-color: black; color: white; font-weight: bold; font-size: large; text-align: center;">Employee Data Management</div>
            </HeaderTemplate>
            <ItemTemplate>
                <div style="width:300px; text-align:center; background-color: #f0f0f0; padding: 10px; border-radius: 5px; margin-bottom: 15px;">
                    <asp:Image ID="image" runat="server" ImageUrl='<%# Eval("image") %>' Width="100px" Height="120px" />
                    <br />
                    <asp:Label ID="LabelFirstName" runat="server" Text='<%# Eval("firstname") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelLastName" runat="server" Text='<%# Eval("lastname") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelDOB" runat="server" Text='<%# Eval("dob") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelGender" runat="server" Text='<%# Eval("gender") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelContact" runat="server" Text='<%# Eval("contact") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelAddress" runat="server" Text='<%# Eval("address") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelEmail" runat="server" Text='<%# Eval("email") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <asp:Label ID="LabelUserName" runat="server" Text='<%# Eval("username") %>' style="font-weight: bold;"></asp:Label>
                    <br />
                    <br />
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' style="background-color: #007bff; color: #fff; border: none; padding: 5px 10px; border-radius: 3px;">Edit</asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.ItemIndex %>' style="background-color: #dc3545; color: #fff; border: none; padding: 5px 10px; border-radius: 3px; margin-left: 5px;">Delete</asp:LinkButton>
                </div>
            </ItemTemplate>

            <EditItemTemplate>
                <div style="width:150px; text-align:center;">
                    <br />
                    <asp:FileUpload ID="fileEditImage" runat="server" />
                    <br />
                    <asp:TextBox ID="textEditFirstName" runat="server" Text='<%# Eval("firstname") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditLastName" runat="server" Text='<%# Eval("lastname") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditDob" runat="server" Text='<%# Eval("dob") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditGender" runat="server" Text='<%# Eval("gender") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditContact" runat="server" Text='<%# Eval("contact") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditAddress" runat="server" Text='<%# Eval("address") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditEmail" runat="server" Text='<%# Eval("email") %>'></asp:TextBox>
                    <br />
                    <asp:TextBox ID="textEditUserName" runat="server" Text='<%# Eval("username") %>'></asp:TextBox>
                    <br />
                    <br />
                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CommandArgument='<%# Container.ItemIndex %>'>Update</asp:LinkButton>
                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                </div>
            </EditItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>