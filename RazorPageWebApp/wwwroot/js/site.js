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