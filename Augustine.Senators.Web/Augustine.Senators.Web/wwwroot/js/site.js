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
    },
    methods: {
        doDelete: function (id) {
            // So with this line, we take the button-click event from the page, and grab the Id that it passed in.
            // With that Id, we can insert the name, and set the button.
            $('#delete-modal-body').html("<p>Are you sure you want to delete " + this.senators[id].Name + "?");
            $('#deleteModal').modal();
        }
    }
})