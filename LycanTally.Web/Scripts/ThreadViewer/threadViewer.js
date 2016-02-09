$(document).ready(function () {
    $("#dayFilter").slider({ 
        min: 0, 
        max: totalDays, 
        range: true, 
        value: [1, totalDays],
        formatter: function(value){
            return "Only show day " + value[0] + " to day " + value[1];
        }
    });

    $('#applyFilter').click(function(){
        var threadID = $('#ThreadID').val();
        var userFilter = $('#userFilter').val().join(",");
        var dayFilter = $('#dayFilter').val();

        threadViewer_OnFilter(threadID, userFilter, dayFilter);
    });
});

function threadViewer_OnFilter(threadID, userFilter, dayFilter) {
    $('#articleContainer').fadeOut("fast", function () {
        $('#articleContainer').load(getArticlesURL, { threadID: threadID, userFilter: userFilter, dayFilter: dayFilter }, function () {
            $('#articleContainer').fadeIn("slow");
        });
    });
}