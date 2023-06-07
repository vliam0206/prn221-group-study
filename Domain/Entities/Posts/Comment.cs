using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;

public class Comment : BaseEntity
{
    #region Properties
    public string Content { get; set; } = null!;
    public Guid PostId { get; set; }
    public Guid? AccountRepliedId { get; set; }
    public Guid? CommentRepliedId { get; set; }
    #endregion

    #region DB Relationship
    public Account AccountCreated { get; set; } = null!;
    public Post Post { get; set; } = null!;
    public Account? AccountReplied { get; set; }
    public Comment? CommentReplied { get; set; }
    public ICollection<Comment> ReplyComments { get; set; } = new HashSet<Comment>();
    #endregion
}
