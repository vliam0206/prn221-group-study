// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function keyValueArrayToObject(array) {
    var dataObject = {};
    for (var i = 0; i < array.length; i++) {
        var pair = array[i];
        var key = pair.name;
        var value = pair.value;
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