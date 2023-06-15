using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class BaseEntity
{
    #region Properties
    public Guid Id { get; set; } = new Guid(); // always remember to assign NEWID() to the Id field in configuration
    public DateTime CreationDate { get; set; } = DateTime.Now; // always remember to assign the creation date in configuration
    public Guid? AccountCreatedID { get; set; }
    public DateTime? ModificationDate { get; set;}
    public Guid? ModifiedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public Guid? DeletedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    #endregion
}
