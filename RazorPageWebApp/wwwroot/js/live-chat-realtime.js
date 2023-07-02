let connection = new signalR.HubConnectionBuilder().withUrl("/liveChat").build();
let form = $("#live-chat-form");
let container = $("#live-chat .chat-container");
var curUserId = $(form).data('userid');
var curGroupId = $(form).data('groupid');

$(() => {
    connection.start();
    connection.on("UserMessage", (user, message, groupid) => userMessage(user, message, groupid))
    connection.on("Typing", (user, groupId) => userTyping(user, groupId))
    form.on('submit', (e) => AddMessage(e.target));
    form.find("textarea").on('keydown', (e) => { check(e); })
});
function AddMessage(form) {
    textarea = $(form).find('textarea');
    message = textarea.val();
    if (message != null && message.length > 0) {
        connection.send("SendMessage", message, curGroupId)
        textarea.val("");
    }
    return false;
}
function check(e) {
    if (!e.shiftKey)
        if (e.keyCode === 10 || e.keyCode === 13) {
            e.preventDefault();
            form.submit();
        } else {
            connection.send("Typing", curGroupId)
        }

}
function userMessage(user, message, groupid) {
   
    console.log(user.id);
    console.log(curUserId);
    if (groupid == curGroupId)
        if (user.id == curUserId) {
            appendMessageMe(user, message);
        } else {
            appendMessageOther(user, message);
        }
}
//doi 2 cai duoi giup tui
//
function appendMessageMe(user, message) {
    if (curGroupId != groupId) return;
    var box = $(`<div class="d-flex flex-row justify-content-end mb-4">
                    <div class="p-2 me-2 border" style="border-radius: 15px; background-color: #fbfbfb;">
                        <p class="small mb-0">`+ message + `</p> 
                    </div> 
                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava2-bg.webp" alt="`+ user.username + `" style="width: 40px; height: 40px;">
                 </div>`);
    console.log('Me');
    console.log(box);
    var toBottom = false;
    if (container[0].scrollTop + container[0].offsetHeight + 50 >= container[0].scrollHeight) toBottom == true;
    container.append(box);
    if (toBottom) container.scrollTop(container.height());

}
function appendMessageOther(user, message) {
    if (curGroupId != groupId) return;
    var box = $('<div class="d-flex flex-row justify-content-start mb-4">\n' +
        '<img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava1-bg.webp"\n'
        + 'alt="avatar 1" style="width: 40px; height: 100%;">\n'
        + '<div class="p-2 ms-2" style="border-radius: 15px; background-color: rgba(57, 192, 237,.2);">'
        + '<p class="small mb-0">\n'
        + message
        + '</p>\n'
        + '</div>\n'
        + '</div >\n'
    );
    console.log('Other');
    console.log(box);
    var toBottom = false;
    if (container[0].scrollTop + container[0].offsetHeight + 50 >= container[0].scrollHeight) toBottom == true;
    container.append(box);
    if (toBottom) container.scrollTop(container.height());
}

// Add typing neu dc
function userTyping(user, groupId) {
    if (curGroupId != groupId) return;
    var box = ` <div class="d-flex flex-row justify-content-start mb-4">
                                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava1-bg.webp"
                                         alt="avatar 1" style="width: 40px; height: 40px;">
                                    <div class="p-2 ms-2" style="border-radius: 15px; background-color: rgba(57, 192, 237,.2);">
                                        <p class="small mb-0">...</p>
                                    </div>
                                </div>`;
    var toBottom = false;
    if (container[0].scrollTop + container[0].offsetHeight + 50 >= container[0].scrollHeight) toBottom == true;
    container.append(box);
    if (toBottom) container.scrollTop(container.height());
}