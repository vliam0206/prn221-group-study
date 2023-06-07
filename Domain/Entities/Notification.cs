using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Notification : BaseEntity
{
    #region Properties
    public Guid? FromAccountId { get; set; } // not create relationship here, just for save data
    public string Content { get; set; } = null!;
    public NotiStatusEnum Status { get; set; }
    public NotiTypeEnum Type { get; set; }    
    public Guid AccountRecievedId { get; set; }           
    #endregion

    #region DB Relationship
    public Account AccountRecieved { get; set; } = null!;
    #endregion

}
