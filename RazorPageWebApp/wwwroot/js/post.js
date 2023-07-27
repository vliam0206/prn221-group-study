var errr = '';
function toggleLike(entity) {
    var postId = $(entity).data('postid');
    var groupId = $(entity).data('groupid');
    var url = new URL(location);
    url.searchParams.append('handler', "Like");
    $.ajax({
        url: url,
        method: 'get',
        success: function (res) {
            console.log(res);
            if (res == 'Like') {
                $(entity).addClass('active');
                $(entity).data('value',parseInt($(entity).data('value')) +1)
            } else {
                $(entity).removeClass('active');
                $(entity).data('value',parseInt($(entity).data('value')) -1)
            }
            $(entity).text(`${$(entity).data('value')} Likes`)
        },
        data: {
            postId: postId,
            groupId: groupId
        },
        error: (err, e, r) => {
            errr = err;
            console.log(err);
            console.log(e);
            console.log(r);
        }
    })
    return false;
}