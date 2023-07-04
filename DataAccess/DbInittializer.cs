using Domain.Entities.Groups;
using Domain.Entities.Posts;
using Domain.Entities;
using Domain.Enums;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess;

public class DbInitializer
{
    public static void InitializeData(AppDBContext context)
    {
        InitializeAccount(context);
        InitializeGroup(context);
        InitializeAccountInGroup(context);
        InitializePost(context);
        InitializeAttachment(context);
        InitializeComment(context);
        InitializeLike(context);
        InitializeTag(context);
        InitializeTagInPost(context);
        InitializeNotification(context);
    }
    public static void InitializeAccount(AppDBContext context)
    {
        if (context.Accounts.Any())
        {
            return;
        }

        var accounts = new Account[]
        {
            new Account
            {
                Username = "lamlam",
                Password = "123".Hash(),
                Email = "v.trclam@gmail.com",
                FirstName = "Truc Lam",
                LastName = "Vo",
                Avatar = "https://png.pngtree.com/png-vector/20190130/ourlarge/pngtree-cute-girl-avatar-material-png-image_678035.jpg",
                IsBanned = false,
                IsAdmin = false
            },
            new Account
            {
                Username = "huynhphu",
                Password = "123".Hash(),
                Email = "phu@fpt.com",
                FirstName = "Phu",
                LastName = "Huynh",
                Avatar = "https://i.pinimg.com/originals/78/54/84/7854843699c1893928012a442386a129.jpg",
                IsBanned = false,
                IsAdmin = true
            },
            new Account
            {
                Username = "binhnguyen",
                Password = "123".Hash(),
                Email = "binh@fpt.com",
                FirstName = "Tranquility",
                LastName = "Nguyen",
                Avatar = "https://i.pinimg.com/originals/61/3d/c8/613dc8f6fb4fdd394490dcf93bbbc8c0.jpg",
                IsBanned = false,
                IsAdmin = true
            }

        };

        context.Accounts.AddRange(accounts);
        context.SaveChanges();
    }

    public static void InitializeAccountInGroup(AppDBContext context)
    {
        if (context.AccountInGroups.Any())
        {
            return;
        }

        var accounts = context.Accounts.ToList();
        var groups = context.Groups.ToList();

        var accountInGroups = new List<AccountInGroup>
        {
            new AccountInGroup
            {
                Role = RoleEnum.Admin,
                Status = GroupStatusEnum.Active,
                AccountId = accounts[0].Id,
                GroupId = groups[0].Id
            },
            new AccountInGroup
            {
                Role = RoleEnum.Member,
                Status = GroupStatusEnum.Pending,
                AccountId = accounts[1].Id,
                GroupId = groups[0].Id
            }
        };
        var random = new Random();
        for (int i = 1; i < 20; i++)
        {
            var accInGroup = new AccountInGroup
            {
                Role = RoleEnum.Admin,
                Status = GroupStatusEnum.Active,
                AccountId = accounts[random.Next(0, 3)].Id,
                GroupId = groups[i].Id
            };

            accountInGroups.Add(accInGroup);
        }

        context.AccountInGroups.AddRange(accountInGroups);
        context.SaveChanges();
    }

    public static void InitializeGroup(AppDBContext context)
    {
        if (context.Groups.Any())
        {
            return;
        }

        var random = new Random();
        var accounts = context.Accounts.ToList();

        var groups = new List<Group>();

        for (int i = 0; i < 20; i++)
        {
            var group = new Group
            {
                Name = "Group " + (i + 1),
                Description = "Description for Group " + (i + 1),
                Visibility = (GroupVisibilityEnum)random.Next(0, 2),
                ForceApprove = random.Next(0, 2) == 1,
                ApproveAuthority = (AuthorityEnum)random.Next(0, 2),
                BanAuthority = (AuthorityEnum)random.Next(0, 2),
                AccountCreatedID = accounts[random.Next(0, 3)].Id,
            };

            groups.Add(group);
        }

        context.Groups.AddRange(groups);
        context.SaveChanges();
    }

    public static void InitializeAttachment(AppDBContext context)
    {
        if (context.Attachments.Any())
        {
            return;
        }

        var posts = context.Posts.ToList();

        var attachments = new Attachment[]
        {
            new Attachment { File = "file1.jpg", PostId = posts[0].Id },
            new Attachment { File = "file2.png", PostId = posts[1].Id }
        };

        context.Attachments.AddRange(attachments);
        context.SaveChanges();
    }

    public static void InitializeComment(AppDBContext context)
    {
        if (context.Comments.Any())
        {
            return;
        }
        var random = new Random();
        var posts = context.Posts.ToList();
        var accounts = context.Accounts.ToList();
        var comments = new Comment[]
        {
            new Comment
            {
                Content = "Comment 1",
                PostId = posts[0].Id,
                AccountRepliedId = null,
                CommentRepliedId = null,
                AccountCreatedID = accounts[random.Next(0, accounts.Count-1)].Id
            },
            new Comment
            {
                Content = "Comment 2",
                PostId = posts[1].Id,
                AccountRepliedId = accounts[0].Id,
                CommentRepliedId = null,
                AccountCreatedID = accounts[random.Next(0, accounts.Count-1)].Id

            }
        };

        context.Comments.AddRange(comments);
        context.SaveChanges();
    }

    public static void InitializeLike(AppDBContext context)
    {
        if (context.Likes.Any())
        {
            return;
        }

        var posts = context.Posts.ToList();
        var accounts = context.Accounts.ToList();

        var likes = new Like[]
        {
            new Like { PostId = posts[0].Id, AccountCreatedID = accounts[0].Id },
            new Like { PostId = posts[1].Id, AccountCreatedID = accounts[1].Id }
        };

        context.Likes.AddRange(likes);
        context.SaveChanges();
    }

    public static void InitializePost(AppDBContext context)
    {
        if (context.Posts.Any())
        {
            return;
        }

        var groups = context.Groups.ToList();
        var accounts = context.Accounts.ToList();

        var posts = new Post[]
        {
            new Post
            {
                Topic = "Post 1",
                Content = "This is post 1, abc, xyz...",
                GroupId = groups[0].Id,
                AccountCreatedID = accounts[0].Id,
                Status = PostStatusEnum.Approved
            },
            new Post
            {
                Topic = "Post 1",
                Content = "This is post 2, abc, xyz...",
                GroupId = groups[1].Id,
                AccountCreatedID = accounts[1].Id,
                Status = PostStatusEnum.Approved
            }
        };

        context.Posts.AddRange(posts);
        context.SaveChanges();
    }

    public static void InitializeTag(AppDBContext context)
    {
        if (context.Tags.Any())
        {
            return;
        }

        var groups = context.Groups.ToList();

        var tags = new Tag[]
        {
            new Tag { Name = "Tag 1", GroupId = groups[0].Id },
            new Tag { Name = "Tag 2", GroupId = groups[1].Id }
        };

        context.Tags.AddRange(tags);
        context.SaveChanges();
    }

    public static void InitializeTagInPost(AppDBContext context)
    {
        if (context.TagInPosts.Any())
        {
            return;
        }

        var tags = context.Tags.ToList();
        var posts = context.Posts.ToList();

        var tagInPosts = new TagInPost[]
        {
            new TagInPost { TagID= tags[0].Id, PostId = posts[0].Id },
            new TagInPost { TagID = tags[1].Id, PostId = posts[1].Id }
        };

        context.TagInPosts.AddRange(tagInPosts);
        context.SaveChanges();
    }

    public static void InitializeNotification(AppDBContext context)
    {
        if (context.Notifications.Any())
        {
            return;
        }

        var accounts = context.Accounts.ToList();

        var notifications = new Notification[]
        {
            new Notification
            {
                FromAccountId = accounts[0].Id,
                Content = "Notification 1",
                Status = NotiStatusEnum.Read,
                Type = NotiTypeEnum.Info,
                AccountRecievedId = accounts[1].Id
            },
            new Notification
            {
                FromAccountId = accounts[1].Id,
                Content = "Notification 2",
                Status = NotiStatusEnum.Unread,
                Type = NotiTypeEnum.Alert,
                AccountRecievedId = accounts[0].Id
            }
        };

        context.Notifications.AddRange(notifications);
        context.SaveChanges();
    }    
}

