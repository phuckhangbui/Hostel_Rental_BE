namespace DAO
{
    public class Utils
    {
        public string FormatCurrency(double amount)
        {
            return amount.ToString("#,##0") + " VNĐ";
        }
    }
}
