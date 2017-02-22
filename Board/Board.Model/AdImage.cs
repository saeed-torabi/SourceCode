namespace Board.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AdImage
    {
        public long Id { get; set; }

        public long AdId { get; set; }

        [Required]
        [StringLength(255)]
        public string Image { get; set; }

        public virtual Ad Ad { get; set; }
    }
}
