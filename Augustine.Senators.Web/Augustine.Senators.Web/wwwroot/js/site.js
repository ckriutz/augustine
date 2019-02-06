// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// Sample Model for a Senator
var senatorsList = [
    { "Id": 0, "Name": "Spongebob Squarepants", "District": 2, "Party": "Democrat", "PhoneNumber": "555-369-2101", "EmailAddress": "sbob@gmail.com" },
    { "Id": 1, "Name": "Patrick Star", "District": 11, "Party": "Democrat", "PhoneNumber": "555-326-9111", "EmailAddress": "pstar@hotmail.com" }
];

var getSenators = function () {
    //var sens = JSON.parse(senatorsList);
    return senatorsList;
};

var indexApp = new Vue({
    el: '#index',
    data: {
        message: 'Hello Vue!',
        senators: getSenators().length
    }
});

var senatorApp = new Vue({
    el: '#senatorMain',
    data: {
        message: 'Hello Vue!',
        senators: getSenators()
    }
})