//$('document').ready(function () {
//    $('#retrieveMetrics').click(function () {
//        $.ajax({
//            url: "https://www.boardgamegeek.com/xmlapi2/thread",
//            jsonp: 'callback',
//            dataType: 'jsonp',
//            data: {}
//        })



//        $.get(
//            "https://www.boardgamegeek.com/xmlapi2/thread",
//            { id: $('#threadID').val() },
//            function (data) {
//                console.log(data);
//            }
//        );
//    });
//});

function retriever_AfterRetrieve(data)
{
    alert('Successfully retrieved data.');
    console.log(data);
}