using Infrastructure.IRepositories;
using Infrastructure.IRepositories.Groups;
using Infrastructure.Repositories.Comments;
using Infrastructure.Repositories.Posts;

namespace Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork {
    #region Fields
    private readonly IAccountRepository _accountRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IAccountInGroupRepository _accountInGroupRepository;
    private readonly INotificationRepository _notificationRepository;
    #endregion
    public UnitOfWork(IAccountRepository accountRepository, 
        IPostRepository postRepository, 
        IGroupRepository groupRepository, 
        ICommentRepository commentRepository,
        IAccountInGroupRepository accountInGroupRepository,
        INotificationRepository notificationRepository)
    {
        _accountRepository = accountRepository;
        _postRepository = postRepository;
        _groupRepository = groupRepository;
        _commentRepository = commentRepository;
        _commentRepository = commentRepository;
        _accountInGroupRepository = accountInGroupRepository;
        _notificationRepository = notificationRepository;
    }

    public IAccountRepository AccountRepository => _accountRepository;
    public ICommentRepository CommentRepository => _commentRepository;
    public IPostRepository PostRepository => _postRepository;
    public IGroupRepository GroupRepository => _groupRepository;
    public IAccountInGroupRepository AccountInGroupRepository => _accountInGroupRepository;

    public INotificationRepository NotificationRepository => _notificationRepository;
}
