using ApplianceTesting.DataAccessLayer.Repository;
using ApplianceTesting.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace ApplianceTesting.DataAccessLayer.Repository
{
    public class MasterPageRepository : IMasterControl
    {
        private readonly IWebHostEnvironment _hostEnvirionment;

        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public MasterPageRepository(IWebHostEnvironment hostEnvirionment)
        {
            _hostEnvirionment = hostEnvirionment;
        }
        public string getConnection()
        {
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string dbconnectionStr = dbconfig["ConnectionStrings:DefaultConnection"];

            return dbconnectionStr;
        }
        private static string FormatValue(object value)
        {
            if (value == null)
                return "NULL";

            if (value is string)
                return $"'{value}'";

            return value.ToString();
        }
        public string CommonInsertOperation(object objModel,string tblName)
        {
            try
            {
                string dbconnectionStr = getConnection().ToString();
                using (SqlConnection con = new SqlConnection(dbconnectionStr))
                {
                    string TableName = tblName;
                    string ColumnList = "";
                    string ValueList = "";
                  //  objModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");

                    Type type = objModel.GetType();
                    PropertyInfo[] properties = type.GetProperties();

                    foreach (var property in properties.Skip(1))
                    {
                        string columnName = property.Name;
                        object value = property.GetValue(objModel);

                        if (ColumnList != "")
                        {
                            ColumnList += ",";
                            ValueList += ",";
                        }

                        ColumnList += columnName;
                        ValueList += FormatValue(value);
                    }

                    SqlCommand cmd = new SqlCommand("sp_DynamicInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TableName", TableName);
                    cmd.Parameters.AddWithValue("@ColumnList", ColumnList);
                    cmd.Parameters.AddWithValue("@ValueList", ValueList);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Record Inserted";
            }
            catch (Exception ex)
            {
                return Convert.ToString(ex);
            }
        }
        public List<Dictionary<string, object>> GetRecords(string tableName, string columnList,string join=null, string condition = null)
        {
            List<Dictionary<string, object>> records = new List<Dictionary<string, object>>();
            
            string dbconnectionStr = getConnection().ToString();

            using (SqlConnection conn = new SqlConnection(dbconnectionStr))
            {
                SqlCommand cmd = new SqlCommand("sp_DynamicSelect", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@ColumnList", columnList);
                cmd.Parameters.AddWithValue("@JoinCondition", join);
                cmd.Parameters.AddWithValue("@Condition", condition);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> record = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        record[reader.GetName(i)] = reader[i];  // Add column name and value to the dictionary
                    }

                    records.Add(record);
                }
            }

            return records;
        }


        //public string AddState(StateModel stModel)
        //{
        //    try
        //    {
        //        string dbconnectionStr = getConnection().ToString();
        //        using (SqlConnection con = new SqlConnection(dbconnectionStr))
        //        {
        //            string TableName = "StateMaster"; 
        //            string ColumnList = "";
        //            string ValueList = "";
        //            stModel.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");

        //            Type type = stModel.GetType();
        //            PropertyInfo[] properties = type.GetProperties();

        //            foreach (var property in properties.Skip(1))
        //            {
        //                string columnName = property.Name;
        //                object value = property.GetValue(stModel);

        //                if (ColumnList != "")
        //                {
        //                    ColumnList += ",";
        //                    ValueList += ",";
        //                }

        //                ColumnList += columnName;
        //                ValueList += FormatValue(value);
        //            }

        //            SqlCommand cmd = new SqlCommand("sp_DynamicInsert", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@TableName", TableName);
        //            cmd.Parameters.AddWithValue("@ColumnList", ColumnList);
        //            cmd.Parameters.AddWithValue("@ValueList", ValueList);


        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        return "Record Inserted";
        //    }
        //    catch (Exception ex)
        //    {
        //        return Convert.ToString(ex);
        //    }
        //}
    }
}
