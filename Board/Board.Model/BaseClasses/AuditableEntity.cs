using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Model
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditable
    {
        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }

        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdateDate { get; set; }

        [ScaffoldColumn(false)]
        public int? UpdatedBy { get; set; }
    }
}
