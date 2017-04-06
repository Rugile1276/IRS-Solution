using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRS_tests.Additional
{
    class Helper
    {
        public Helper()
        {
        }

        public void WaitForLoadFinish(int number)
        {
            System.Threading.Thread.Sleep(number);
        }
    }
}
