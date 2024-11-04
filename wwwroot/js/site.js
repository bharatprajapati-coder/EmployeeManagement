$(".dt").datepicker({
    format: "dd/mm/yyyy",
    autoclose: true,
    todayHighlight: true
});
$('#calendarIcon').on('click', function () {
    $('.dt').datepicker('show');
});


$(document).ready(function () {

    BindList();

});
function BindList() {
    var table = $('#EmployeeTbl').DataTable({
        "searching": true,
        "bLengthChange": true,
        "serverside": true,
        "destroy": true,
        "ajax": {
            "url": '/Employee/GetEmpList',
            "type": "POST",
            "dataType": "json",
            "data": function (d) {
                d.FirstName = $('#FirstName').val(),
                    d.LastName = $('#LastName').val(),
                    d.City = $('#City').val(),
                    d.StateId = $('#StateId').val(),
                    d.DepartmentId = $('#DepartmentId').val(),
                    d.JoiningDate = $('#JoiningDate').val(),
                    d.LeavingDate = $('#LeavingDate').val(),
                    d.HasLeft = $('#isEmployeeHasLeft').prop('checked')
            }
        },
        "columns": [

            { "data": "rowNo", "autoWidth": true },
            { "data": "firstName", "autoWidth": true },
            { "data": "lastName", "autoWidth": true },
            {
                "data": function (row) {
                    if (row.city == null || row.city == "") {
                        return '-';
                    }
                    else {
                        return row.city;
                    }
                },
            },
            {
                "data": function (row) {
                    if (row.stateName == null || row.stateName == "") {
                        return '-';
                    }
                    else {
                        return row.stateName;
                    }
                },
            },
            { "data": "joiningDate", "autoWidth": true },
            { "data": "departmentName", "autoWidth": true },
            {
                "data": function (row) {
                    if (row.salary == null || row.salary == "") {
                        return 0;
                    }
                    else {
                        return row.salary;
                    }
                },
            },
            {
                "data": function (row) {
                    if (row.leavingDate == null || row.leavingDate == "") {
                        return '-'
                    }
                    else {
                        return row.leavingDate
                    }
                },
            },
            {
                "data": function (row) {
                    return '<div class="btn-group">'
                        + '<a class="btn btn-primary" value= ' + row.id + ' onclick = "fnEditEmployee(' + row.id + ')"> Edit</a>'
                        + '<a class="btn btn-danger ms-2" value= ' + row.id + ' onclick ="DeleteEmployee(' + row.id + ')">Delete</a>'
                        + '</div>'
                }
            }

        ],
        "columnDefs": [
            { "vertical-align": "middle", "targets": "_all" },
            { "className": "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
            { "targets": [9], "orderable": false }
        ]
    });


    $('#advanceSearchButton').on('click', function () {
        debugger
        $('#EmployeeTbl').DataTable().ajax.reload();
    })

    $('#clearButton').on('click', function () {
        debugger
        $('#FirstName').val('');
        $('#LastName').val('');
        $('#City').val('');
        $('#StateId').val('').trigger('change');
        $('#DepartmentId').val('').trigger('change');
        $('#JoiningDate').val('');
        $('#LeavingDate').val('');
        $('#isEmployeeHasLeft').prop('checked', false);
        $('#EmployeeTbl').DataTable().ajax.reload();
    })
}

function fnEditEmployee(id) {
    debugger
    $.ajax({
        url: "/Employee/GetOneEmployee",
        type: "Get",
        data: {
            Id: id
        },
        dataType: "json",
        success: function (data) {
            $('#EmployeeModal').modal('show');
            $('#Id').val(id);
            $('#firstName').val(data.obj.firstName);
            $('#middleName').val(data.obj.middleName);
            $('#lastName').val(data.obj.lastName);
            $('#Address1').val(data.obj.address1);
            $('#Address2').val(data.obj.address2);
            $('#city').val(data.obj.city);
            $('#leavingDate').val(data.obj.leavingDate);
            $('#isEmployeeHasLeft').prop('checked', data.obj.hasLeft);

            $('#stateId').empty();
            data.stateList.forEach(Item => {
                const isSelected = Item.value == data.obj.stateId ? 'selected' : '';
                $('#stateId').append(`<option value="${Item.value}" ${isSelected}>${Item.text}</option>`);
            });

            $('#departmentId').empty();
            data.departmentList.forEach(Item => {
                const isSelected = Item.value == data.obj.departmentId ? 'selected' : '';
                $('#departmentId').append(`<option value="${Item.value}" ${isSelected}>${Item.text}</option>`)
            });
            checkValidation();

        },
        error: function (response) {
            alert("It seems to be wrong with this record!");
        }
    })
}


function DeleteEmployee(id) {
    debugger
    if (confirm("Are you sure you want to Delete this Record")) {
        $.ajax({
            url: '/Employee/DeleteEmployee',
            type: 'Delete',
            data: { Id: id },
            success: function (data) {
                alert(data.message);
                $('#EmployeeTbl').DataTable().ajax.reload();
            },
            error: function (response) {
                alert(data.message);
            }
        });
    }
}







function checkValidation() {
    debugger
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var DepartmentId = $('#departmentId').val();
    var isValid = true;
    if (firstName == '') {
        document.getElementById('firstNameValidation').style.display = 'block';
        isValid = false;
    }   
    else {
        document.getElementById('firstNameValidation').style.display = 'none';
    }

    if (lastName == '') {
        document.getElementById('lastNameValidation').style.display = 'block'
        isValid =  false;
    }
    else {
        document.getElementById('lastNameValidation').style.display = 'none';
    }

    if (DepartmentId == '') {
        document.getElementById('departmentIdValidation').style.display = 'block'
        isValid =  false;
    }
    else {
        document.getElementById('departmentIdValidation').style.display = 'none';
    }
    return isValid;


    //if (firstName == '' || lastName == '' || DepartmentId == '') {
    //    return false;
    //}
}


$('#updateBtn').off('click').on('click', function () {
    debugger
    if (checkValidation() === false) {
        return false;
    }

    var Id = $('#Id').val();
    var FirstName = $('#firstName').val();
    var MiddleName = $('#middleName').val();
    var LastName = $('#lastName').val();
    var Address1 = $('#Address1').val();
    var Address2 = $('#Address2').val();
    var City = $('#city').val();
    var stateId = $('#stateId').val();
    var DepartmentId = $('#departmentId').val();
    var LeavingDate = $('#leavingDate').val();
    var HasLeft = $('#isEmployeeHasLeft').is(':checked');

    var obj = {
        Id: parseInt(Id),
        FirstName: FirstName,
        MiddleName: MiddleName,
        LastName: LastName,
        Address1: Address1,
        Address2: Address2,
        City: City,
        StateId: parseInt(stateId),
        DepartmentId: parseInt(DepartmentId),
        LeavingDate: LeavingDate,
        hasLeft: HasLeft
    };


    $.ajax({
        url: "/Employee/UpdateEmployee",
        type: "POST",
        dataType: 'json',
        data: {
            Obj: JSON.stringify(obj)
        },
        success: function (data) {
            $('#EmployeeTbl').DataTable().ajax.reload(null, false);
            $('#EmployeeModal').modal('hide');
            alert(data.message);
        },
        error: function (response) {
            alert(response.message);
        }


    });


})
