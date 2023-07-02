using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;

public class Comment : BaseEntity
{
    #region Properties
    [Required]
    [DisplayName("Comment")]
    public string Content { get; set; } = null!;
    [Required]
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
