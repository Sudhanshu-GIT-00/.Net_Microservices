var dataTable;

$(function () {
    GetDate(); BindingYears();
    var url = window.location.search;

    if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else if (url.includes("dailyOrderId")) {
        SortOrderListDateWise();
    }
    else if (url.includes("monthlyOrderId")) {
        SortOrderListDateWise("monthlyOrderId");
    }
    else if (url.includes("yearlyOrderId")) {
        SortOrderListDateWise("yearlyOrderId");
    }
    else {
        if (url.includes("readyforpickup")) {
            loadDataTable("readyforpickup");
        }
        else {
            if (url.includes("cancelled")) {
            }
            else {
                loadDataTable("all")
            }
        }
    }
    $("#txtBtn").on("click", function () {
        if (url.includes("monthlyOrderId")) {
            SortOrderListDateWise("monthlyOrderId");
        }
        else if (url.includes("yearlyOrderId")) {
            SortOrderListDateWise("yearlyOrderId") 
        }
        else {
            SortOrderListDateWise();
        }

    });
});

function BindingYears() {
    var ddlYears = $("#ddlYears");
    var currentYear = (new Date()).getFullYear();
    for (var i = 2020; i <= currentYear; i++) {
        var option = $("<option />");
        option.html(i);
        option.val(i);
        ddlYears.append(option);
    }
    ddlYears.val(currentYear);
}
function GetDate() {
    const currentDate = new Date();
    const formattedDate = currentDate.toISOString().split('T')[0];
    var year = currentDate.getFullYear();
    var month = currentDate.getMonth();
    var mm = month + 1;
    mm = '0' + mm;
    var days = currentDate.getDate();
    var dates = year + '/' + mm + '/' + days;
    $("#ddlMonths").val(mm);
    $("#txtDatePicker").val(dates);
    $("#txtDatePicker").datepicker({
        dateFormat: "yy/mm/dd"
    });
}
function loadDataTable(status) {

    $.ajax({
        url: "/order/getall?status=" + status,
        datatype: "json",
        success: function (response) {
            bindTable(response);
        }
    })
}

function SortOrderListDateWise(condtionalUrl) {
    var url = "";
    if (condtionalUrl == "monthlyOrderId") {
        url = "/order/getall?orderMonth=" + $("#ddlMonths").val() + "&orderYear=" + $("#ddlYears").val();
    }
    else if (condtionalUrl == "yearlyOrderId") {
        url = "/order/getall?orderYear=" + $("#ddlYears").val();
    }
    else {
        url = "/order/getall?orderDate=" + $("#txtDatePicker").val()
    }
    $.ajax({
        url: url,
        datatype: "json",
        success: function (response) {
            bindTable(response);
        }
    })
}

function bindTable(response) {
    $('#tblData').DataTable({
        "autoWidth": false,
        "bDestroy": true,
        "data": response.data,
        "columns": [
            { data: 'orderHeaderId', "width": "5%" },
            { data: 'email', "width": "25%" },
            { data: 'name', "width": "20%" },
            { data: 'phone', "width": "10%" },
            { data: 'status', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'orderTime', "width": "10%",
                "render": function (data) {
                    return convertToDate(data);
                }
            },
            {
                data: 'orderHeaderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/order/orderDetail?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}

function convertToDate(data) {
    // Format using moment.js.
    var dtStartWrapper = moment(data);
    return dtStartWrapper.format("YYYY/MM/DD");
}
