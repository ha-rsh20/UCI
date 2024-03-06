using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;
using System.Web.DynamicData;
using System.Xml.Linq;

namespace testApp
{
    public partial class _Default : Page
    {
        public string _StrCon_Window_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrWin"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                lblQueryName.Text = Request.QueryString["name"];
                lblQueryEmail.Text = Request.QueryString["email"];
                lblHiddenVal.Text = hfVal.Value;
                /*if (Session["name"] != null)
                {
                    lblSessionName.Text = "Name: "+Session["name"].ToString();
                }
                if (Session["email"] != null)
                {
                    lblSessionEmail.Text = " Email:"+Session["email"].ToString();
                }*/

            }
                /*try
                {
                    if (Request.Cookies["name"].Value != null)
                    {
                        lblSessionName.Text = "Name: " + Request.Cookies["name"].Value;
                    }
                    if (Request.Cookies["email"].Value != null)
                    {
                        lblSessionEmail.Text = " Email:" + Request.Cookies["email"].Value;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("cookie isn't set yet!");
                }*/
        }

        protected void BindGridView()
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlDataAdapter adapter = new SqlDataAdapter("getAllStudents", _Con);
            DataTable dataTable = new DataTable();

            SqlDataReader rd = null;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                adapter.Fill(dataTable);

                gvInfo.DataSource = dataTable;
                gvInfo.DataBind();
            }
            catch (SqlException Ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void gvInfo_RowCancelingEdit(object sender,GridViewCancelEditEventArgs e)
        {
            gvInfo.EditIndex = -1;
            BindGridView();
        }

        protected void gvInfo_RowDeleting(object sender,GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gvInfo.DataKeys[e.RowIndex].Value.ToString());
            deleteStudentData(id);
            BindGridView();
        }

        protected void gvInfo_RowEditing(object sender,GridViewEditEventArgs e)
        {
            gvInfo.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvInfo_RowUpdating(object sender,GridViewUpdateEventArgs e)
        {
            int id = int.Parse(gvInfo.DataKeys[e.RowIndex].Value.ToString());
            TextBox txtName = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtName");
            TextBox txtGender = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtGender");
            TextBox txtDob = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtDob");
            TextBox txtAddress1 = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtAddress1");
            TextBox txtPhone1 = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtPhone1");
            TextBox txtEmail = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtEmail");

            updateStudentData(id,txtName.Text, txtGender.Text, txtDob.Text, txtAddress1.Text, txtPhone1.Text, txtEmail.Text);
            gvInfo.EditIndex = -1;
            BindGridView();
        }

        protected void updateStudentData(int id,string name,string gender,string dob,string address1,string phone1,string email)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand _cmd = new SqlCommand("updateStudentDataWithoutPassword", _Con);
            _cmd.Parameters.AddWithValue("name", name);
            _cmd.Parameters.AddWithValue("gender", gender);
            _cmd.Parameters.AddWithValue("dob", dob);
            _cmd.Parameters.AddWithValue("address1", address1);
            _cmd.Parameters.AddWithValue("phone1", phone1);
            _cmd.Parameters.AddWithValue("email", email);
            _cmd.Parameters.AddWithValue("id", id);
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                _cmd.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {
                /*Console.WriteLine(Ex);
                Response.Write(Ex);*/
            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
            /*Response.Write($"no {id} updated");*/
        }

        protected void deleteStudentData(int id)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand _cmd = new SqlCommand("deleteStudentData", _Con);
            _cmd.Parameters.AddWithValue("id", id);
            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                _cmd.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
            /*Response.Write($"no {id} deleted");*/
        }
    }
}