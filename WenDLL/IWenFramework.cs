using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WenDLL
{
    interface IWenFramework
    {
        
        DataTable ExecuteSelect(string column, string table, string where = "");
        void ExecuteUpdate(string table, Dictionary<string, object> fields, string where);
        void ExecuteInsert(string table, Dictionary<string, object> fieldsAndvalues);
        void ExecuteDelete(string table, Dictionary<string, object> where);
        void ExecuteStoredProcedure(string storedprocedure, string[] param);

    }
}
