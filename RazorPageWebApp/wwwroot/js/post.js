
function toggleLike(entity) {
    var postId = $(entity).data('postid');
    var url = new URL(location);
    url.searchParams.append('handler', "Like");
    $.ajax({
        url: url,
        method: 'post',
        data: {
            postId
        },
        success: function (res) {
            console.log(res);
        },
        error: (err) => {
            console.log(err);
            console.log(err.status);
        }
    })
}