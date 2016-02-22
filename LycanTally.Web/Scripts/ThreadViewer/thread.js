$(document).ready(function () {
    $('#userFilter').selectpicker({});

    $("#dayFilter").slider({
        min: 0,
        max: totalDays,
        range: true,
        value: [1, totalDays],
        formatter: function (value) {
            return "Only show day " + value[0] + " to day " + value[1];
        }
    });

    $('#applyFilter').click(function () {
        var threadID = $('#Thread_ID').val();
        var dayFilter = $('#dayFilter').val();
        var userFilter = $('#userFilter').val() || "";

        if (userFilter.length > 0)
            userFilter = userFilter.join(",");

        thread_OnFilter(threadID, userFilter, dayFilter);
    });
});

function thread_OnFilter(threadID, userFilter, dayFilter) {
    $('#articleContainer').fadeOut("fast", function () {
        $('#refresh').addClass("fa-spin");
        $('#articleContainer').load(getArticlesURL, { threadID: threadID, userFilter: userFilter, dayFilter: dayFilter }, function () {
            $('#refresh').removeClass("fa-spin");
            $('#articleContainer').fadeIn("slow");
        });
    });
}