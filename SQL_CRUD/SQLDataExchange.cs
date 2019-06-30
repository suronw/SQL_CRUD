﻿using System;
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


        DataTable GetPeople(string SearchField, string SearchValue)
        {
            String sql;
            DataTable datas = new DataTable();
            sql = "SELECT ID,FirstName,LastName,EmailAddress,PhoneNumber " +
                "FROM People " +
                "WHERE " + SearchField + " = '" + SearchValue + "'";

            using (SqlConnection SQLconn = new SqlConnection(SQLCreds.Connection_String))
            {
                SQLconn.Open();
                //MessageBox.Show("Connection Open!");

                using (SqlCommand cmd = new SqlCommand(sql, SQLconn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        try
                        {
                            datas.Load(dr);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error searching SQL: " + e.Message);
                        }
                        finally
                        {
                            SQLconn.Close();
                        }
                    }
                }

                return datas;
            }
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