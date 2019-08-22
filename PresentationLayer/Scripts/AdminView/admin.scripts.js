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
    $(collapse).ready(function () {
        $.ajax({
            type: 'GET',
            url: requestUrl,
            dataType: 'html',
            data: { requestUID: collapse.value },
            success: function (partialViewData) {
                $('#card-' + collapse.value).html(partialViewData)
                ButtonAddValue(collapse)
            }
        });
    });
    collapse.onclick = null;
}
function ButtonAddValue(collapse) {
    $('#approve-' + collapse.value).val(collapse.value);
    $('#deny-' + collapse.value).val(collapse.value);
    $('#edit-' + collapse.value).val(collapse.value);
    ButtonsAddAction(collapse.value);
}
function ButtonsAddAction(id) {
    console.log('id neki')
    console.log($('#approve-' + id).val());
    $('#approve-' + id).on('click', () => ShowApproveModal($('#approve-' + id).val()));
    $('#deny-' + id).on('click', () => ShowDenyModal($('#deny').val()));
    $('#edit-' + id).on('click', () => ShowEditComponents($('#edit').val()));
}
function ShowApproveModal(value) {
    $.ajax({
        type: 'GET',
        url: 'Request/ApproveModal',
        dataType: 'html',
        success: function (partialViewData) {
            $('#myModal').html(partialViewData);
            $('#myModal').modal('show');
            $('#yes').val(value);
        }
    });
}
function ApproveRequest(url) {
    console.log('pozvan sam');
    console.log($('#yes').val());
    var token = $('input[name="__RequestVerificationToken"]').val();
    console.log(token);
    $.ajax({
        type: 'POST',
        url: url,
        data: ({ "__RequestVerificationToken": token, "RequestUID": $('#yes').val(), "RequestStatus": 'Accepted' }),
        success: function () {
            $('#item-' + $('#yes').val()).remove();
        }
    })
}
function ShowDenyModal(value) {
    $.ajax({
        type: 'GET',
        url: 'Request/DenyModal',
        dataType: 'html',
        success: function (partialViewData) {
            $('#myModal').html(partialViewData);
            $('#myModal').modal('show');
            $('#yes').val(value);
        }
    });
}
function ShowEditComponents(value) {



}
function RequestForm(url) {
    console.log('pozvan sma')
    $('#accordion').hide()
    ShowRequestForm(url);
}
function ShowRequestForm(url) {
    $.ajax({
        type: 'GET',
        url:  url,
        datatype: 'html',
        success: function (partialViewData) {
            $('#request').html(partialViewData);       
        }
    });
}
function restore() {
    $('#requestForm').remove();
    $('#accordion').show();
}