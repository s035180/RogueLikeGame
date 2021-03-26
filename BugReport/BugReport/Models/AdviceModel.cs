using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugReport.Models
{
    class AdviceModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }

        private static AdviceModel _advice;

        public static void saveAdvice(AdviceModel advice)
        {
            _advice = advice;
        }

        public static AdviceModel returnAdvice()
        {
            return _advice;
        }
    }
}
