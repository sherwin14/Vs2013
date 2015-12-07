using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
namespace WenDLL
{
    public class WenFramework : IWenFramework
    {

        public WenFramework(string connectionString)
        {
            _connstring = connectionString;
        }

        /// <summary>
        /// used in SELECT Statement; (where) is Optional.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        public DataTable ExecuteSelect(string column, string table, string where = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = _connstring; 
                    conn.Open();

                    where = !String.IsNullOrEmpty(where) ? "WHERE " + where : ";";
                    using (SqlCommand comm = new SqlCommand("SELECT " + column + " from " + table + " " + where, conn))
                    {

                        using (SqlDataAdapter adapter = new SqlDataAdapter(comm))
                        {
                            adapter.Fill(dt);
                        }
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return dt;
        }

        /// <summary>
        /// Used in Updating records.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        public void ExecuteUpdate(string table, Dictionary<string, object> fields, string where)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = _connstring; // String.Format("Server={0};Database={1};User Id={2};Password={3};", Server, Database, UserID, Password);
                    conn.Open();

                    string QUERY = "UPDATE " +  table + " SET ";
                    string fieldAndValue="";
                   // string WHERE = " WHERE ";
                    foreach (KeyValuePair<string, object> field in fields) {
                        fieldAndValue = String.Format("{0}, {1} = '{2}'", fieldAndValue, field.Key, field.Value);
                    }
                    fieldAndValue = fieldAndValue.TrimStart(',').Trim();

                    QUERY += fieldAndValue;
                    
                    
                    using (SqlCommand comm = new SqlCommand(QUERY + " WHERE " + where, conn))
                    {
                     //   Debug.Write(QUERY + " " + where);

                        comm.ExecuteNonQuery();

                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }



        /// <summary>
        /// used in INSERT Statement;
        /// </summary>
        /// <param name="column"></param>
        /// <param name="table"></param>
        /// <param name="values"></param>
        public void ExecuteInsert(string table, Dictionary<string, object> fieldsAndvalues)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = _connstring;//String.Format("Server={0};Database={1};User Id={2};Password={3};", Server, Database, UserID, Password);
                    conn.Open();

                    string myFields = "";
                    string myValues = "";
                    foreach (KeyValuePair<string, object> field in fieldsAndvalues)
                    {
                        myFields = String.Format("{0},{1}", myFields, field.Key);
                        myValues = String.Format("{0},'{1}'", myValues, field.Value);
                    }

                    myFields = myFields.TrimStart(',').Trim();
                    myValues = myValues.TrimStart(',').Trim();
                    using (SqlCommand comm = new SqlCommand("INSERT INTO " + table + "(" + myFields + ") VALUES (" + myValues + ")", conn))
                    {
                          Debug.Write("INSERT INTO " + table + "(" + myFields + ") VALUES (" + myValues + "77)");
                        comm.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// used in DELETE Statement; 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        public void ExecuteDelete(string table, Dictionary<string, object> where)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {

                    conn.ConnectionString = _connstring;//String.Format("Server={0};Database={1};User Id={2};Password={3};", Server, Database, UserID, Password);
                    conn.Open();
                    string myFields = "";
                    foreach (KeyValuePair<string, object> field in where)
                    {
                        myFields = String.Format(" WHERE {0}={1}", field.Key, field.Value);
                    }

                   // myFields = myFields.TrimStart(',').Trim();

                    using (SqlCommand comm = new SqlCommand("DELETE FROM " + table + myFields, conn))
                    {
                        Debug.Write("DELETE FROM " + table + myFields);
                        comm.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// used in STORED PROCEDURE Statement;
        /// TODO: Coming soon
        /// </summary>
        /// <param name="storedprocedure"></param>
        /// <param name="param"></param>
        public void ExecuteStoredProcedure(string storedprocedure, string[] param)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {

                    conn.ConnectionString = _connstring;// String.Format("Server={0};Database={1};User Id={2};Password={3};", Server, Database, UserID, Password);
                    conn.Open();
                    
                    using (SqlCommand comm = new SqlCommand("DELETE FROM ", conn))
                    {
                        
                        comm.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }


private string _connstring;
      #region backup

        private string _server;
        private string _database;
        private string _user;
        private string _pass;
        
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        public string UserID
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Password
        {
            get { return _pass; }
            set { _pass = value; }
        }
      #endregion
    }
}
