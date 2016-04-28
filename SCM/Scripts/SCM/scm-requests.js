function callRequestTab(e, url, listId) {
    $(this).tab('show');
    collectFilter();
}

function callRequests(page, status) {
    
    collectFilter(page);
}


function updateTotals() {
    var allCount = $("#totalAll").val();
    var activeCount = $("#totalActive").val();
    var pendingCount = $("#totalPending").val();
    var delayedCount = $("#totalDelayed").val();
    var closedCount = $("#totalClosed").val();
    $("#badgeAll").text(allCount);
    $("#badgeActive").text(activeCount);
    $("#badgeDelayed").text(delayedCount);
    $("#badgePending").text(pendingCount);
    $("#badgeClosed").text(closedCount);
}