using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace testApp
{
    public partial class About : Page
    {
        List<string> names = new List<string>();
        List<string> emails = new List<string>();
        public string _StrCon_Window_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["ConStrWin"].ConnectionString;
        HttpCookie studentInfo = new HttpCookie("studentInfo");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cascadingDropdown();
            }
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            SqlCommand cmd = new SqlCommand("getAllStudents", _Con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader rd = null;

            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                rd = cmd.ExecuteReader();
                int i = 0;
                while(rd.Read())
                {
                    names.Add(Convert.ToString(rd["name"]));
                    emails.Add(Convert.ToString(rd["email"]));
                }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?name="+txtStudentName.Text+"&email="+txtStudentEmail.Text);
            /*            Session["name"] = Convert.ToString(txtStudentName.Text);
                        Session["email"] = Convert.ToString(txtStudentEmail.Text);*/
            /*studentInfo["name"] = Convert.ToString(txtStudentName.Text);
            studentInfo["email"] = Convert.ToString(txtStudentEmail.Text);
            studentInfo.Expires.Add(new TimeSpan(0, 1, 0));
            Response.Cookies.Add(studentInfo);*/
            /*Response.Cookies["name"].Value = Convert.ToString(txtStudentName.Text);
            Response.Cookies["email"].Value = Convert.ToString(txtStudentEmail.Text);*/

            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            //string query = "insert into StdData values('" + txtStudentName.Text + "','" + txtStudentGender.Text + "','" + txtStudentDob.Text + "','" + txtStudentAddress1.Text + "','" + txtStudentAddress2.Text+"'," + txtStudentPhone1.Text  + "," + txtStudentPhone2.Text + ",'" + txtStudentEmail.Text + "','" + txtStudentPassword.Text + "')";
            SqlCommand _cmd = new SqlCommand("insertStudentData", _Con);
            SqlCommand _cmd_add = new SqlCommand("insertStudentAddress", _Con);
            SqlCommand _cmd_ph = new SqlCommand("insertStudentPhone", _Con);

            _cmd.Parameters.AddWithValue("name",Convert.ToString(txtStudentName.Text));
            _cmd.Parameters.AddWithValue("gender", Convert.ToString(txtStudentGender.Text));
            _cmd.Parameters.AddWithValue("dob", Convert.ToString(txtStudentDob.Text));
            _cmd.Parameters.AddWithValue("address1", Convert.ToString(txtStudentAddress1.Text));
            _cmd.Parameters.AddWithValue("phone1", Convert.ToString(txtStudentPhone1.Text));
            _cmd.Parameters.AddWithValue("email", Convert.ToString(txtStudentEmail.Text));
            _cmd.Parameters.AddWithValue("password", Convert.ToString(txtStudentPassword.Text));

            

            _cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }

                bool flag = true;
                for (int i = 0; i < names.Count; i++)
                {
                    if (names[i] == Convert.ToString(txtStudentName.Text) || emails[i] == Convert.ToString(txtStudentEmail.Text))
                    {
                        flag = false;
                    }
                }
                if(flag)
                {
                    /*_cmd.ExecuteNonQuery();*/
                    int id_for_table = (int)_cmd.ExecuteScalar();

                    _cmd_add.Parameters.Add("id",SqlDbType.Int).Value = id_for_table;
                    _cmd_add.Parameters.Add("address", SqlDbType.VarChar).Value = Convert.ToString(txtStudentAddress2.Text);

                    _cmd_ph.Parameters.Add("id",SqlDbType.Int).Value = id_for_table;
                    _cmd_ph.Parameters.Add("phone", SqlDbType.VarChar).Value = Convert.ToString(txtStudentPhone2.Text);

                    _cmd_add.CommandType = CommandType.StoredProcedure;
                    _cmd_ph.CommandType = CommandType.StoredProcedure;

                    _cmd_add.ExecuteNonQuery();
                    _cmd_ph.ExecuteNonQuery();
                    Response.Write($"{id_for_table} Records Inserted Successfully");
                }
                else
                {
                    Response.Write("Name or Email already taken!");
                }
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
        protected void cascadingDropdown()
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from countries", _Con);
                ddlCountries.DataSource = cmd.ExecuteReader();
                ddlCountries.DataTextField = "CountryName";
                ddlCountries.DataValueField = "CId";
                ddlCountries.DataBind();
                ddlCountries.Items.Insert(0, new ListItem("---Select Country---", "0"));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                }
            }
        }

        protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            int countryId = Convert.ToInt32(ddlCountries.SelectedValue);
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from states where cid = " + countryId, _Con);
                ddlStates.DataSource = cmd.ExecuteReader();
                ddlStates.DataTextField = "StateName";
                ddlStates.DataValueField = "SId";
                ddlStates.DataBind();
                ddlStates.Items.Insert(0, new ListItem("---Select State  ---", "0"));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                }
            }
        }

        protected void ddlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            int stateId = Convert.ToInt32(ddlStates.SelectedValue);
            try
            {
                if (_Con.State == ConnectionState.Closed)
                {
                    _Con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from district where sid = " + stateId, _Con);
                ddlDistricts.DataSource = cmd.ExecuteReader();
                ddlDistricts.DataTextField = "DistrictName";
                ddlDistricts.DataValueField = "DId";
                ddlDistricts.DataBind();
                ddlDistricts.Items.Insert(0, new ListItem("---Select District  ---", "0"));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                }
            }
        }

        protected void RepeatInformation_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection _Con = new SqlConnection(_StrCon_Window_Auth);
            if (e.CommandName == "update")
            {
                Response.Write("Update call");
            }
            if (e.CommandName == "delete")
            {
                Response.Write("Delete call");
            }
        }

    }
}