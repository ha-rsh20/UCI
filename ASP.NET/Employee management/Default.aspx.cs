using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using Azure.Storage.Blobs;

namespace EmployeeImageUpload
{
    public partial class _Default : System.Web.UI.Page
    {
        string Sql_Auth = System.Configuration.ConfigurationManager.ConnectionStrings["SQL_Auth"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear();
                dataListBind();
            }
        }

        protected async void buttonInsert_Click(object sender, EventArgs e)
        {
            string blobStorageConStr = "DefaultEndpointsProtocol=https;AccountName=teststoragepro;AccountKey=mEtAsJjw8UJmSsNC9L76eYBeQ9iU8+mlUhG0jPXkUMY0cww6tI1K+Qy11brrhCOcb7PVkwV4D7DA+AStpptjdQ==;EndpointSuffix=core.windows.net";
            string blobStorageContainerName = "images";
            BlobContainerClient container = new BlobContainerClient(blobStorageConStr, blobStorageContainerName);
            
            
            if (fileImage.HasFiles)
            {
                try
                {
                    string extension = Path.GetExtension(fileImage.FileName).ToString();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension ==".JPG")
                    {
                        string filename = Path.GetFileName(fileImage.FileName).ToString();
                        fileImage.SaveAs(Server.MapPath("Images/") + filename);

                        using (SqlConnection con = new SqlConnection(Sql_Auth))
                        {
                            con.Open();

                            //string insertQuery = "INSERT INTO EmployeePersonalDetails (firstname, lastname, dob, gender, contact, address, email, username, image) VALUES (@firstname, @lastname, @dob, @gender, @contact, @address, @email, @username, @image )";
                            SqlCommand cmd = new SqlCommand("insertEmployee", con);
                            cmd.CommandType = CommandType.StoredProcedure;


                            cmd.Parameters.AddWithValue("@firstname", textFirstname.Text);
                            cmd.Parameters.AddWithValue("@lastname", textLastname.Text);
                            cmd.Parameters.AddWithValue("@dob", textDob.Text);
                            cmd.Parameters.AddWithValue("@gender", radioGender.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@contact", textContact.Text);
                            cmd.Parameters.AddWithValue("@address", textAddress.Text);
                            cmd.Parameters.AddWithValue("@email", textEmail.Text);
                            cmd.Parameters.AddWithValue("@username", textUsername.Text);

                            string iPath = "Images/"+filename;
                            string imagePath = "D:/Projects/Internship Projects/Asp projects/EmployeeImageUpload/Images/" + filename; 
                            cmd.Parameters.AddWithValue("@image", iPath);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            var blob = container.GetBlobClient(iPath);
                            var stream = File.OpenRead(imagePath);
                            await blob.UploadAsync(stream);
                            if (rowsAffected > 0)
                            {
                                Response.Write("Data Inserted");
                                Clear();
                                dataListBind();
                            }
                        }
                    }
                    else
                    {
                        labelMessage.Text = "Image not in correct format";
                        labelMessage.ForeColor = System.Drawing.Color.ForestGreen;
                    }
                }
                catch (Exception ex)
                {
                    labelMessage.Text = "Error: " + ex.Message;
                    labelMessage.ForeColor = System.Drawing.Color.ForestGreen;
                }
            }
        }

        protected void DataList_EditCommand(object source, DataListCommandEventArgs e)
        {
            DataList.EditItemIndex = e.Item.ItemIndex;
            Clear();
            dataListBind();
        }

        protected void DataList_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            string username = ((Label)e.Item.FindControl("LabelUserName")).Text;

            using (SqlConnection con = new SqlConnection(Sql_Auth))
            {
                con.Open();
                //string deleteQuery = "DELETE FROM EmployeePersonalDetails WHERE username = @username";
                SqlCommand cmd = new SqlCommand("deleteEmployee", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    string imageUrl = ((Image)e.Item.FindControl("image")).ImageUrl;
                    string imagePath = Server.MapPath(imageUrl);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                    Response.Write("Data Deleted");
                    Clear();
                    dataListBind();
                }
            }
        }

        protected void DataList_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            string username = ((Label)e.Item.FindControl("LabelUserName")).Text;
            TextBox textEditFirstName = (TextBox)e.Item.FindControl("textEditFirstName");
            TextBox textEditLastName = (TextBox)e.Item.FindControl("textEditLastName");
            TextBox textEditDob = (TextBox)e.Item.FindControl("textEditDob");
            TextBox textEditGender = (TextBox)e.Item.FindControl("textEditGender");
            TextBox textEditContact = (TextBox)e.Item.FindControl("textEditContact");
            TextBox textEditAddress = (TextBox)e.Item.FindControl("textEditAddress");
            TextBox textEditEmail = (TextBox)e.Item.FindControl("textEditEmail");
            TextBox textEditUserName = (TextBox)e.Item.FindControl("textEditUserName");
            FileUpload fileUploadImage = (FileUpload)e.Item.FindControl("fileUploadImage");

            if (fileUploadImage.HasFile)
            {
                string filename = Path.GetFileName(fileUploadImage.FileName);
                string uploadFolderPath = Server.MapPath("~/Images/"); 
                string filePath = Path.Combine(uploadFolderPath, filename);
                fileUploadImage.SaveAs(filePath);

                string imagePath = "Images/" + filename;

                using (SqlConnection con = new SqlConnection(Sql_Auth))
                {
                    con.Open();
                   //string updateImageQuery = "UPDATE EmployeePersonalDetails SET image = @image WHERE username = @username";
                    SqlCommand cmdImage = new SqlCommand("updateEmployee", con);
                    cmdImage.CommandType = CommandType.StoredProcedure;
                    cmdImage.Parameters.AddWithValue("@image", imagePath);
                    cmdImage.Parameters.AddWithValue("@username", username);
                    cmdImage.ExecuteNonQuery();
                }
            }

            using (SqlConnection con = new SqlConnection(Sql_Auth))
            {
                con.Open();
                string updateQuery = "UPDATE EmployeePersonalDetails SET firstname = @firstname, lastname = @lastname, dob = @dob, gender = @gender, contact = @contact, address = @address, email = @email WHERE username = @username";
                SqlCommand cmd = new SqlCommand(updateQuery, con);

                cmd.Parameters.AddWithValue("@firstname", textEditFirstName.Text);
                cmd.Parameters.AddWithValue("@lastname", textEditLastName.Text);
                cmd.Parameters.AddWithValue("@dob", textEditDob.Text);
                cmd.Parameters.AddWithValue("@gender", textEditGender.Text);
                cmd.Parameters.AddWithValue("@contact", textEditContact.Text);
                cmd.Parameters.AddWithValue("@address", textEditAddress.Text);
                cmd.Parameters.AddWithValue("@email", textEditEmail.Text);
                cmd.Parameters.AddWithValue("@username", username);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Response.Write("Data Updated");
                    DataList.EditItemIndex = -1;
                    Clear();
                    dataListBind();
                }
                else
                {
                    Response.Write("Data Not Updated");
                }
            }
        }



        protected void DataList_CancelCommand(object source, DataListCommandEventArgs e)
        {
            DataList.EditItemIndex = -1;
            Clear();
            dataListBind();
        }

        public void Clear()
        {
            textFirstname.Text = "";
            textLastname.Text = "";
            textDob.Text = "";
            radioGender.ClearSelection();
            textContact.Text = "";
            textAddress.Text = "";
            textEmail.Text = "";
            textUsername.Text = "";
            labelMessage.Text = "";
        }

        protected void dataListBind()
        {
            using (SqlConnection con = new SqlConnection(Sql_Auth))
            {
                con.Open();
                string selectQuery = "SELECT * FROM EmployeePersonalDetails";
                SqlCommand cmd = new SqlCommand(selectQuery, con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    DataList.DataSource = dr;
                    DataList.DataBind();
                }
            }
        }
    }
}
