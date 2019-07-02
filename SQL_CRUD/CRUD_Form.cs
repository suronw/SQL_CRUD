using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_CRUD
{
    public partial class frmSQLCRUD : Form
    {
        public frmSQLCRUD()
        {
            InitializeComponent();
            GetSQLInstances();

        }

        private DataTable persons = new DataTable();
        private DataTable blank = new DataTable();
        
        private SQLDataExchange dbAccess = new SQLDataExchange();

        private void GetSQLInstances()
        {
            {
                // Retrieve the enumerator instance and then the data.  
                SqlDataSourceEnumerator instance =
                  SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();

                foreach (System.Data.DataRow row in table.Rows)
                {
                    // Populate the SQL instance drop down
                    cboInstance.Items.Add(row[0].ToString() + @"\" + row[1].ToString());
                }
                
            }
        }

        private int CheckSQLSettings()
        {
            if (lblSQLstatus.Text == "Not Connected")
            {
                MessageBox.Show("Check SQL settings!");
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void ClearTextboxes()
        {
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmailAddress.Text = "";
            txtPhoneNumber.Text = "";
        }

        private void UpdateBinding()
        {
            dgPeople.DataSource = persons;
        }

        private void UpdateFields(Person selectGuy)
        {
            txtID.Text = selectGuy.id.ToString();
            txtFirstName.Text = selectGuy.FirstName.ToString();
            txtLastName.Text = selectGuy.LastName.ToString();
            txtEmailAddress.Text = selectGuy.EmailAddress.ToString();
            txtPhoneNumber.Text = selectGuy.PhoneNumber.ToString();
        }

        private void FetchPeople(string field, string value)
        {
            if (CheckSQLSettings() == 0)
            {
                persons = dbAccess.GetPeople(field, value);

                UpdateBinding();
            }
            
        }

        private void AddPeople()
        {
            if (CheckSQLSettings() == 0)
            {
                int res = dbAccess.AddPerson(txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtEmailAddress.Text.ToString(), txtPhoneNumber.Text.ToString());
                if (res == 0)
                {
                    MessageBox.Show("Record Added");
                }
                ClearTextboxes();
            }
        }

        private void UpdatePeople()
        {
            if (CheckSQLSettings() == 0)
            {
                int res = dbAccess.UpdatePerson(Convert.ToInt32(txtID.Text), txtFirstName.Text.ToString(), txtLastName.Text.ToString(), txtEmailAddress.Text.ToString(), txtPhoneNumber.Text.ToString());
                if (res == 0)
                {
                    MessageBox.Show("Record updated");
                }
                else
                {
                    MessageBox.Show("Error updating record");
                }
                ClearTextboxes();
            }
        }

        private void DeletePeople()
        {
            if (CheckSQLSettings() == 0)
            {
                int res = dbAccess.DeletePerson(Convert.ToInt32(txtID.Text));
                if (res == 0)
                {
                    MessageBox.Show("Record deleted");
                }
                else
                {
                    MessageBox.Show("Error deleting record");
                }
                ClearTextboxes();

            }
        }

        private void GetAllPeople()
        {
            if (CheckSQLSettings() == 0)
            {
                persons = dbAccess.GetAllPeople();

                UpdateBinding();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFirstName.TextLength == 0 || txtLastName.TextLength == 0)
            {
                MessageBox.Show("Name cannot be blank");
            }
            else
            {
                AddPeople();
                GetAllPeople();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtFirstName.TextLength == 0 || txtLastName.TextLength == 0)
            {
                MessageBox.Show("Name may not be blank");
            }
            else
            {
                UpdatePeople();
                GetAllPeople();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete Person?", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                DeletePeople();
                GetAllPeople();
            }
        }

        private void cboAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 0)
            {
                txtUsername.Text = Environment.UserName;
                txtUsername.Enabled = false;
                txtPassword.Text = "";
                txtPassword.Enabled = false;
                SQLCreds.Integrated_Security = true;
            }
            else
            {
                txtUsername.Text = "";
                txtUsername.Enabled = true;
                txtPassword.Text = "";
                txtPassword.Enabled = true;
                SQLCreds.Integrated_Security = false;
            }

            
        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                FetchPeople("FirstName", txtFirstName.Text);
                txtFirstName.Text = "";
                txtFirstName.Focus();
                txtFirstName.SelectAll();
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                FetchPeople("LastName", txtLastName.Text);
                txtLastName.Text = "";
                txtLastName.Focus();
                txtLastName.SelectAll();
            }
        }

        private void txtPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                FetchPeople("PhoneNumber", txtPhoneNumber.Text);
                txtPhoneNumber.Text = "";
                txtPhoneNumber.Focus();
                txtPhoneNumber.SelectAll();
            }
        }

        private void txtEmailAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                FetchPeople("EmailAddress", txtEmailAddress.Text);
                txtEmailAddress.Text = "";
                txtEmailAddress.Focus();
                txtEmailAddress.SelectAll();
            }
        }

        private void cboInstance_SelectedValueChanged(object sender, EventArgs e)
        {
            SQLCreds.Instance = cboInstance.Text.ToString();
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 0)
            {
            }
            else
            {
                if (txtUsername.Text != "")
                {
                    SQLCreds.User_Name = txtUsername.Text;
                }
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 0)
            {
            }
            else
            {
                if (txtPassword.Text != "")
                {
                    SQLCreds.Password = txtPassword.Text;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cboInstance.Text == "" || cboAuthentication.Text == "" || txtDatabase.Text == "")
            {
                MessageBox.Show("Bad SQL");
            }
            else
            {
                SQLCreds.Database = txtDatabase.Text;
                MessageBox.Show(SQLCreds.Connection_String);
                lblSQLstatus.Text = "Connected";
                GetAllPeople();
            }
        }

        private void dgPeople_SelectionChanged(object sender, EventArgs e)
        {
            if (dgPeople.SelectedRows.Count > 0)
            {
                Person GotGuy = new Person();
                GotGuy.id = (int)dgPeople.SelectedRows[0].Cells[0].Value;
                GotGuy.FirstName = dgPeople.SelectedRows[0].Cells[1].Value.ToString();
                GotGuy.LastName = dgPeople.SelectedRows[0].Cells[2].Value.ToString();
                GotGuy.EmailAddress = dgPeople.SelectedRows[0].Cells[3].Value.ToString();
                GotGuy.PhoneNumber = dgPeople.SelectedRows[0].Cells[4].Value.ToString();

                UpdateFields(GotGuy);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
            persons = blank;
            UpdateBinding();
        }

        private void BtnViewAll_Click(object sender, EventArgs e)
        {
            ClearTextboxes();
            GetAllPeople();
        }
    }
}
