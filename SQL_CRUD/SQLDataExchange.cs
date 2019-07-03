using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_CRUD
{
    class SQLDataExchange
    {

        void TEMP_METHOD_TO_SAVE_SQL_QUERIES()
        {
            string sql_customer = "" +
                "CREATE TABLE Customer (" +
                "CustomerID int NOT NULL PRIMARY KEY," +
                "FirstName varchar(50) NOT NULL," +
                "LastName varchar(50) NOT NULL," +
                "EmailAddress varchar(150)," +
                "PhoneNumber varchar(10)," +
                "Balance money" +
                ");";

            string sql_vendor = "" +
                "CREATE TABLE Vendor (" +
                "VendorID int NOT NULL PRIMARY KEY," +
                "VendorName varchar(50) NOT NULL," +
                "ShortName varchar(50) NOT NULL," +
                "Address varchar(150)," +
                "PhoneNumber varchar(10)," +
                "Balance money" +
                ");";

            string sql_employee = "" +
                "CREATE TABLE Employee (" +
                "EmployeeID int NOT NULL PRIMARY KEY," +
                "FirstName varchar(50) NOT NULL," +
                "LastName varchar(50) NOT NULL," +
                "Address varchar(150)," +
                "PhoneNumber varchar(10)," +
                "DiscountRate float" +
                ");";

            string sql_product = "" +
                "CREATE TABLE Product (" +
                "ProductID int NOT NULL PRIMARY KEY," +
                "Name varchar(50) NOT NULL," +
                "Code varchar(10) NOT NULL," +
                "VendorID int FOREIGN KEY REFERENCES Vendor(VendorID)," +
                "Price money NOT NULL," +
                "OnSaleFlag bit NOT NULL," +
                "SaleRate float" +
                ");";

            string sql_download = "" +
                "CREATE TABLE Product (" +
                "DownloadID int NOT NULL PRIMARY KEY," +
                "Name varchar(50) NOT NULL," +
                "Code varchar(10) NOT NULL," +
                "WebAddress varchar(150)," +
                "VendorID int FOREIGN KEY REFERENCES Vendor(VendorID)," +
                "Price money NOT NULL," +
                "OnSaleFlag bit NOT NULL," +
                "SaleRate float" +
                ");";

            string sql_training = "" +
                "CREATE TABLE Product (" +
                "TrainingID int NOT NULL PRIMARY KEY," +
                "Name varchar(50) NOT NULL," +
                "Code varchar(10) NOT NULL," +
                "WebAddress varchar(150)," +
                "ExpirationDays int," +
                "VendorID int FOREIGN KEY REFERENCES Vendor(VendorID)," +
                "Price money NOT NULL," +
                "OnSaleFlag bit NOT NULL," +
                "SaleRate float" +
                ");";

            string sql_transaction = "" +
                "CREATE TABLE Transaction (" +
                "TransID int NOT NULL PRIMARY KEY," +
                "DateTime datetime NOT NULL," +
                "EmployeeID int NOT NULL FOREIGN KEY REFERENCES Employee(EmployeeID)," +
                "CustomerID int NOT NULL FOREIGN KEY REFERENCES Customer(CustomerID)," +
                "OriginalPrice money NOT NULL," +
                "SaleFlag bit NOT NULL," +
                "SalePrice money NOT NULL," +
                "Quantity int NOT NULL," +
                "FinalPrice money NOT NULL," +
                "PaymentMethod varchar(20)" +
                ");";


        }


        public int CreateTable()
        {
            
            String sql;
            sql = "CREATE TABLE People (" +
                "ID int NOT NULL PRIMARY KEY," +
                "FirstName nvarchar(50) NOT NULL," +
                "LastName nvarchar(50) NOT NULL," +
                "EmailAddress nvarchar(150)," +
                "PhoneNumber nvarchar(10)" +
                ");";

            using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
            {
                using (SqlCommand command = new SqlCommand(sql, SQLconn))
                {
                    using (SqlDataAdapter sql_Data = new SqlDataAdapter(command))
                    {
                        try
                        {
                            SQLconn.Open();
                            //MessageBox.Show("Connection Open!");

                            sql_Data.InsertCommand = new SqlCommand(sql, SQLconn);
                            sql_Data.InsertCommand.ExecuteNonQuery();

                            command.Dispose();
                            SQLconn.Close();

                            return 0;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error writing to SQL: " + e.Message);
                            return 1;
                        }
                    }
                }
            }

        }
        
        public int AddPerson(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            String sql = "INSERT INTO dbo.People(FirstName, LastName, EmailAddress, PhoneNumber)" +
                "VALUES('" + firstName + "', '" + lastName + "', '" + emailAddress + "', '" + phoneNumber + "')";

            using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
            {

                using (SqlCommand command = new SqlCommand(sql, SQLconn))
                {

                    using (SqlDataAdapter sql_Data = new SqlDataAdapter(command))
                    {

                        try
                        {
                            SQLconn.Open();
                            //MessageBox.Show("Connection Open!");

                            sql_Data.InsertCommand = new SqlCommand(sql, SQLconn);
                            sql_Data.InsertCommand.ExecuteNonQuery();

                            command.Dispose();
                            SQLconn.Close();

                            return 0;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error writing to SQL: " + e.Message);
                            return 1;
                        }

                    }
                }

            }

        }

        public DataTable GetAllPeople()
        {
            String sql;
            DataTable datas = new DataTable();
            sql = "SELECT * " +
                "FROM People ";

            try
            {
                using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
                {
                    SQLconn.Open();
                    //MessageBox.Show("Connection Open!");

                    using (SqlCommand cmd = new SqlCommand(sql, SQLconn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {

                            datas.Load(dr);

                        }
                    }

                    SQLconn.Close();


                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error searching SQL: " + e.Message);
            }
            return datas;
        }

        public DataTable GetPeople(string SearchField, string SearchValue)
        {
            String sql;
            DataTable datas = new DataTable();
            sql = "SELECT ID,FirstName,LastName,EmailAddress,PhoneNumber " +
                "FROM People " +
                "WHERE " + SearchField + " = '" + SearchValue + "'";

            try
            {
                using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
                {
                    SQLconn.Open();
                    //MessageBox.Show("Connection Open!");

                    using (SqlCommand cmd = new SqlCommand(sql, SQLconn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                        
                                datas.Load(dr);
                        
                        }
                    }

                    SQLconn.Close();

                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error searching SQL: " + e.Message);
            }
            return datas;
        }

        public int UpdatePerson(int iD, string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            String sql = "Update dbo.People " +
                "SET FirstName = '" + firstName +
                "', LastName = '" + lastName +
                "', EmailAddress = '" + emailAddress +
                "', PhoneNumber = '" + phoneNumber + "' " +
                "WHERE ID = " + iD + ";";

            using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
            {
                using (SqlCommand command = new SqlCommand(sql, SQLconn))
                {
                    using (SqlDataAdapter sql_Data = new SqlDataAdapter(command))
                    {
                        try
                        {
                            SQLconn.Open();
                            //MessageBox.Show("Connection Open!");

                            sql_Data.InsertCommand = new SqlCommand(sql, SQLconn);
                            sql_Data.InsertCommand.ExecuteNonQuery();

                            command.Dispose();
                            SQLconn.Close();

                            return 0;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error writing to SQL: " + e.Message);
                            return 1;
                        }
                    }
                }
            }
        }

        public int DeletePerson(int iD)
        {
            String sql = "DELETE " +
                "FROM dbo.People " +
                "WHERE ID = " + iD + ";";

            using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
            {
                using (SqlCommand command = new SqlCommand(sql, SQLconn))
                {
                    using (SqlDataAdapter sql_Data = new SqlDataAdapter(command))
                    {
                        try
                        {
                            SQLconn.Open();
                            //MessageBox.Show("Connection Open!");

                            sql_Data.InsertCommand = new SqlCommand(sql, SQLconn);
                            sql_Data.InsertCommand.ExecuteNonQuery();

                            command.Dispose();
                            SQLconn.Close();

                            return 0;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error writing to SQL: " + e.Message);
                            return 1;
                        }
                    }
                }
            }
        }

    }
}
