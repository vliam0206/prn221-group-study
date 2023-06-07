using Domain.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;

public class Tag : BaseEntity
{
    #region Properties
    public string Name { get; set; } = null!;
    public Guid GroupId { get; set; }
    #endregion

    #region DB Relatioship    
    public Group Group { get; set; } = null!;
    public ICollection<TagInPost> TagInPosts { get; set; } = new HashSet<TagInPost>();
    #endregion
}
