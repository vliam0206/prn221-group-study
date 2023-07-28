// notify.js

// Wrap the code in a function to avoid global namespace pollution
var notifyConnection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();

$(function () {
    var hubProxy = notifyConnection;

    hubProxy.on("ReceiveNotify", function (notification) {
        // Assuming notification is a JavaScript object with properties: Type, Status, Id, and Content

        // Determine the CSS classes based on notification.Type and notification.Status
        const typeClass = notification.Type.toString().toLowerCase();
        const statusClass = notification.Status === "Read" ? "bg-secondary text-light" : "";

        // Create the notification item element using jQuery
        const notificationItem = $("<li>").addClass('list-group-item');

        // Create the notification link element using jQuery with all the necessary attributes and classes
        const notificationLink = $("<a>")
            .addClass(`dropdown-item ${typeClass} ${statusClass}`)
            .attr("href", `/UserScreen/HomePage?id=${notification.Id}&path=/UserScreen/HomePage`);

        // Create the notification icon element using jQuery and set its HTML content
        const notificationIcon = $("<span>")
            .addClass("notification-icon")
            .html('<i class="fa-solid fa-comment"></i>');

        // Create the notification content element using jQuery and set its text content
        const notificationContent = $("<span>")
            .addClass("notification-content")
            .text(notification.Content);

        // Append the icon and content elements to the notification link
        notificationLink.append(notificationIcon, notificationContent);

        // Append the notification link to the notification item
        notificationItem.append(notificationLink);

        // Now you can prepend the notificationItem element to the appropriate parent element in your HTML
        // For example:
        $(".dropdown-menu").prepend(notificationItem);
        console.log("Received notification:", notification);
    });
    notifyConnection.start()

});

function callNotification(postId, type) {
    $.ajax({
        url: `/Notification/create/${postId}/${type}`,
        method: 'get',
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: (res) => {
            notifyConnection.send('SendNotifyOther', res);
            console.log(res);
        },
        error: err => {
            console.log(err);
            console.log(err.status);
        }
    })
}
