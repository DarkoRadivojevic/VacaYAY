function BindActionToRequestForm(url){
    $('#showRequestForm').on("click", action);
    function action() {
        RequestForm(url);
    }
}

function RequestForm(url) {
    $('#accordion').hide()
    $('.btn-primary').each(function (index, element) {
        if (element.value == 1) {
            $('#previousButton').val(element.id);
        }
        element.value = 0;
    })
    ShowRequestForm(url);
}

function ShowRequestForm(url) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    var uid = $('#showRequestForm').val();
    $.ajax({
        type: 'POST',
        url: url,
        datatype: 'html',
        data: { "__RequestVerificationToken": token, "EmployeeUID": uid },
        success: function (partialViewData) {
            $('#request').html(partialViewData);
        }
    });
}

function SubmitRequest() {
    $('#requestForm').on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action') || windows.location.pathname,
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                $('#request').html(data);
                setTimeout(function () {
                    BackButton();
                }, 1500)
            }
        });
    });
}

function BackButton() {
        RestoreMain();
        ClearRequest();
}

function ClearRequest() {
    $('#requestSucces').html(' ')
}