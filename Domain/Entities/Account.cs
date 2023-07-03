using Domain.Entities.Groups;
using Domain.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Account : BaseEntity
{
    #region Properties
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
    public bool IsBanned { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    #endregion

    public string FullName => FirstName + LastName;

    #region DB Relationship
    public ICollection<AccountInGroup> AccountInGroups { get; set; } = new HashSet<AccountInGroup>();
    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public ICollection<Comment> ReplyComments { get; set; } = new HashSet<Comment>();
    public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
    #endregion
}
