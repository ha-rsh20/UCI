using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace testApp
{
    public partial class Contact : Page
    {
        public string _StrCon_Window_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrWin"].ConnectionString;
        protected string id;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("getStudentById", _Con);
            cmd.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(txtSearchStudentName.Text);
            
            cmd.CommandType = CommandType.StoredProcedure;

            if (txtSearchStudentName.Text == "")
            {
                txtSearchStudentName.Text = "";
            }

            SqlDataReader rd = null;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                rd = cmd.ExecuteReader();


                lblNameData.Text = "Name";
                lblGender.Text = "Gender";
                lblDob.Text = "Dob";
                lblAddress1.Text = "Address1";
                lblAddress2.Text = "Address2";
                lblPhone1.Text = "Phone1";
                lblPhone2.Text = "Phone2";
                lblEmail.Text = "Email";

                while (rd.Read())
                {
                    lblIdData.Text = Convert.ToString(rd["id"]);
                    lblNameData.Text += "<br/>" + Convert.ToString(rd["name"]);
                    lblGender.Text += "<br/>" + Convert.ToString(rd["gender"]);
                    lblDob.Text += "<br/>" + Convert.ToString(rd["dob"]);
                    lblAddress1.Text += "<br/>" + Convert.ToString(rd["address1"]);
                    /*lblAddress2.Text += "<br/>" + Convert.ToString(rd["address2"]);*/
                    lblPhone1.Text += "<br/>" + Convert.ToString(rd["phone1"]);
                    /*lblPhone2.Text += "<br/>" + Convert.ToString(rd["phone2"]);*/
                    lblEmail.Text += "<br/>" + Convert.ToString(rd["email"]);
                    txtDataId.Text = Convert.ToString(rd["id"]);
                    txtDataName.Text = Convert.ToString(rd["name"]);

                    id = Convert.ToString(rd["id"]);

                    txtUpdateStudentName.Text = Convert.ToString(rd["name"]);
                    txtUpdateStudentGender.Text = Convert.ToString(rd["gender"]);
                    txtUpdateStudentDob.Equals(rd["dob"]);
                    txtUpdateStudentAddress1.Text = Convert.ToString(rd["address1"]);
                    txtUpdateStudentPhone1.Text = Convert.ToString(rd["phone1"]);
                    txtUpdateStudentEmail.Text = Convert.ToString(rd["email"]);
                }

                txtSearchStudentName.Text = "";
                txtSearchStudentName.Text = "";
            }
            catch (SqlException se)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            /*Response.Write("Data updated");*/
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("updateStudentData", _Con);
            cmd.Parameters.AddWithValue("id", Convert.ToInt32(txtDataId.Text));
            cmd.Parameters.AddWithValue("name", Convert.ToString(txtUpdateStudentName.Text));
            cmd.Parameters.AddWithValue("gender", Convert.ToString(txtUpdateStudentGender.Text));
            cmd.Parameters.AddWithValue("dob", Convert.ToDateTime(txtUpdateStudentDob.Text));
            cmd.Parameters.AddWithValue("address1", Convert.ToString(txtUpdateStudentAddress1.Text));
            cmd.Parameters.AddWithValue("address2", Convert.ToString(txtUpdateStudentAddress2.Text));
            cmd.Parameters.AddWithValue("phone1", Convert.ToInt64(txtUpdateStudentPhone1.Text));
            cmd.Parameters.AddWithValue("phone2", Convert.ToInt64(txtUpdateStudentPhone2.Text));
            cmd.Parameters.AddWithValue("email", Convert.ToString(txtUpdateStudentEmail.Text));
            cmd.Parameters.AddWithValue("password", Convert.ToString(txtUpdateStudentPassword.Text));

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                Response.Write("Data updated");
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("deleteStudentData", _Con);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                string name = Convert.ToString(lblNameData.Text);
                
/*                    int id = Convert.ToInt32(txtDataId.Text);
*/                    Response.Write(id);
/*                    cmd.Parameters.AddWithValue("id", Convert.ToInt64(txtDataId.Text));
                    cmd.ExecuteNonQuery();
*/                
                /*Response.Write("Search Student first!");*/
            }
            catch (SqlException se)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                    _Con.Close();
            }
        }
    }
}