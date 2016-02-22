$(document).ready(function () {

    // initialize the connection to the server
    var progressNotifier = $.connection.progressHub;

    // client-side sendMessage function that will be called from the server-side
    progressNotifier.client.sendMessage = function (message, count) {
        // update progress
        UpdateProgress(message, count);
    };

    // establish the connection to the server and start server-side operation
    $.connection.hub.start().done(function () {
        // call the method CallLongOperation defined in the Hub
        progressNotifier.server.updateThreadFromApi($.connection.hub.id, threadID);
    });
});

function UpdateProgress(message, count) {
    //var currentValue = parseInt($('#progressBar').attr('aria-valuenow'));
    $('#progressBar').css('width', count + '%').attr('aria-valuenow', count);
    $('#progressBar').html('<span>' + message + '</span>');

    if (count == 100)
        threadViewer_OnLoadComplete(threadID);
}

function threadViewer_OnLoadComplete(threadID) {
    $('#threadContainer').load(getThreadURL, { threadID: threadID }, function () {
        $('.progress').hide();
        $('#threadContainer').show();
    });
}