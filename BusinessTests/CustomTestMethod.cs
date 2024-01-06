using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessTests
{
    public class CustomTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            using (var scope = new TransactionScope())
            {
                var result = Invoke(testMethod);
                scope.Dispose();
                return result;
            }
        }

        private TestResult[] Invoke(ITestMethod testMethod)
        {
            return new[] { testMethod.Invoke(null) };
        }
    }
}
