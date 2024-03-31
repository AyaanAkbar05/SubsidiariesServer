using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ParentCompanyModel;

[Table("Subsidiary")]
public partial class Subsidiary
{
    [Key]
    public int SubsidiaryId { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Unicode(false)]
    public string Location { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Revenue { get; set; }

    public int ParentCompanyId { get; set; }

    [ForeignKey("ParentCompanyId")]
    [InverseProperty("Subsidiaries")]
    public virtual ParentCompany ParentCompany { get; set; } = null!;
}
