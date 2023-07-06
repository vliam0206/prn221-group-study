// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function keyValueArrayToObject(array) {
    var dataObject = {};
    for (var i = 0; i < array.length; i++) {
        var pair = array[i];
        var key = pair.name;
        var value = pair.value;
        console.log(key)
        console.log(value)
        dataObject[key] = value;
    }
    return dataObject;
}

//$(document).ready(function () {
//    $('#searchInput').on('input', function () {
//        var searchText = $(this).val();

//        // Send an AJAX request to the server
//        $.ajax({
//            url: '/Search', // Replace with the actual URL of your search handler
//            type: 'GET',
//            data: { searchText: searchText },
//            success: function (result) {
//                // Handle the search results
//                // You can update the UI or redirect to a different page
//                console.log(result);
//                location = "/Groups/Index";
//            },
//            error: function (xhr, status, error) {
//                // Handle the error if any
//                console.log(error);
//            }
//        });
//    });
//});
//}
function initTinyMce(selector) {
    tinymce.init({
        selector: selector,
        min_height: 300,
        max_height: 1000,
        skin: 'oxide',
        toolbar_location: 'bottom',
        plugins: 'lists code table codesample link autoresize',
        toolbar: 'blocks | bold italic underline strikethrough bullist link codesample',
        resize: true,
        menubar: false,
        statusbar: false,
        submit_patch: true,
        content_css: '/css/tinymce.css'
    });
}

$(() => {
    var notiContainer = $("#notifination-container");

    if (notiContainer != null) {
        $.ajax({
            url: "/Notification/0/10?handler=Header",
            method: "GET",
            success: (result) => {
                notiContainer.html(result);
            }
        })
    }
});