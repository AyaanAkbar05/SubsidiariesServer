using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ParentCompanyModel;

[Table("ParentCompany")]
public partial class ParentCompany
{
    [Key]
    public int ParentCompanyId { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("ParentCompany")]
    public virtual ICollection<Subsidiary> Subsidiaries { get; set; } = new List<Subsidiary>();
}
