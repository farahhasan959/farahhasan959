using RightProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RightProject.Data
{
    public  class AppAuthentination
    {
        private readonly finalContext _context;
        public  AppAuthentination(finalContext context)
        {
            _context = context;
        }
        public  int GetRoleId(string roleName)
        {

            /* string str = string.Empty;
             DataTable dt = new DataTable();
             SqlParameter[] param = new SqlParameter[1];
             param[0] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 150);
             param[0].Value = roleName;
             DataAccessLayar dal = new DataAccessLayar();
             dt = dal.SelectData("GetMemberId", param);
             if (dt.Rows.Count >0)
             {
                 str = dt.Rows[0][0].ToString();
             }

             return str;*/
            int id = _context.AppRoles.Where(a => a.RoleName == roleName).Select(a => a.id).SingleOrDefault() ;
            return id;
        }
    }
}
