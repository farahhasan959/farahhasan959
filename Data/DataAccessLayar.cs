using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace RightProject.Data
{
    public class DataAccessLayar
    {
        SqlConnection con;
        public string a = string.Empty;
        public bool state = false;
        public DataAccessLayar()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-GP709LE\\MSSQLSERVER1;Initial Catalog=final;Integrated Security=SSPI;");
        }
        public void open()
        {
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
        }
        public void close()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        public DataTable SelectData(string Stored_procedure, SqlParameter[] param)
        {
            state = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Stored_procedure;
            cmd.Connection = con;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                state = true;
            }
            catch
            {
                state = false;
            }
            return dt;
        }
        public void ExecuteCommand(string Stored_procedure,SqlParameter[] param)
        {
            try
            {
                state = false;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = Stored_procedure;
                cmd.Connection = con;
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                }
                cmd.ExecuteNonQuery();
                state = true;

            }
            catch(Exception ex)
            {
                state = false;

            }
        }
        public string GetString(string Stored_procedure,SqlParameter[] param)
        {
            a = string.Empty;
            state = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = Stored_procedure;
            cmd.Connection = con;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
            }
            try
            {
                open();
                a = cmd.ExecuteScalar().ToString();
                close();
                state = true;
            }
            catch (Exception ex)
            {
                state = false;

            }
            return a;
        }
        
        
    }

}
