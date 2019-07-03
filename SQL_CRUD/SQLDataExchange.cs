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

        public int CreateTable()
        {
            
            String sql;
            sql = "CREATE TABLE People (" +
                "ID int NOT NULL PRIMARY KEY," +
                "FirestName nvarchar(50) NOT NULL," +
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
