﻿using Domain.Entities.Groups;
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
                Password = "abc123".Hash(),
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
                Password = "abc123".Hash(),
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
                Password = "abc123".Hash(),
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
        var accountInGroups = new List<AccountInGroup>();
        var random = new Random();

        for (int i = 0; i < 20; i++)
        {
            var accInGroup = new AccountInGroup
            {
                Role = RoleEnum.Admin,
                AccountId = groups[i].AccountCreatedID.Value,
                GroupId = groups[i].Id
            };
            accountInGroups.Add(accInGroup);

            for(int j = 0; j < 3; j++)
            {
                var accId = accounts[j].Id;
                if (accId != groups[i].AccountCreatedID)
                {
                    var accInGroup2 = new AccountInGroup
                    {
                        Role = RoleEnum.Member,
                        AccountId = accId,
                        GroupId = groups[i].Id
                    };
                    accountInGroups.Add(accInGroup2);
                }
            }

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
        var cmtList = new List<Comment>();
        foreach (var post in posts)
        {
            var comments = new Comment[]
            {
                new Comment
                {
                    Content = "Amazing goodjob!!!",
                    PostId = post.Id,
                    AccountRepliedId = null,
                    CommentRepliedId = null,
                    AccountCreatedID = accounts[random.Next(0, accounts.Count-1)].Id
                },
                new Comment
                {
                    Content = "I like it!!!",
                    PostId = post.Id,
                    AccountRepliedId = null,
                    CommentRepliedId = null,
                    AccountCreatedID = accounts[random.Next(0, accounts.Count-1)].Id
                }
            };
            cmtList.AddRange(comments);
        }

        context.Comments.AddRange(cmtList);
        context.SaveChanges();
    }

    public static void InitializeLike(AppDBContext context)
    {
        if (context.Likes.Any())
        {
            return;
        }

        var random = new Random();
        var posts = context.Posts.ToList();
        var accounts = context.Accounts.ToList();
        var likeList = new List<Like>();
        foreach (var post in posts)
        {
            var likes = new Like[]
            {
                new Like
                {
                    PostId = post.Id,
                    Status = LikeStatusEnum.Like,
                    AccountCreatedID = accounts[0].Id
                },
                new Like
                {
                    PostId = post.Id,
                    Status = LikeStatusEnum.Unlike,
                    AccountCreatedID = accounts[1].Id
                }
            };
            likeList.AddRange(likes);
        }

        context.Likes.AddRange(likeList);
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
        var postList = new List<Post>();
        var random = new Random();

        foreach (var group in groups)
        {
            var posts = new Post[]
            {
                new Post
                {
                    Topic = $"{group.Name}: Post 1 - Group Study",
                    Content = SampleData.POST_CONTENT_1,
                    Image = SampleData.image_1,
                    GroupId = group.Id,
                    AccountCreatedID = accounts[random.Next(0,2)].Id,
                    Status = PostStatusEnum.Approved
                },
                new Post
                {
                    Topic = $"{group.Name}: Post 2 - Animal Kingdom",
                    Content = SampleData.POST_CONTENT_2,
                    Image = SampleData.image_2,
                    GroupId = group.Id,
                    AccountCreatedID = accounts[random.Next(0,2)].Id,
                    Status = PostStatusEnum.Approved
                }
            };
            postList.AddRange(posts);
        }
        context.Posts.AddRange(postList);
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
            new Tag { Name = "animal", GroupId = groups[0].Id },
            new Tag { Name = "group-study", GroupId = groups[1].Id }
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
        var tagInPosts = new List<TagInPost>();
        foreach (var post in posts)
        {
            var tag = new TagInPost[]
            {
            new TagInPost { TagID= tags[0].Id, PostId = post.Id },
            new TagInPost { TagID = tags[1].Id, PostId = post.Id }
            };
            tagInPosts.AddRange(tag);
        }
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
                Status = NotiStatusEnum.Unread,
                Type = NotiTypeEnum.Like,
                AccountRecievedId = accounts[1].Id
            },
            new Notification
            {
                Content = "Notification 2",
                Status = NotiStatusEnum.Read,
                Type = NotiTypeEnum.Comment,
                AccountRecievedId = accounts[0].Id
            },
            new Notification
            {
                Content = "Notification 1",
                Status = NotiStatusEnum.Unread,
                Type = NotiTypeEnum.Comment,
                AccountRecievedId = accounts[1].Id
            },
            new Notification
            {
                Content = "Notification 2",
                Status = NotiStatusEnum.Read,
                Type = NotiTypeEnum.Comment,
                AccountRecievedId = accounts[1].Id
            },
            new Notification
            {
                Content = "Notification 1",
                Status = NotiStatusEnum.Unread,
                Type = NotiTypeEnum.Comment,
                AccountRecievedId = accounts[2].Id
            },
            new Notification
            {
                Content = "Notification 2",
                Status = NotiStatusEnum.Read,
                Type = NotiTypeEnum.Comment,
                AccountRecievedId = accounts[2].Id
            }
        };

        context.Notifications.AddRange(notifications);
        context.SaveChanges();
    }
}

