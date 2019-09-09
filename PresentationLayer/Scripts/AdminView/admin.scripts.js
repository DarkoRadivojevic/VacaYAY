var locker = false;

function StartUp() {
    $(document).ready(function () {
        $("#pageOffset").val($(window).height() / 100);
        GetCurrentEmployee();
        InfiniteRegister();
        SetButtons();
        GetRequests();
        BindSearchFunctions();
        AddCollectiveLink();
    });
}

function InfiniteRegister() {
    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 150) {
            $('.btn-primary').each(function (index, element) {
                if (element.value == 1) {
                    element.click();
                }

            })
        }
    });
}

function GetCurrentEmployee() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $(document).ready(
        $.ajax({
            type: 'POST',
            url: "Employee/FindCurrentEmployee",
            data: { "__RequestVerificationToken": token },
            success: function (PartialViewData) {
                $('#navbarCollapse').append(PartialViewData);
            }
        }));
}

function BindSearchFunctions() {
    BindEmployeeSearch();
    BindRequestSearch();
    BindContractSearch();
}

function BindEmployeeSearch() {
    var url = $('#findByParametersEmployees').val();
    var token = $('input[name="__RequestVerificationToken"]').val();

    $('#employeeSearchField, #employeeEmploymentSearchField').on('keydown', function (ev) {
        if (ev.keyCode == 13) {
            var input = $('#employeeSearchField').val();
            var inputStart = $('#employeeEmploymentSearchField').val();
            if (input != "" || inputStart != "") {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { "__RequestVerificationToken": token, "SearchParameters": input, "EmployeeEmploymentDate": inputStart },
                    success: function (PartialViewData) {
                        $('#accordion').html(PartialViewData);
                        $('#employeesShow').val(0);
                    }
                });
            }
            else {
                $("#pageCount").val(1);
                $('#employeesShow').val(1);
                $('#accordion').html("");
                GetEmployees();
            }
        }
    });
}

function BindRequestSearch() {
    var url = $('#findByParametersRequests').val();
    var token = $('input[name="__RequestVerificationToken"]').val();

    $('#requestSearchField, #requestStartSearchField, #requestEndSearchField').on('keydown', function (ev) {
        if (ev.keyCode == 13) {
            var input = $('#requestSearchField').val();
            var inputStart = $('#requestStartSearchField').val();
            var inputEnd = $('#requestEndSearchField').val();
            if (input != "" || inputStart != "" || inputEnd != "") {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { "__RequestVerificationToken": token, "SearchParameters": input, "RequestStartDate": inputStart, "RequestEndDate": inputEnd },
                    success: function (PartialViewData) {
                        $('#accordion').html(PartialViewData);
                        $('#requestShow').val(0);
                    }
                });
            }
            else {
                $("#pageCount").val(1);
                $('#accordion').html("");
                $('#requestShow').val(1);
                GetRequests();
            }
        }
    });
}

function BindContractSearch() {
    var url = $('#findByParametersContracts').val();
    var token = $('input[name="__RequestVerificationToken"]').val();

    $('#contractSearchField, #contractStartSearchField, #contractEndSearchField').on('keydown', function (ev) {
        if (ev.keyCode == 13) {
            var input = $('#contractSearchField').val();
            var inputStart = $('#contractStartSearchField').val();
            var inputEnd = $('#contractEndSearchField').val();
            if (input != "" || inputStart != "" || inputEnd != "") {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { "__RequestVerificationToken": token, "SearchParameters": input, "ContractStartDate": inputStart, "ContactEndDate": inputEnd },
                    success: function (PartialViewData) {
                        $('#accordion').html(PartialViewData);
                        $('#contractsShow').val(0);
                    }
                });
            }
            else {
                $("#pageCount").val(1);
                $('#contractsShow').val(1);
                $('#accordion').html("");
                GetContracts();
            }
        }
    });
}

function BindEmployeeNameSearch() {
    var url = $('#findByNameEmployees').val();
    var token = $('input[name="__RequestVerificationToken"]').val();

    $('#employeeSearchField').on('keypress', function (ev) {
        if (ev.keyCode == 13) {
            var inputString = $('#employeeSearchField').val();
            if (inputString != "") {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { "__RequestVerificationToken": token, "FullName": inputString },
                    success: function (PartialViewData) {
                        $('#accordion').html(PartialViewData);
                    }
                });
            }
            else {
                $("#pageCount").val(1);
                $('#accordion').html("");
                GetEmployees();
            }
        }
    });
}

function BindEmployeeEmploymentSearch() {
    var url = $('#findByEmploymentDateEmployees').val();
    var token = $('input[name="__RequestVerificationToken"]').val();

    $('#employeeEmploymentSearchField').on('keydown', function (ev) {
        if (ev.keyCode == 13) {
            var input = $('#employeeEmploymentSearchField').val();
            if (input != "") {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { "__RequestVerificationToken": token, "EmployeeEmploymentDate": input },
                    success: function (PartialViewData) {
                        $('#accordion').html(PartialViewData);
                    }
                });
            }
            else {
                $('#pageCount').val(1);
                $("#accordion").html("");
                GetEmployees();
            }
        }
    });
}

function GetRequests() {
    SetButtonInFocus($('#requestShow'));
    $('#accordion').show();
    $('#requestForm').remove();
    var token = $('input[name="__RequestVerificationToken"]').val();
    requestUrl = $('#requestUrl').val();

    if (locker == false) {
        locker = true;
        $.ajax({
            type: 'POST',
            url: requestUrl,
            dataType: 'html',
            data: { pageOffset: parseInt($('#pageOffset').val()), pageCount: parseInt($('#pageCount').val()), "__RequestVerificationToken": token },
            success: function (partialViewData) {
                $('#accordion').append(partialViewData);
                var count = parseInt($('#pageCount').val());
                $("#pageCount").val(count + 1)
                locker = false;
            }
        });
    };
}

function GetRequestDetails(collapse, requestUrl) {
    $(collapse).ready(function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: 'POST',
            url: requestUrl,
            dataType: 'html',
            data: { requestUID: collapse.value, "__RequestVerificationToken": token },
            success: function (partialViewData) {
                $('#card-' + collapse.value).html(partialViewData)
                ButtonAddValue(collapse, false)
            }
        });
    });
    collapse.onclick = null;
}

function ButtonAddValue(collapse, employee) {
    if (!employee) {
        $('#approve-' + collapse.value).val(collapse.value);
        $('#deny-' + collapse.value).val(collapse.value);
        $('#edit-' + collapse.value).val(collapse.value);
        ButtonsAddAction(collapse.value, employee);
    }
    else {
        $('#editEmployee-' + collapse.value).val(collapse.value);
        $('#deleteEmployee-' + collapse.value).val(collapse.value);

        $('#addContract-' + collapse.value).val(collapse.value);
        $('#addAdditionalDays-' + collapse.value).val(collapse.value);

        ButtonsAddAction(collapse.value, employee);
    }
}

function ButtonsAddAction(id, employee) {
    if (!employee) {
        $('#approve-' + id).on('click', () => ShowRequestApproveModal(id));
        $('#deny-' + id).on('click', () => ShowRequestDenyModal(id));
        $('#edit-' + id).on('click', () => ShowRequestEditComponents(id));
    }
    else {
        $('#editEmployee-' + id).on('click', () => ShowEmployeeEditComponents(id));
        $('#deleteEmployee-' + id).on('click', () => ShowDeleteEmployeeModal(id));

        $('#addContract-' + id).on('click', () => ShowAddContractComponents(id));
        $('#addAdditionalDays-' + id).on('click', () => ShowAddAdditionalDaysComponents(id));
    }
}

function ShowAddContractComponents(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Contract/AddContractView',
        dataType: 'html',
        data: { "__RequestVerificationToken": token, EmployeeUID: id },
        success: function (PartialViewData) {
            $('#addContainer-' + id).html(PartialViewData);
        }
    });
}

function ShowAddAdditionalDaysComponents(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'AdditionalDays/AddAdditionalDaysView',
        dataType: 'html',
        data: { "__RequestVerificationToken": token, EmployeeUID: id },
        success: function (PartialViewData) {
            $('#addContainer-' + id).html(PartialViewData);
            AddEmployeeUID(id);
            $('#addContainer-' + id + ' #additionalDaysCancel').val(id);
        }
    });
}

function AddEmployeeUID(id) {
    $('#addContainer-' + id + ' #EmployeeFormUID').val(id);
}

function RemoveAdditionalDaysForm(button) {
    var id = button.value;
    $('#additionalDaysForm-' + id).remove();
}

function ShowRequestApproveModal(value) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Request/ApproveModal',
        dataType: 'html',
        data: { "__RequestVerificationToken": token },
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
    var id = $('#yes').val();
    console.log('#denial-' + id)
    var comment = $('#denial-' + id).val();
    console.log(comment);

    $.ajax({
        type: 'POST',
        url: url,
        data: ({ "__RequestVerificationToken": token, "RequestUID": $('#yes').val(), "RequestStatus": "Rejected", "RequestComment": comment }),
        success: function () {
            $('#item-' + $('#yes').val()).remove();
        }
    })
}

function ShowRequestDenyModal(value) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Request/DenyModal',
        dataType: 'html',
        data: { "__RequestVerificationToken": token, RequestUID: value },
        success: function (partialViewData) {
            $('#myModal').html(partialViewData);
            $('#myModal').modal('show');
            $('#yes').val(value);
        }
    });
}

function ShowRequestEditComponents(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Request/EditRequestView',
        dataType: 'html',
        data: { "__RequestVerificationToken": token, RequestUID: id },
        success: function (PartialViewData) {
            $('#editRequest-' + id).html(PartialViewData);

            $('#editRequest-' + id + ' #requestCancel').val(id);
        }
    });
}


function SubmitEditRequest(id) {
    $('#editRequestForm-' + id).on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action') || windows.location.pathname,
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                if (data['Error'] == true) {
                    $('#editRequest-' + id).html(data.View);
                }
                else {
                    $('#editRequest-' + id).html(data);
                    setTimeout(function () {
                        $('#item-' + id).remove();
                    }, 2000);
                }
            }
        });
    });
}


function RestoreMain(form) {
    if ($('#accordion').attr('data') == 1) {
        var isChanged = true;
        $('.btn-primary').each(function (index, element) {
            if (element.value == 1)
                isChanged = false;
        });
        if (isChanged) {
            $('#' + form).remove();
            $('#accordion').show();
            $('.btn-primary').val(1);
        }
    }
    else {
        $('#' + form).remove();
        $('#userRequests').show();
        $('#userContracts').show();
    }
}

function SetButtons() {
    $(document).ready(function () {
        $('#requestShow').on('mousedown', () => {
            $('#pageCount').val(1)
            $('#accordion').html(" ");
        });
        $('#requestShow').on('click', function () {
            GetRequests();
            SetRequestSearchFields();
        });
        $('#requestShow').val(1);

        $('#employeesShow').on('mousedown', () => {
            $('#pageCount').val(1);
            $('#accordion').html(" ");
        });
        $('#employeesShow').on('click', function () {
            GetEmployees()
            SetEmployeeSearchFileds();
        });
        $('#employeesShow').val(0);

        $('#contractsShow').on('mousedown', () => {
            $('#pageCount').val(1)
            $('#accordion').html(" ");
        });
        $('#contractsShow').on('click', function () {
            GetContracts();
            SetContractSearchFileds();
        });
        $('#contractsShow').val(0);
    });
}

function SetContractSearchFileds() {
    $('#employeeSearch').attr('hidden', true);
    $('#contractSearch').attr('hidden', false);
    $('#requestSearch').attr('hidden', true);
}

function SetEmployeeSearchFileds() {
    $('#employeeSearch').attr('hidden', false);
    $('#contractSearch').attr('hidden', true);
    $('#requestSearch').attr('hidden', true);
}

function SetRequestSearchFields() {
    $('#employeeSearch').attr('hidden', true);
    $('#contractSearch').attr('hidden', true);
    $('#requestSearch').attr('hidden', false);
}

function GetEmployees() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    SetButtonInFocus($('#employeesShow'));
    $('#requestForm').remove();
    $('#accordion').show();

    if (locker == false) {
        locker = true;
        $.ajax({
            type: 'POST',
            url: 'Employee/GetEmployees',
            data: { "__RequestVerificationToken": token, pageOffset: parseInt($('#pageOffset').val()), pageCount: parseInt($('#pageCount').val()) },
            dataType: 'html',
            success: function (PartialViewData) {
                $('#accordion').append(PartialViewData);
                var count = parseInt($('#pageCount').val());
                $("#pageCount").val(count + 1)
                locker = false;
            }
        });
    };
}

function GetContracts() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    SetButtonInFocus($('#contractsShow'));
    $('#requestForm').remove();
    $('#accordion').show();

    if (locker == false) {
        locker = true;
        $.ajax({
            type: 'POST',
            url: 'Contract/GetContracts',
            data: { "__RequestVerificationToken": token, pageOffset: parseInt($('#pageOffset').val()), pageCount: parseInt($('#pageCount').val()) },
            dataType: 'html',
            success: function (PartialViewData) {
                $('#accordion').append(PartialViewData);
                var count = parseInt($('#pageCount').val());
                $("#pageCount").val(count + 1)
                locker = false;
            }
        });
    }

}

function GetContractDetails(collapse, requestDetailsUrl) {
    $(collapse).ready(function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: 'POST',
            url: requestDetailsUrl,
            data: { "__RequestVerificationToken": token, ContractUID: collapse.value },
            datatype: 'html',
            success: function (PartialViewData) {
                $('#card-' + collapse.value).html(PartialViewData);
                ButtonAddDownload(collapse.value);
            }
        });
    });
}

function ButtonAddDownload(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $('#donwloadContract-' + id).on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'Contract/GetContractFile',
            data: { "__RequestVerificationToken": token, ContractUID: id },
            success: function (file) {

                var arr = String.fromCharCode.apply(String, file['ContractFile']);

                var blob = new Blob([arr]);

                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = file.ContractFileName;
                link.click();

            },
            error: function (xhr, textStatus, error) {
                console.log(xhr.statusText);
                console.log(textStatus);
                console.log(error);
            }
        });
    });
}

function SetButtonInFocus(button) {
    $('.btn-primary').each(function (index, element) {
        element.value = 0;
    });
    $(button).val(1);
}

function GetEmployeeDetails(collapse, requestDetailsUrl) {
    $(collapse).ready(function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: 'POST',
            url: requestDetailsUrl,
            data: { "__RequestVerificationToken": token, EmployeeUID: collapse.value },
            dataType: 'html',
            success: function (PartialViewData) {
                $('#card-' + collapse.value).html(PartialViewData)
                ButtonAddValue(collapse, true)
                AddColoumData(collapse.value);
            }
        });
        collapse.onclick = null;
    });
}

function AddColoumData(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: 'Contract/GetEmployeeContracts',
        data: { "__RequestVerificationToken": token, EmployeeUID: id },
        dataType: 'html',
        success: function (PartialViewData) {

            var ifEmpty = PartialViewData.includes('div');
            if (ifEmpty) {
                $('.dynamic-size').height(250);
            }
            $('#contracts-' + id).html(PartialViewData);

        }
    });
    $.ajax({
        type: 'POST',
        url: 'Request/GetEmployeeRequests',
        data: { "__RequestVerificationToken": token, EmployeeUID: id },
        dataType: 'html',
        success: function (PartialViewData) {
            var ifEmpty = PartialViewData.includes('div');
            if (ifEmpty) {
                $('.dynamic-size').height(250);
            }
            $('#vacations-' + id).html(PartialViewData);
        }
    });
}

function ShowEmployeeEditComponents(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: "Employee/EditEmployeeView",
        data: { "__RequestVerificationToken": token, EmployeeUID: id },
        datatype: 'html',
        success: function (PartialViewData) {
            $('#editEmployeeDiv-' + id).html(PartialViewData);
            BindEmployeeUID(id);
        }
    });
}

function SubmitEditEmployee(id) {
    $('#employeeForm-' + id).on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action') || windows.location.pathname,
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                if (data['Error'] == true)
                    $('#editEmployeeDiv-' + id).html(data['View']);
                else {
                    $('#btnLink-' + id).html(data);
                    collapse = $('#card-' + id);
                    collapse.value = id;
                    url = $('#getEmployeeDetails').val();
                    GetEmployeeDetails(collapse, url);
                }



            }
        });
    });
}

function SubmitAddContract(id) {
    $('#addContractForm-' + id).on('submit', function (e) {
        e.preventDefault();
        var form = document.getElementById('addContractForm-' + id);
        var formData = new FormData(form);
        $.ajax({
            url: $(this).attr('action') || windows.location.pathname,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                $('#addContainer-' + id).html(data);
            }
        });
    });
}

function SubmitAdditionalDay(id) {
    $('#additionalDaysForm-' + id).on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action') || windows.location.pathname,
            datatype: 'multipart/form-data',
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                $('#addContainer-' + id).html(data);
                setTimeout(function () {
                    $('#addContainer-' + id).html(" ");
                }, 1500)
            }
        });
    });
}

function ShowDeleteEmployeeModal(id) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: "Employee/DeleteEmployeeModal",
        data: { "__RequestVerificationToken": token, EmployeeUID: id },
        datatype: 'html',
        success: function (PartialViewData) {
            $('#myModal').html(PartialViewData);
            $('#myModal').modal('show');
            $('#yes').val(id);
        }
    });
}

function DeleteEmployee(url) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    var uid = $('#yes').val();
    $.ajax({
        type: 'POST',
        url: url,
        data: { "__RequestVerificationToken": token, EmployeeUID: uid },
        datatype: 'html',
        success: function (PartialViewData) {
            $('#item-' + uid).remove();
        }
    });
}

function RestoreEmployeeCard(id) {
    $('#employeeForm-' + id).remove();
}

function RestoreRequestCard(id) {
    $('#editRequestForm-' + id).remove()
}

function BindEmployeeUID(id) {
    $(document).ready(function () {
        $('#EmployeeFormUID').val(id)
    });
}

function BindInnerButtonsForRequests(url) {
    $(document).ready(function () {
        $('.btn-link').each(function (i, element) {
            element.onclick = (() => GetRequestDetails(element, url));
        });
    });
}

function BindInnerButtonsForEmployees(url) {
    $(document).ready(function () {
        $('.btn-link').each(function (i, element) {
            element.onclick = (() => GetEmployeeDetails(element, url))
        });
    });
}

function BindInnerButtonsForContracts(url) {
    $(document).ready(function () {
        $('.btn-link').each(function (i, element) {
            if($(element).attr('data') == "con")
                element.onclick = (() => GetContractDetails(element, url))
        });
    })
}

function RemoveAddContractForm(button) {
    var id = button.value;
    $('#addContractForm-' + id).remove();
}

function UpdateFileName() {
    $('input[type="file"]').change(function (e) {
        var fileName = e.target.files[0].name;
        $('.file-path').attr('placeholder', fileName);
    });
}

function AddCollectiveLink() {
    $('#collective').on('click', GetCollectiveForm);
}

function GetCollectiveForm() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    HideAccordion();
    $.ajax({
        type: 'POST',
        url: 'Request/CollectiveView',
        data: { "__RequestVerificationToken": token },
        datatype: 'html',
        success: function (PartialViewData) {
            $('#request').html(PartialViewData);
        }
    });
}

function HideAccordion() {
    $('#accordion').hide();
    $('.btn-primary').each(function (index, element) {
        if (element.value == 1) {
            $('#previousButton').val(element.id);
        }
        element.value = 0;
    })
}

function SubmitCollective() {
    $('#collectiveForm').on('submit', function (e) {
        e.preventDefault();
        $.ajax({
            url: $(this).attr('action') || windows.location.pathname,
            type: 'POST',
            data: $(this).serialize(),
            success: function (data) {
                $('#request').html(data);
            }
        });
    });
}
