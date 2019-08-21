function StartUp(requestUrl, employeeUrl) {
    $(document).ready(function () {
        $("#pageOffset").val($(window).height() / 100);
        GetRequests(requestUrl);
        FindCurrentEmployee(employeeUrl);
    });
}

function GetRequests(requestUrl) {
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: requestUrl,
            dataType: 'html',
            data: { pageOffset: parseInt($('#pageOffset').val()), pageCount: parseInt($('#pageCount').val()) },
            success: function (partialViewData) {
                $('#accordion').html(partialViewData);
            }

        });
        var count = parseInt($('#pageCount').val());
        $("#pageCount").val(count + 1)
    });
}
function FindCurrentEmployee(employeeUrl) {
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: employeeUrl,
            dataType: 'html',
            success: function (partialViewData) {
                $('#currentUser').html(partialViewData)
            }
        })
    });
}

function GetRequestDetails(collapse, requestUrl) {
    console.log(collapse)
    $(collapse).ready(function () {
        $.ajax({
            type: 'GET',
            url: requestUrl,
            dataTyp: 'html',
            data: {requestUID : collapse.value},
            success: function (partialViewData) {
                $('.card-body').html(partialViewData)
            }
        });
    });
}