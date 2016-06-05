using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Chat.Dal
{
    public class usersDal
    {
        private users ToModel(DataRow row)
        {
            users model = new users();
            model.UserID = (System.Int32?)SqlHelper.FromDbValue(row["UserID"]);
            model.NickName = (System.String)SqlHelper.FromDbValue(row["NickName"]);
            model.Pwd = (System.String)SqlHelper.FromDbValue(row["Pwd"]);
            model.Sex = (System.String)SqlHelper.FromDbValue(row["Sex"]);
            model.status = (System.Int32?)SqlHelper.FromDbValue(row["status"]);
            model.logintime = (System.DateTime?)SqlHelper.FromDbValue(row["logintime"]);
            model.logouttime = (System.DateTime?)SqlHelper.FromDbValue(row["logouttime"]);
            model.last_msg_id = (System.Int32?)SqlHelper.FromDbValue(row["last_msg_id"]);
            return model;
        }

        public IEnumerable<users> ListAll()
        {
            List<users> list = new List<users>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM users");
            foreach (DataRow row in dt.Rows)
            {
                users model = ToModel(row);
                list.Add(model);
            }
            return list;
        }

        public users GetByRowGuid(string RowGuid)
        {
            DataTable table = SqlHelper.ExecuteDataTable("SELECT * FROM users WHERE RowGuid=@RowGuid", new SqlParameter("@RowGuid", RowGuid));
            if (table.Rows.Count <= 0) { return null; }
            else if (table.Rows.Count == 1) { return ToModel(table.Rows[0]); }
            else { throw new System.Exception(); }
        }

        public void DeleteByRowGuid(string RowGuid)
        {
            SqlHelper.ExecuteNonQuery("DELETE FROM users WHERE RowGuid=@RowGuid", new SqlParameter("@RowGuid", RowGuid));
        }

        public void Update(users model)
        {
            SqlHelper.ExecuteNonQuery("UPDATE users SET "
+ "[UserID]=@UserID,"
+ "[NickName]=@NickName,"
+ "[Pwd]=@Pwd,"
+ "[Sex]=@Sex,"
+ "[status]=@status,"
+ "[logintime]=@logintime,"
+ "[logouttime]=@logouttime,"
+ "[last_msg_id]=@last_msg_id"
+ " WHERE RowGuid=@RowGuid", new SqlParameter("@UserID", SqlHelper.ToDbValue(model.UserID)),
new SqlParameter("@NickName", SqlHelper.ToDbValue(model.NickName)),
new SqlParameter("@Pwd", SqlHelper.ToDbValue(model.Pwd)),
new SqlParameter("@Sex", SqlHelper.ToDbValue(model.Sex)),
new SqlParameter("@status", SqlHelper.ToDbValue(model.status)),
new SqlParameter("@logintime", SqlHelper.ToDbValue(model.logintime)),
new SqlParameter("@logouttime", SqlHelper.ToDbValue(model.logouttime)),
new SqlParameter("@last_msg_id", SqlHelper.ToDbValue(model.last_msg_id)));
        }

        public void Insert(users model)
        {
            SqlHelper.ExecuteNonQuery("INSERT INTO users("
             + "[UserID],"
             + "[NickName],"
             + "[Pwd],"
             + "[Sex],"
             + "[status],"
             + "[logintime],"
             + "[logouttime],"
             + "[last_msg_id]) VALUES("
             + "@UserID,"
             + "@NickName,"
             + "@Pwd,"
             + "@Sex,"
             + "@status,"
             + "@logintime,"
             + "@logouttime,"
             + "@last_msg_id) ",
            new SqlParameter("@UserID", SqlHelper.ToDbValue(model.UserID)),
            new SqlParameter("@NickName", SqlHelper.ToDbValue(model.NickName)),
            new SqlParameter("@Pwd", SqlHelper.ToDbValue(model.Pwd)),
            new SqlParameter("@Sex", SqlHelper.ToDbValue(model.Sex)),
            new SqlParameter("@status", SqlHelper.ToDbValue(model.status)),
            new SqlParameter("@logintime", SqlHelper.ToDbValue(model.logintime)),
            new SqlParameter("@logouttime", SqlHelper.ToDbValue(model.logouttime)),
            new SqlParameter("@last_msg_id", SqlHelper.ToDbValue(model.last_msg_id)));
        }

    }
}
