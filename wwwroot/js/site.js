var sortedMode = localStorage.getItem("sortMode") || "numeric";
var table;

$(".dt").datepicker({
    format: "dd/mm/yyyy",
    autoclose: true,
    todayHighlight: true
});
$('#calendarIcon').on('click', function () {
    $('.dt').datepicker('show');
});

$('.advanceSearchBtn').hover(function () {
    var data = "Advance Search asdfjkalfds, asdf jkldslfdlsafjdj, l";
    var tooltipContent = data.split(',').join('');

    $(this).attr('title', tooltipContent).tooltip({
        content: function () {
            return $(this).attr('title');
        }
    });
});

$('.logOutBtn').hover(function()  {
    $(this).attr('title', "Logout");
})



$(document).ready(function () {
    BindList(); 
    $.fn.dataTable.ext.type.order['numeric-asc'] = function (a, b) {
             var aVal = a || ''; // Get text and trim whitespace
            var bVal = b || ''; // Get text and trim whitespace

            var aIsBlank = aVal === '';
            var bIsBlank = bVal === '';
            var aIsNum = !isNaN(parseFloat(aVal));
            var bIsNum = !isNaN(parseFloat(bVal));

            // If a is blank and b is not, a should be after b
            if (aIsBlank && !bIsBlank) return 1;

            // If b is blank and a is not, b should be after a
            if (!aIsBlank && bIsBlank) return -1;

            // If both are blank, they are equal
            if (aIsBlank && bIsBlank) return 0;

            // If a is a number and b is not, a should come first
            if (aIsNum && !bIsNum) return -1;

            // If b is a number and a is not, b should come first
            if (!aIsNum && bIsNum) return 1;

            // If both are numbers, compare them numerically
            if (aIsNum && bIsNum) {
                return parseFloat(aVal) - parseFloat(bVal);
            }

            // If both are non-numeric, use string comparison
            return aVal.localeCompare(bVal);

        };

    $.fn.dataTable.ext.type.order['numeric-desc'] = function (a, b) {

        var aVal = a || ''; // Get text and trim whitespace
        var bVal = b || ''; // Get text and trim whitespace

        var aIsBlank = aVal === '';
        var bIsBlank = bVal === '';
        var aIsNum = !isNaN(parseFloat(aVal)) && isFinite(aVal);
        var bIsNum = !isNaN(parseFloat(bVal)) && isFinite(bVal);

        // If a is blank and b is not, a should be after b
        if (aIsBlank && !bIsBlank) return 1;

        // If b is blank and a is not, b should be after a
        if (!aIsBlank && bIsBlank) return -1;

        // If both are blank, they are equal
        if (aIsBlank && bIsBlank) return 0;

        // If both are numbers, compare them numerically in descending order
        if (aIsNum && bIsNum) {
            return parseFloat(bVal) - parseFloat(aVal);
        }

        // If a is a number and b is a string, string should come after number
        if (aIsNum && !bIsNum) return -1;

        // If b is a number and a is a string, number should come after string
        if (!aIsNum && bIsNum) return 1;

        // If both are strings, use string comparison in ascending order
        return aVal.localeCompare(bVal);
    };

    $.fn.dataTable.ext.type.order['alphanumeric-asc'] = function (a, b) {
        var aVal = a || ''; // Get text and handle undefined/null
        var bVal = b || ''; // Get text and handle undefined/null

        var aIsBlank = aVal.trim() === '';
        var bIsBlank = bVal.trim() === '';
        var aIsNum = !isNaN(parseFloat(aVal)) && !isNaN(aVal - 0);
        var bIsNum = !isNaN(parseFloat(bVal)) && !isNaN(bVal - 0);

        // Handle blanks separately
        if (aIsBlank && !bIsBlank) return 1;  // a is blank, b is not
        if (!aIsBlank && bIsBlank) return -1; // b is blank, a is not
        if (aIsBlank && bIsBlank) return 0;   // both are blank

        // Prioritize strings over numbers and blanks
        if (!aIsNum && bIsNum) return -1; // a is a string, b is a number
        if (aIsNum && !bIsNum) return 1;  // a is a number, b is a string

        if (aIsNum && bIsNum) {
            return parseFloat(aVal) - parseFloat(bVal);
        }
        // Compare strings in ascending order
        if (!aIsNum && !bIsNum) {
            return aVal.localeCompare(bVal);
        }

        return 0;
    }

    $.fn.dataTable.ext.type.order['alphanumeric-desc'] = function (a, b) {
        var aVal = a || ''; // Get text and trim whitespace
        var bVal = b || ''; // Get text and trim whitespace

        var aIsBlank = aVal === '';
        var bIsBlank = bVal === '';

        // If a is blank and b is not, a should be after b
        if (aIsBlank && !bIsBlank) return 1;

        // If b is blank and a is not, b should be after a
        if (!aIsBlank && bIsBlank) return -1;

        // If both are blank, they are equal
        if (aIsBlank && bIsBlank) return 0;

        // If both are alphanumeric strings, compare them in descending order
        return bVal.localeCompare(aVal);
    }
   
   
});

function BindList() {

     table = $('#EmployeeTbl').DataTable({
            
            "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
            "async": false,
            "searching": true,
            "serverside": true,
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
                    "data": function (row, data) {

                        if (row.city == null || row.city == "") {
                            return '';
                        }
                        else {
                            return row.city;
                        }
                    }
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
                { "targets": 3, "type": sortedMode },
                {
                    "targets": "_all",
                    "createdCell": function (cell) {
                        $(cell).css('vertical-align', 'middle');
                    }
                },
                { "className": "text-center", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8] },
                { "targets": [9], "orderable": false }
            ],
            "createdRow": function (row, data, index) {
              if(data.departmentName == "IT"){
                    $(row).css('color', 'purple');
                }
            }
        });
   
   
    $('#btnToggle').off('click').on('click', function (e) {

        //table = $('#EmployeeTbl').DataTable();
        e.stopPropagation();
        if (sortedMode == "numeric") {
            sortedMode = "alphanumeric";
        }
        else {
            sortedMode = "numeric";
        }

        localStorage.setItem("sortMode", sortedMode);
        table.settings()[0].aoColumns[3].sType = sortedMode;

        // Store the current sort mode in localStorage

        // Update the button text based on the current sort mode
        $(this).text(sortedMode == "numeric" ? "1-9" : "A-Z");

        // Get the current order of the table
        let order = table.order();
        if (order.length > 0 && order[0].length > 1) {
            orderDir = order[0][1]; // Use the direction from the first order array
        }

        //let orderDir = order.length > 0 ? order[0][1] : 'asc'; 
        if (orderDir) {
            table.order([3, orderDir]).draw();
        }
        else {
            table.order(3, 'asc').draw();
        }


    });

    
    $('#advanceSearchButton').on('click', function () {
        table.ajax.reload();
    });

    $('#clearButton').on('click', function () {
        $('#FirstName').val('');
        $('#LastName').val('');
        $('#City').val('');
        $('#StateId').val('').trigger('change');
        $('#DepartmentId').val('').trigger('change');
        $('#JoiningDate').val('');
        $('#LeavingDate').val('');
        $('#isEmployeeHasLeft').prop('checked', false);
        table.ajax.reload();
    });

    

}


function fnEditEmployee(id) {
    
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
    
    if (confirm("Are you sure you want to Delete this Record")) {
        $.ajax({
            url: '/Employee/DeleteEmployee',
            type: 'Delete',
            data: { Id: id },
            success: function (data) {
                alert(data.message);
                table.ajax.reload();
            },
            error: function (response) {
                alert(data.message);
            }
        });
    }
}


function checkValidation() {
    
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
            table.ajax.reload(null, false);
            $('#EmployeeModal').modal('hide');
            alert(data.message);
        },
        error: function (response) {
            alert(response.message);
        }


    });


})



