using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Model
{
    public interface IAuditable
    {
        DateTime CreateDate { get; set; }

        int? CreatedBy { get; set; }

        DateTime? UpdateDate { get; set; }

        int? UpdatedBy { get; set; }
    }
}
