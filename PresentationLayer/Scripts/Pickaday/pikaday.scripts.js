function InitializeAddRequestPikaday() {
    var startDate,
        endDate,
        updateStartDate = function () {
            startPicker.setStartRange(startDate);
            endPicker.setStartRange(startDate);
            endPicker.setMinDate(startDate);
        },
        updateEndDate = function () {
            startPicker.setEndRange(endDate);
            startPicker.setMaxDate(endDate);
            endPicker.setEndRange(endDate);
        },
        startPicker = new Pikaday({
            field: document.getElementById('RequestStartDate'),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                startDate = this.getDate();
                updateStartDate();
            },
        }),
        endPicker = new Pikaday({
            field: document.getElementById('RequestEndDate'),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                endDate = this.getDate();
                updateEndDate();
            }
        }),
        _startDate = startPicker.getDate(),
        _endDate = endPicker.getDate();
    if (_startDate) {
        startDate = _startDate;
        updateStartDate();
    }
    if (_endDate) {
        endDate = _endDate;
        updateEndDate();
    }
}

function InitializeEditEmployeePikady() {
    statPicker = new Pikaday({
        field: document.getElementById('EmployeeEmploymentDate'),
        onSelect: function () {
            endDate = this.getDate();
        }
    });
}

function InitializeAddContractPikady(id) {
    var startDate,
        endDate,
        updateStartDate = function () {
            startPicker.setStartRange(startDate);
            endPicker.setStartRange(startDate);
            endPicker.setMinDate(startDate);
        },
        updateEndDate = function () {
            startPicker.setEndRange(endDate);
            startPicker.setMaxDate(endDate);
            endPicker.setEndRange(endDate);
        },
        startPicker = new Pikaday({
            field: document.getElementById('contractStartDate-' + id),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                startDate = this.getDate();
                updateStartDate();
            },
        }),
        endPicker = new Pikaday({
            field: document.getElementById('contractEndDate-' + id),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                endDate = this.getDate();
                updateEndDate();
            }
        }),
        _startDate = startPicker.getDate(),
        _endDate = endPicker.getDate();
    if (_startDate) {
        startDate = _startDate;
        updateStartDate();
    }
    if (_endDate) {
        endDate = _endDate;
        updateEndDate();
    }
}

function InitializeEditRequestPikady(id) {
    var startDate,
        endDate,
        updateStartDate = function() {
            startPicker.setStartRange(startDate);
            endPicker.setStartRange(startDate);
            endPicker.setMinDate(startDate);
        },
        updateEndDate = function() {
            startPicker.setEndRange(endDate);
            startPicker.setMaxDate(endDate);
            endPicker.setEndRange(endDate);
        },
        startPicker = new Pikaday({
            field: document.getElementById('requestStart-' + id),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function() {
                startDate = this.getDate();
                updateStartDate();
            },
        }),
        endPicker = new Pikaday({
            field: document.getElementById('requestEnd-' + id),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function() {
                endDate = this.getDate();
                updateEndDate();
            }
        }),
        _startDate = startPicker.getDate(),
        _endDate = endPicker.getDate();
    if (_startDate) {
        startDate = _startDate;
        updateStartDate();
    }
    if (_endDate) {
        endDate = _endDate;
        updateEndDate();
    }
}

function InitializeSearchPikaday() {
    RequestSearchPikaday();
    EmployeeSearchPikaday();
    ContractSearchPikaday();
}

function RequestSearchPikaday() {
    var startDate,
        endDate,
        updateStartDate = function () {
            startPicker.setStartRange(startDate);
            endPicker.setStartRange(startDate);
            endPicker.setMinDate(startDate);
        },
        updateEndDate = function () {
            startPicker.setEndRange(endDate);
            startPicker.setMaxDate(endDate);
            endPicker.setEndRange(endDate);
        },
        startPicker = new Pikaday({
            field: document.getElementById('requestStartSearchField'),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                startDate = this.getDate();
                updateStartDate();
            },
        }),
        endPicker = new Pikaday({
            field: document.getElementById('requestEndSearchField'),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                endDate = this.getDate();
                updateEndDate();
            }
        }),
        _startDate = startPicker.getDate(),
        _endDate = endPicker.getDate();
    if (_startDate) {
        startDate = _startDate;
        updateStartDate();
    }
    if (_endDate) {
        endDate = _endDate;
        updateEndDate();
    }
}

function EmployeeSearchPikaday() {
    statPicker = new Pikaday({
        field: document.getElementById('employeeEmploymentSearchField'),
        onSelect: function () {
            endDate = this.getDate();
        }
    });
}

function ContractSearchPikaday() {
    var startDate,
        endDate,
        updateStartDate = function () {
            startPicker.setStartRange(startDate);
            endPicker.setStartRange(startDate);
            endPicker.setMinDate(startDate);
        },
        updateEndDate = function () {
            startPicker.setEndRange(endDate);
            startPicker.setMaxDate(endDate);
            endPicker.setEndRange(endDate);
        },
        startPicker = new Pikaday({
            field: document.getElementById('contractStartSearchField'),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                startDate = this.getDate();
                updateStartDate();
            },
        }),
        endPicker = new Pikaday({
            field: document.getElementById('contractEndSearchField'),
            minDate: new Date(),
            maxDate: new Date(2020, 12, 31),
            onSelect: function () {
                endDate = this.getDate();
                updateEndDate();
            }
        }),
        _startDate = startPicker.getDate(),
        _endDate = endPicker.getDate();
    if (_startDate) {
        startDate = _startDate;
        updateStartDate();
    }
    if (_endDate) {
        endDate = _endDate;
        updateEndDate();
    }
}