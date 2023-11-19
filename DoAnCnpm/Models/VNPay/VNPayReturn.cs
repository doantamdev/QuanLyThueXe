using System;
using System.Configuration;
using static System.Web.HttpContext;

namespace DoAnCnpm.Models.VNPay
{
    public class VNPayReturn
    {
        private string terminalID;
        private string clientTransacID;
        private string serverTransacID;
        private string bankCode;
        private double paymentAmount;
        private int transacStatus;
        private string returnText;

        public static readonly string RESPONSE_CODE = "00";
        public static readonly string TRANSAC_CODE = "00";
        public static readonly string HASH_SECRET = ConfigurationManager.AppSettings["vnp_HashSecret"];

        public VNPayReturn() { }

        public string TerminalID { get => this.terminalID; set => this.terminalID = value; }
        public string ClientTransacID { get => this.clientTransacID; set => this.clientTransacID = value; }
        public string ServerTransacID { get => this.serverTransacID; set => this.serverTransacID = value; }
        public double PaymentAmount { get => this.paymentAmount; set => this.paymentAmount = value; }
        public int TransacStatus { get => this.transacStatus; set => this.transacStatus = value; }
        public string ReturnText { get => this.returnText; set => this.returnText = value; }
        public string BankCode { get => this.bankCode; set => this.bankCode = value; }

        public bool ProcessResponses()
        {
            if (Current.Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = HASH_SECRET; 
                var vnpayData = Current.Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    // Get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = Current.Request.QueryString["vnp_SecureHash"];
                string TerminalID = Current.Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                string bankCode = Current.Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    this.TerminalID = TerminalID;
                    this.ClientTransacID = orderId.ToString();
                    this.ServerTransacID = vnpayTranId.ToString();
                    this.PaymentAmount = vnp_Amount;
                    this.BankCode = bankCode;
                    if (vnp_ResponseCode == TRANSAC_CODE && vnp_TransactionStatus == RESPONSE_CODE)
                    {
                        //Thanh toan thanh cong
                        this.ReturnText = "Chúc mừng quý khách thanh toán thành công";

                        return true;
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        this.ReturnText = "Có lỗi xảy ra trong quá trình xử lý.<br/>Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    this.ReturnText = "Có lỗi xảy ra trong quá trình xử lý.<br/>Mã lỗi: " + vnp_ResponseCode;
                }
            }

            return false;
        }
    }
}