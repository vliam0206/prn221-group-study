using Domain.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Posts;
public class Post : BaseEntity
{
    #region Properties 
    public string Topic { get; set; } = null!;
    public string Content { get; set; } = null!;
    public byte[]? Image { get; set; }
    public Guid GroupId { get; set; }
    #endregion

    #region DB Relatioship    
    public Group Group { get; set; } = null!;
    public Account AccountCreated { get; set; } = null!;
    public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    public ICollection<Comment> Comments { get;set; } = new HashSet<Comment>();
    public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    public ICollection<TagInPost> TagInPosts { get; set; } = new HashSet<TagInPost>();
    #endregion
}
