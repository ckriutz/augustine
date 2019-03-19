// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// This function gets the senators from the webpai microservice.
async function getAllSenators() {
    console.log("getting all senators...");
    var senators = [];
    let getSenators = new Promise((resolve, reject) => {
        $.ajax({
            async: true,
            type: "GET",
            url: "http://52.188.220.229/api/Senators",
            contentType: "application/json; charset=utf-8"
        }).done(function (data) {
            senators = data;
            resolve(senators);
        }).fail(function (jqXHR, textStatus) {
            console.log(textStatus);
            resolve(senators);
        });
    });

    let result = await getSenators;
    return result;
}

async function addSenator(senator) {
    console.log("adding: " + JSON.stringify(senator));
    let addSen = new Promise((resolve, reject) => {
        $.ajax({
            aysnc: true,
            type: "POST",
            url: "http://52.188.220.229/api/Senators",
            data: JSON.stringify(senator),
            contentType: "application/json; charset=utf-8"
        }).done(function () {
            console.log("success!");
        }).fail(function () { console.log("fail."); });
    });
}

// This is for the Main Index page of Augustine.
var indexApp = new Vue({
    el: '#index',
    data: {
        senators: ''
    },
    methods: {
        init: async function () {
            var allSenators = await getAllSenators();
            this.senators = allSenators;
        }
    },
    mounted() {
        this.init();
    }
});

// This is for the main Senator app at /Senators
var senatorApp = new Vue({
    el: '#senatorMain',
    data: {
        senators: '',
        SenatorToEdit: '',
        SenatorToAdd:
        {
            Name: '',
            District: '',
            Party: '',
            PhoneNumber: '',
            EmailAddress: ''
        }
    },
    methods: {
        init: async function () {
            var allSenators = await getAllSenators();
            this.senators = allSenators;
        },
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
        doAdd: async function () {
            // This function will add the senator to the service.
            await addSenator(this.SenatorToAdd);

            // Now, lets update the list from the database.
            await this.init();
        }
    },
    mounted() {
        this.init();
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







