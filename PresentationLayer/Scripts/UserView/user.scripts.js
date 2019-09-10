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
            CollorStatus();
        },
        error: errorNotification
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
        },
        error: errorNotification
    });
}

function CancelRequest(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Request/CancelRequest',
        data: { "__RequestVerificationToken": token, "RequestUID": id },
        success: function (data) {
            successNotification();
        },
        error: errorNotification()
    });

}

function DownloadRequest(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: 'POST',
            url: 'Request/GetRequestFile',
            data: { "__RequestVerificationToken": token, "requestUID": id },
            success: function (file) {
                var arr = new Uint8Array(file['RequestFile']);

                var blob = new Blob([arr], { type: 'application/pdf' })

                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = file.RequestFileName;
                link.click();

                successNotification();
            },
            error: function () {
                errorNotification()
            }
        });
}

function CollorStatus() {
    $('.btn-link').each(function (index, element) {
        if ($(element).attr('data') == 'req') {
            $(element).addClass('border-left');
            $(element).addClass('border-3')
            CollorBorder(element, $(element).attr('data-color'));
        }
    });
}

function CollorBorder(element, status) {
    if (status == "Adjusted")
        $(element).addClass('border-info');
    if (status == "Rejected")
        $(element).addClass('border-danger');
    if (status == "Accepted")
        $(element).addClass('border-success')
    if (status == "InReview")
        $(element).addClass('border-warning')
}