using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;

public class Attachment : BaseEntity
{
    #region Properties
    public string File { get; set; } = null!;
    public Guid PostId { get; set; }
    #endregion

    #region DB Relatioship    
    public Post Post { get; set; } = null!;
    #endregion
}
