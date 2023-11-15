using System.Globalization;
using System.Collections.Generic;

namespace DoAnCnpm.Models.VNPay
{
    public class VnPayCompare : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == y)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            CompareInfo vnpCompare = CompareInfo.GetCompareInfo("en-US");

            return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}