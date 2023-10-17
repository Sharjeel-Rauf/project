// Add Employee
function addEmployee() {
    var name = $('#empName').val();
    var age = $('#empAge').val();
    var department = $('#empDept').val();

    var employee = {
        name: name,
        age: age,
        departmentID: department
    };

    $.ajax({
        url: 'https://localhost:7186/api/employees',
        type: 'POST',
        data: JSON.stringify(employee),
        contentType: 'application/json',
        success: function() {
            // Clear form
            $('#employeeForm')[0].reset();
            // Update Employee List
            fetchEmployees();
        }
    });
}

// Add Department
function addDepartment() {
    var name = $('#deptName').val();

    var department = {
        name: name
    };

    $.ajax({
        url: 'https://localhost:7186/api/departments',
        type: 'POST',
        data: JSON.stringify(department),
        contentType: 'application/json',
        success: function() {
            // Clear form
            $('#departmentForm')[0].reset();
            // Update Department List
            fetchDepartments();
        }
    });
}

// Fetch Employees
function fetchEmployees() {
    $.ajax({
        url: 'https://localhost:7186/api/employees',
        type: 'GET',
        success: function(data) {
            $('#employeeList').empty();
            data.forEach(function(employee) {
                $('#employeeList').append('<li class="list-group-item">' + employee.name + ', ' + employee.age + ', ' + employee.departmentID + '</li>');
            });
        }
    });
}

// Fetch Departments
function fetchDepartments() {
    $.ajax({
        url: 'https://localhost:7186/api/departments',
        type: 'GET',
        success: function(data) {
            $('#departmentList').empty();
            data.forEach(function(department) {
                $('#departmentList').append('<li class="list-group-item">' + department.name + '</li>');
            });
        }
    });
}

// Initial fetch
$(document).ready(function() {
    fetchEmployees();
    //fetchDepartments();
});











