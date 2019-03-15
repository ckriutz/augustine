// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// Sample Model for a Senator
var senatorsList = [
    { "Id": 0, "Name": "Spongebob Squarepants", "District": 2, "Party": "Democrat", "PhoneNumber": "555-369-2101", "EmailAddress": "sbob@gmail.com" },
    { "Id": 1, "Name": "Patrick Star", "District": 11, "Party": "Democrat", "PhoneNumber": "555-326-9111", "EmailAddress": "pstar@hotmail.com" }
];

// This function will reach out to the service and get all the senators.
var getSenators = function () {
    //senatorsList = 
    return senatorsList;
};

var indexApp = new Vue({
    el: '#index',
    data: {
        senators: getSenators().length
    }
});


var senatorApp = new Vue({
    el: '#senatorMain',
    data: {
        message: 'Hello Vue!',
        senators: getSenators(),
        SenatorToEdit: '',
        SenatorToAdd: [
            {
                Id: '',
                Name: '',
                District: '',
                Party: '',
                PhoneNumber: '',
                EmailAddress: ''
            }
        ]
    },
    methods: {
        showDelete: function (id) {
            // So with this line, we take the button-click event from the page, and grab the Id that it passed in.
            // With that Id, we can insert the name, and set the button.

            // First we delete from the service, and if that works, we can just remove from the array also.

            // Now to remove from the array.
            $('#delete-modal-body').html("<p>Are you sure you want to delete " + this.senators[id].Name + "?");
            $('#deleteModal').data('id', id).modal();
        },
        doDelete: function (id) {
            this.senators.splice(this.senators.indexOf(this.senators[id]), 1);
        },
        showEdit: function (senator) {
            this.SenatorToEdit = senator;
            $('#editModal').modal();
        },
        doEdit: function () {
            //SenatorToEdit should be here already, just need to remove/replace
            this.senators.splice(this.senators.indexOf(this.SenatorToEdit), 1);
            this.senators.push(SenatorToEdit);
            this.SenatorToEdit = '';
        },
        showAdd: function () {
            $('#addModal').modal();
        },
        doAdd: function () {
            this.senators.push(this.SenatorToAdd);
            this.SenatorToAdd = '';
        }
    }
});

$('#btnDestroy').click(function () {
    // handle deletion here
    var id = $('#deleteModal').data('id');
    //$('[data-id=' + id + ']').remove();
    $('#deleteModal').modal('hide');
    senatorApp.doDelete(id);
});

$('#btnUpdate').click(function () {
    //Now we write back to the array.
    $('#editModal').modal('hide');
});

$('#btnSave').click(function () {
    //Now we write back to the array.
    senatorApp.doAdd();
    $('#addModal').modal('hide');
});