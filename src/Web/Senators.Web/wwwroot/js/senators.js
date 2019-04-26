"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/senatorsHub").build();

connection.on("DisplayViewNotification", function () {
    alert('Someone is viewing senators');
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