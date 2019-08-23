function StartUp(requestUrl, employeeUrl) {
    $(document).ready(function () {
        $("#pageOffset").val($(window).height() / 100);
        InfiniteRegister();
        SetButtons();
        GetRequests();
        FindCurrentEmployee();
    });
}

function InfiniteRegister() {
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
            $('.btn-primary').each(function (index, element) {
                if (element.value == 1) {
                    element.click();
                }

            })
        }
    });
}

function GetRequests() {
    SetButtonInFocus(this);
    
    requestUrl = $('#requestUrl').val();
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: requestUrl,
            dataType: 'html',
            data: { pageOffset: parseInt($('#pageOffset').val()), pageCount: parseInt($('#pageCount').val()) },
            success: function (partialViewData) {
                $('#accordion').append(partialViewData);
            }

        });
    });
    var count = parseInt($('#pageCount').val());
    $("#pageCount").val(count + 1)
}
function FindCurrentEmployee() {
    employeeUrl = $('#currentEmployeeUrl').val();
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
    $('#approve-' + id).on('click', () => ShowApproveModal($('#approve-' + id).val()));
    $('#deny-' + id).on('click', () => ShowDenyModal($('#deny-' + id).val()));
    $('#edit-' + id).on('click', () => ShowEditComponents($('#edit-' + id).val()));
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
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: url,
        data: ({ "__RequestVerificationToken": token, "RequestUID": $('#yes').val(), "RequestStatus": 'Accepted' }),
        success: function () {
            $('#item-' + $('#yes').val()).remove();
        }
    })
}
function DenyRequest(url) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: url,
        data: ({ "__RequestVerificationToken": token, "RequestUID": $('#yes').val(), "RequestStatus": 'Denied' }),
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
    $('#accordion').hide()
    ShowRequestForm(url);
}
function ShowRequestForm(url) {
    $.ajax({
        type: 'GET',
        url: url,
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
function asyncFormSubmit() {
    $(document).ready(function () {
        $('#requestForm').on('submit', function (e) {
            e.preventDefault();
            $.ajax({
                url: $(this).attr('action') || windows.location.pathname,
                type: 'POST',
                data: $(this).serialize(),
                success: function (data) {
                    $('#request').hmtl(data);
                }
            })
        })
    });
}
function SetButtons() {
    $(document).ready(function () {
        $('#requestShow').on('mousedown ', () => {
            console.log('promenjeno');
            $('#pageCount').val(1)
            $('#accordion').html(" ");
        });
        $('#requestShow').on('click', GetRequests);
        $('#requestShow').val(1);

        $('#employeesShow').on('mousedown', () => {
            console.log('promenjeno');
            $('#pageCount').val(1);
            $('#accordion').html(" ");
        });
        $('#employeesShow').on('click', GetEmployees);
        $('#employeesShow').val(0);
    });
}

function GetEmployees() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    SetButtonInFocus(this);
    $('#requestForm').remove();
    $.ajax({
        type: 'POST',
        url: 'Employee/GetEmployees',
        data: { "__RequestVerificationToken": token, pageOffset: parseInt($('#pageOffset').val()), pageCount: parseInt($('#pageCount').val()) },
        dataType: 'html',
        success: function (PartialViewData) {
            $('#accordion').append(PartialViewData);
        }
    })
    var count = parseInt($('#pageCount').val());
    $("#pageCount").val(count + 1)
}
function SetButtonInFocus(button) {
    $('.btn-primary').each(function (index, element) {
        element.value = 0;
    });
    $(button).val(1);
}

function GetEmployeeDetails(collapse, requestDetailsUrl) {
    $(collapse).ready(function () {
        var token = $('input[name="__RequestVerificationToken"').val();
        $.ajax({
            type: 'POST',
            url: requestDetailsUrl,
            data: { "__RequestVerificationToken": token, EmployeeUID: collapse.value },
            dataType: 'html',
            success: function (PartialViewData) {
                $('#card-' + collapse.value).html(PartialViewData)
                ButtonAddValue(collapse)
            }
        });
    });
}