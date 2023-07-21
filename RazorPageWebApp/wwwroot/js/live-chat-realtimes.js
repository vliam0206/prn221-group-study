let connection;
let form = $("#live-chat-form");
let container = $("#live-chat .chat-container");
var curUserId = $(form).data('userid');
var curGroupId = $(form).data('groupid');
var typingTimer = undefined;
$(() => {
    connection = new signalR.HubConnectionBuilder().withUrl("/liveChat").build();
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
    clearTimeout(typingTimer);
    return false;
}
function check(e) {
    if (!e.shiftKey) {
        if (e.keyCode === 10 || e.keyCode === 13) {
            e.preventDefault();
            form.submit();
        } else {
            clearTimeout(typingTimer); // Xóa timer hiện tại nếu có
            connection.send("Typing", curGroupId);
        }
    }
}

function userMessage(user, message, groupid) {
    console.log(user.id);
    console.log(curUserId);
    removeTypingIndicator(user.id);
    if (groupid == curGroupId)
        if (user.id == curUserId) {
            appendMessageMe(user, message, groupid);
        } else {
            appendMessageOther(user, message, groupid);
        }
}
//doi 2 cai duoi giup tui
//
function appendMessageMe(user, message) {
    var box = $(`<div class="d-flex flex-row justify-content-end mb-4">
                    <div class="p-2 me-2 border" style="border-radius: 15px; background-color: #fbfbfb;">
                        <p class="small fw-bold mb-0 text-end" style="font-size:0.75em">${user.fullName}</p>
                        <p class="small mb-0">${message}</p> 
                    </div> 
                    <img src="${user.avatar}" alt="${user.username}" style="width: 40px; height: 40px;">
                 </div>`);
    appendToContainer(box);

}
function appendMessageOther(user, message) {
    var box = $(`<div class="d-flex flex-row justify-content-start mb-4">
                    <img src="${user.avatar}" alt="${user.username}" style="width: 40px; height: 100%;">
                    <div class="p-2 ms-2" style="border-radius: 15px; background-color: rgba(57, 192, 237,.2);">
                        <p class="small fw-bold mb-0 text-start" style="font-size:0.75em">${user.fullName}</p>
                        <p class="small mb-0">${message}</p>
                    </div>
                </div>`);
    appendToContainer(box);
}

let isTypingIndicatorRunning = false; // Biến kiểm tra trạng thái Timeout
function userTyping(user, groupId) {
    if (curGroupId !== groupId) return;
    var box = `<div id="chat-typing-${user.id}" class="d-flex  flex-row justify-content-start mb-4">
                <img src="${user.avatar}"
                    alt="${user.username}" style="width: 40px; height: 40px;">
                <div class="p-2 ms-2" style="border-radius: 15px; background-color: rgba(57, 192, 237,.2);">
                    <p class="small mb-0">Typing...</p>
                </div>
            </div>`;
    clearTimeout(typingTimer); // Xóa timer hiện tại nếu có

    if (!isTypingIndicatorRunning) {
        appendToContainer(box);
    }
        typingTimer = setTimeout(() => removeTypingIndicator(user.id), 3000);
        isTypingIndicatorRunning = true; // Đánh dấu Timeout đang chạy
}

function appendToContainer(html) {
    var toBottom = false;
    if (container[0].scrollTop + container[0].offsetHeight + 50 >= container[0].scrollHeight) toBottom = true;
    container.append(html);
    if (toBottom) container.scrollTop(container[0].scrollHeight);
}

function removeTypingIndicator(userId) {
    $(`#chat-typing-${userId}`).remove();
    isTypingIndicatorRunning = false; // Đánh dấu Timeout dừng

}