function callRequestTab(e, url, listId) {
    $(this).tab('show');
    collectFilter();
    //$.ajax(
    //    {
    //        type: 'GET',
    //        url: url,
    //        success: function (result) {
    //            $(listId).html(result);
    //            updateTotals();
    //        }
    //    }
    //);
}

function callRequests(page, status) {
    
    collectFilter(page);
    //var listId = '#' + status + 'List';
    //var url = '/ServiceRequests/Requests?page=' + page + '&status=' + status;

    //$.ajax(
    //{
    //    type: 'GET',
    //    url: url,
    //    success: function (result) {
    //        $(listId).html(result);
    //        updateTotals();
    //    }
    //}
    //);
}


function updateTotals() {

    var activeCount = $("#totalActive").val();
    var pendingCount = $("#totalPending").val();
    var delayedCount = $("#totalDelayed").val();
    $("#badgeActive").text(activeCount);
    $("#badgeDelayed").text(delayedCount);
    $("#badgePending").text(pendingCount);
}