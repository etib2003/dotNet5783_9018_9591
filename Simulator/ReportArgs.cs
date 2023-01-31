using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simulator
{
    /// <summary>
    /// An object for reporting the report function
    /// </summary>
    public class ReportArgs: EventArgs
    {
        public int delay { get; set; }
        public BO.Order order { get; set; }
        public string massege { get; set; }

        public ReportArgs(int _delay , BO.Order _order)
        {
            delay = _delay ;
            order = _order ;
        }

        public ReportArgs(string _massege)
        {
            massege = _massege;
            delay = -1;
        }
    }
}
