namespace Chat.Dal
{
    public class users
    {
        public System.Int32? UserID { get; set; }
        public System.String NickName { get; set; }
        public System.String Pwd { get; set; }
        public System.String Sex { get; set; }
        public System.Int32? status { get; set; }
        public System.DateTime? logintime { get; set; }
        public System.DateTime? logouttime { get; set; }
        public System.Int32? last_msg_id { get; set; }
    }
}
