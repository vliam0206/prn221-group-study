
function toggleLike(entity) {
    var postId = $(entity).data('postId');
    var url = new URL(location);
    url.searchParams.append('handler', "Like");
    $.ajax({
        url: url,
        method: 'post',
        success: function (res) {
            console.log(res);
        },
        error: (err) => {
            console.log(err);
            console.log(err.status);
        }
    })
}