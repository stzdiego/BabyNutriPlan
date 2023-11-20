using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BabyNutriPlan.Shared.Bases
{
    /// <summary>
    /// Base class for audit fields
    /// </summary>
    public class BaseAudit
    {
        [Key]
        [Column("rowid", TypeName = "integer")]
        public int RowId { get; set; }

        [Timestamp]
        [Column("rowversion", TypeName = "bytea")]
        [DataType(DataType.Currency)]
        public byte[]? RowVersion { get; set; }

        [Column("creation_date", TypeName = "timestamp with time zone")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Column("modification_date", TypeName = "timestamp with time zone")]
        [DataType(DataType.DateTime)]
        public DateTime? ModificationDate { get; set; }
    }
}