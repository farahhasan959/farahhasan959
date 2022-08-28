using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace RightProject.Data
{
    public class Users
    {
        DataAccessLayar dal = new DataAccessLayar();
        DataTable dt = new DataTable();
        public bool state = false;
        public DataTable CheckUserNameExist(string username)
        {
            state = false;
            dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@username", SqlDbType.NVarChar, 100);
            param[0].Value = username;
            dt = dal.SelectData("CheckUserNameExist", param);
            this.state = dal.state;
            return dt;
        }
        public DataTable checkLogin(string username,string password){
            state = false;
            dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            param[0].Value = username;
            param[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 150);
            param[1].Value = password;
            dt = dal.SelectData("userLogin", param);
            this.state = dal.state;
            return dt;



        }
    }
}
