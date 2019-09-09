function UserStartUp() {
    GetCurrentEmployee();
    GetPendingRequests();
    GetUserContracts();
}

function GetPendingRequests() {
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        type: 'POST',
        url: 'Request/GetPendingRequests',
        data: { "__RequestVerificationToken": token },
        success: function (data) {
            $('#accordion').html(data);
        }
    });
}

function GetUserContracts() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Contract/GetUserContracts',
        data: { "__RequestVerificationToken": token },
        success: function (data) {
            $('#contractAccordion').html(data);
        }
    });
}