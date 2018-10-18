using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Advice_Entry_System.Entity
{

    public class Advice
    {
        public enum RequestReply
        {
            Yes,
            No,
            NotAssigned
        }


        private long m_ID;
        private string m_Branch_Code;
        private string m_GSL;
        private char m_Dr_Cr;
        private char m_O_R;
        private long m_Amount;
        private string m_Advice_No;
        private string m_Description;
        private string m_user;
        private string m_type;
        private DateTime m_transactiondate;
        private RequestReply m_HO_Reply;
        private RequestReply m_Branch_Reply;
        private string m_Message;
        private string m_Final_Action;
        private DateTime m_Final_Action_Date;
        private char m_Transaction_Status;
        private long m_Branch_Transaction_Number;
        private long m_HO_Transaction_Number;



        /// <summary>
        /// ////////////////// Class Constructors /////////////////////
        /// </summary>
        public Advice()
        { }

        public Advice(long ID, string Branch_Code, string GSL, char Dr_Cr, long Amount, string Advice_No, string Description, string user, string type, DateTime TransactionDate, RequestReply Branch_Reply, RequestReply HO_Reply, string Message, char O_R, string Final_Action, DateTime Final_Action_Date, char Transaction_Status)
        {
            m_ID = ID;
            m_Branch_Code = Branch_Code;
            m_GSL = GSL;
            m_Dr_Cr = Dr_Cr;
            m_Amount = Amount;
            m_Advice_No = Advice_No;
            m_Description = Description;
            m_user = user;
            m_type = type;
            m_transactiondate = TransactionDate;
            m_Branch_Reply = Branch_Reply;
            m_HO_Reply = HO_Reply;
            m_Message = Message;
            m_O_R = O_R;
            m_Final_Action = Final_Action;
            m_Final_Action_Date = Final_Action_Date;
            m_Transaction_Status = Transaction_Status;
        }

        public Advice(long ID, string Branch_Code, string GSL, char Dr_Cr, long Amount, string Advice_No, string Description, string user, string type, DateTime TransactionDate, RequestReply Branch_Reply, RequestReply HO_Reply, string Message, char O_R, string Final_Action, DateTime Final_Action_Date, char Transaction_Status, long Branch_Transaction_Number, long HO_Transaction_Number)
        {
            m_ID = ID;
            m_Branch_Code = Branch_Code;
            m_GSL = GSL;
            m_Dr_Cr = Dr_Cr;
            m_Amount = Amount;
            m_Advice_No = Advice_No;
            m_Description = Description;
            m_user = user;
            m_type = type;
            m_transactiondate = TransactionDate;
            m_Branch_Reply = Branch_Reply;
            m_HO_Reply = HO_Reply;
            m_Message = Message;
            m_O_R = O_R;
            m_Final_Action = Final_Action;
            m_Final_Action_Date = Final_Action_Date;
            m_Transaction_Status = Transaction_Status;
            m_Branch_Transaction_Number = Branch_Transaction_Number;
            m_HO_Transaction_Number = HO_Transaction_Number;
        }

        public Advice(long ID, string Branch_Code, string GSL, char Dr_Cr, long Amount, string Advice_No, string Description, string user, string type, DateTime TransactionDate, RequestReply Branch_Reply, RequestReply HO_Reply, string Message, char O_R)
        {
            m_ID = ID;
            m_Branch_Code = Branch_Code;
            m_GSL = GSL;
            m_Dr_Cr = Dr_Cr;
            m_Amount = Amount;
            m_Advice_No = Advice_No;
            m_Description = Description;
            m_user = user;
            m_type = type;
            m_transactiondate = TransactionDate;
            m_Branch_Reply = Branch_Reply;
            m_HO_Reply = HO_Reply;
            m_Message = Message;
            m_O_R = O_R;
        }

        public Advice(string Branch_Code, string GSL, char Dr_Cr, long Amount, string Advice_No, string Description, string user, string type, DateTime TransactionDate, char O_R, DateTime Final_Action_Date)
        {
            m_ID = -1;
            m_Branch_Code = Branch_Code;
            m_GSL = GSL;
            m_Dr_Cr = Dr_Cr;
            m_Amount = Amount;
            m_Advice_No = Advice_No;
            m_Description = Description;
            m_user = user;
            m_type = type;
            m_transactiondate = TransactionDate;
            m_Branch_Reply = RequestReply.NotAssigned;
            m_HO_Reply = RequestReply.NotAssigned;
            m_Message = "NotAssigned";
            m_O_R = O_R;
            m_Final_Action = "0";
            m_Final_Action_Date = Final_Action_Date;
            m_Transaction_Status = Convert.ToChar("E");
            m_Branch_Transaction_Number = 0;
            m_HO_Transaction_Number = 0;
        }

        /// <summary>
        /// //////////// Class Properties ////////////////////////////////
        /// </summary>

        public string Branch_Code
        {
            get { return m_Branch_Code; }
            set { m_Branch_Code = value; }
        }

        public long ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string GSL
        {
            get { return m_GSL; }
            set { m_GSL = value; }
        }

        public char Dr_Cr
        {
            get { return m_Dr_Cr; }
            set { m_Dr_Cr = value; }
        }

        public char O_R
        {
            get { return m_O_R; }
            set { m_O_R = value; }
        }

        public long Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        public string Advice_No
        {
            get { return m_Advice_No; }
            set { m_Advice_No = value; }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        public string user
        {
            get { return m_user; }
            set { m_user = value; }
        }

        public string type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public DateTime transactiondate
        {
            get { return m_transactiondate; }
            set { m_transactiondate = value; }
        }

        public RequestReply HO_Reply
        {
            get { return m_HO_Reply; }
            set { m_HO_Reply = value; }
        }

        public RequestReply Branch_Reply
        {
            get { return m_Branch_Reply; }
            set { m_Branch_Reply = value; }
        }

        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }

        public string Final_Action
        {
            get { return m_Final_Action; }
            set { m_Final_Action = value; }
        }

        public char Transaction_Status
        {
            get { return m_Transaction_Status; }
            set { m_Transaction_Status = value; }
        }

        public DateTime Final_Action_Date
        {
            get { return m_Final_Action_Date; }
            set { m_Final_Action_Date = value; }
        }

        public long Branch_Transaction_Number
        {
            get { return m_Branch_Transaction_Number; }
            set { m_Branch_Transaction_Number = value; }
        }

        public long HO_Transaction_Number
        {
            get { return m_HO_Transaction_Number; }
            set { m_HO_Transaction_Number = value; }
        }
    }
}
