
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Reflection;

namespace RoleBasedDemo.Helpers
{
    public static class Common
    {
        public const string USERID = "_userID";
        public const string LOCAL_URL = "https://localhost:7155/";
        public const string FilePath = @"D:\MySCHOOLDRIVE";
        public const string USERNAME = "_user";
        public const string USERMAIL = "_email";
        public const string FULLNAME= "_fullname";
        public const string USERROLE = "_role";
        public const string USERLOCATION = "_location";
        //public const string USERID = "_userid";

        public const string UNIQUE_CODE = "_ucode";
        public const string ADMIN = "_admin";
        public const string STUDENT = "_studnet";
        public const string PARENT = "_parent";
        public const string TEACHER = "_teacher";
        public const string CLASS = "_class";

        //for error message
        public const string ERROR = "OOPS ! SOMETHING WENT WRONGN";
        public const string ResultSuccessMessage = "RECORD SAVED SUCCESSFULLY";
        public const string ResultUpdateMessage = "RECORD UPDATED SUCCESSFULLY";
        public const string SUCCESSFULL = "YOU LOGED SUCCESSFULLY";

        public static List<SelectListItem> BindSelectionTools(DataTable dt)
        {
            var ListItems =new  List<SelectListItem>();

            foreach (DataRow row in dt.Rows)
            {

                ListItems.Add(new SelectListItem
                {

                    Value = row["CODE"].ToString().Trim(),
                    Text = row["NAME"].ToString().Trim()
               });
   
            }

            return ListItems;
        }

        #region convertdatatabletolist
        public static List<T> ConvertDtToList <T>(DataTable dt) 
        {
            List<T> listitem = new();
           
            foreach (DataRow row in dt.Rows)
            {
               
               T item = GetItem<T> (row);
               listitem.Add (item);              
            }

            return listitem;
        }

        //private static T GetItem<T>(DataRow dr)
        //{

        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if(pro.Name.ToUpper() == column.ColumnName.ToUpper())
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else 
        //                continue;
        //        }
        //    }
        //    return obj;

        //}

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                PropertyInfo prop = temp.GetProperty(column.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && dr[column.ColumnName] != DBNull.Value)
                {
                    object value = Convert.ChangeType(dr[column.ColumnName], prop.PropertyType);
                    prop.SetValue(obj, value);
                }
            }
            return obj;
        }

        #endregion
    }
}