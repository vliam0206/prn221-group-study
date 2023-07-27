// notify.js

// Wrap the code in a function to avoid global namespace pollution
(function () {
    $(function () {
        var connection = new signalR.HubConnectionBuilder().withUrl("/notifyHub").build();
        var hubProxy = connection;

        hubProxy.on("ReceiveNotify", function (notification) {
            // Create a new notification item element
            var notificationItem = $("<li>").addClass("dropdown-item").attr("data-userid", notification.userId);
            var userInfo = $("<div>").addClass("user-info");
            var userAvatar = $("<img>").addClass("user-avatar").attr("src", notification.avatarUrl);
            var userName = $("<span>").addClass("user-name").text(notification.userName);
            var userEmail = $("<span>").addClass("user-email").text(notification.email);
            var notificationContent = $("<span>").addClass("notification-content").text(notification);

            // Append the elements to the dropdown menu
            $(".dropdown-menu").prepend(notificationItem.append(userInfo.append(userAvatar, userName, userEmail), notificationContent));

        console.log("Received notification:", notification);
    });
  
    connection.start()
     
});
}) ();

