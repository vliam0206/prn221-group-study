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