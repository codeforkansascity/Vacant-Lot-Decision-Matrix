using System.Collections.Generic;
using System.Linq;

namespace CFKC.VPV.Models
{
    // For simple but unique responses ** be mindful of where you use
    // as genereics can make bug tracking difficult and cumbersome

    public class ResultResponse<T> where T : class
    {
        public T[] Results { get; set; } = new T[0];

        public string Status { get; set; } = "NO_RESULTS";

        public ResultResponse(IEnumerable<T> results, string statusPostfix)
        {
            if (results != null)
            {
                Results = results.ToArray();

                Status = BuildStatusString(Results.Length, statusPostfix);
            }
        }

        public ResultResponse(T result, string statusPostfix)
        {
            if (result != null)
            {
                Results = new T[] { result };
                Status = BuildStatusString(Results.Length, statusPostfix);
            }
        }
        
        /// <summary>
        /// This constructor is for empty responses only
        /// </summary>
        /// <param name="statusPostfix"></param>
        public ResultResponse (string statusPostfix)
        {
            Status = BuildStatusString(Results.Length, statusPostfix);
        }

        public static string BuildStatusString(int count, string postfix)
        {
            if (count == 0) return "NO_RESULTS_" + postfix;

            else if (count == 1) return "SINGLE_RESULT_" + postfix;

            return "MANY_RESULTS_" + postfix;
        }

    }
}
