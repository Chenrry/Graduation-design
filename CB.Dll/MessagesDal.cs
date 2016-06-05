using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Chat.Dal
{
    public class MessagesDal
    {
        private Messages ToModel(DataRow row)
        {
            Messages model = new Messages();
            model.Msg_ID = (System.Int32?)SqlHelper.FromDbValue(row["Msg_ID"]);
            model.Msg_con = (System.String)SqlHelper.FromDbValue(row["Msg_con"]);
            model.Msg_send = (System.Int32?)SqlHelper.FromDbValue(row["Msg_send"]);
            model.Msg_rec = (System.Int32?)SqlHelper.FromDbValue(row["Msg_rec"]);
            model.Msg_sendTime = (System.DateTime?)SqlHelper.FromDbValue(row["Msg_sendTime"]);
            return model;
        }

        public IEnumerable<Messages> ListAll()
        {
            List<Messages> list = new List<Messages>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM Messages");
            foreach (DataRow row in dt.Rows)
            {
                Messages model = ToModel(row);
                list.Add(model);
            }
            return list;
        }

        public Messages GetByRowGuid(string RowGuid)
        {
            DataTable table = SqlHelper.ExecuteDataTable("SELECT * FROM Messages WHERE RowGuid=@RowGuid", new SqlParameter("@RowGuid", RowGuid));
            if (table.Rows.Count <= 0) { return null; }
            else if (table.Rows.Count == 1) { return ToModel(table.Rows[0]); }
            else { throw new System.Exception(); }
        }

        public void DeleteByRowGuid(string RowGuid)
        {
            SqlHelper.ExecuteNonQuery("DELETE FROM Messages WHERE RowGuid=@RowGuid", new SqlParameter("@RowGuid", RowGuid));
        }

        public void Update(Messages model)
        {
            SqlHelper.ExecuteNonQuery("UPDATE Messages SET "
+ "[Msg_ID]=@Msg_ID,"
+ "[Msg_con]=@Msg_con,"
+ "[Msg_send]=@Msg_send,"
+ "[Msg_rec]=@Msg_rec,"
+ "[Msg_sendTime]=@Msg_sendTime"
+ " WHERE RowGuid=@RowGuid", new SqlParameter("@Msg_ID", SqlHelper.ToDbValue(model.Msg_ID)),
new SqlParameter("@Msg_con", SqlHelper.ToDbValue(model.Msg_con)),
new SqlParameter("@Msg_send", SqlHelper.ToDbValue(model.Msg_send)),
new SqlParameter("@Msg_rec", SqlHelper.ToDbValue(model.Msg_rec)),
new SqlParameter("@Msg_sendTime", SqlHelper.ToDbValue(model.Msg_sendTime)));
        }

        public void Insert(Messages model)
        {
            SqlHelper.ExecuteNonQuery("INSERT INTO Messages("
             + "[Msg_ID],"
             + "[Msg_con],"
             + "[Msg_send],"
             + "[Msg_rec],"
             + "[Msg_sendTime]) VALUES("
             + "@Msg_ID,"
             + "@Msg_con,"
             + "@Msg_send,"
             + "@Msg_rec,"
             + "@Msg_sendTime) ",
            new SqlParameter("@Msg_ID", SqlHelper.ToDbValue(model.Msg_ID)),
            new SqlParameter("@Msg_con", SqlHelper.ToDbValue(model.Msg_con)),
            new SqlParameter("@Msg_send", SqlHelper.ToDbValue(model.Msg_send)),
            new SqlParameter("@Msg_rec", SqlHelper.ToDbValue(model.Msg_rec)),
            new SqlParameter("@Msg_sendTime", SqlHelper.ToDbValue(model.Msg_sendTime)));
        }

    }
}
