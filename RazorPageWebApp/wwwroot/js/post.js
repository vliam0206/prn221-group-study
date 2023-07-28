async function toggleLike(entity) {
    e = entity.event || window.event;
    e.preventDefault();
    var postId = $(entity).data('postid');
    var groupId = $(entity).data('groupid');
    var url = new URL(location);
    url.searchParams.append('handler', "Like");
    await $.ajax({
        url: url,
        type: 'POST',
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success:  function (res) {
            console.log(res);
            if (res == 'Like') {
                $(entity).addClass('active');
                $(entity).data('value', parseInt($(entity).data('value')) + 1)
                $(entity).text(`${$(entity).data('value')} Likes`)
                callLikeNotification(postId);
            } else if (res == "Unlike") {
                $(entity).removeClass('active');
                $(entity).data('value', parseInt($(entity).data('value')) - 1)
                $(entity).text(`${$(entity).data('value')} Likes`)
                callLikeNotification(postId);
            } else {
                console.log("Nothing Happened")
            }
        },
        data: {
            postId: postId,
            groupId: groupId
        },
        error: (err, e, r) => {
            console.log(err);
            console.log(e);
            console.log(r);
        }
    })
    return false;
}
function callLikeNotification(postId) {
    var type = 'like'
    callNotification(postId, type);
}