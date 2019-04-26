"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/senatorsHub").build();

connection.on("DisplayViewNotification", function () {
    // Display an info toast with no title
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.warning('Someone else is viewing senators');
});

connection.start()
    .then(
        function () {
            connection.invoke("NotifyOnSenatorPage")
                .catch(
                    function (err) {
                        return console.error(err.toString());
                    }
                );
        }
    )
    .catch(function (err) {
        return console.error(err.toString());
    }
);