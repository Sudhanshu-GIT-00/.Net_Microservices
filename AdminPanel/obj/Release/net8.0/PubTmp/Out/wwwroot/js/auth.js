
$(function myfunction() {
    $("#btnCreateScrty").on('click', function () {
        if ($("txtSecurityQstn").val() !== "") {
            CreateSecurityQuestion();
        } else {
            $("#spnSecurtyQstn").html("Security Questions is required")
        }
    });
})

function CreateSecurityQuestion() {

    $.ajax({
        url: "/Auth/UpsertSecurityQuestion",
        datatype: "json",
        type: "POST",
        success: function (response) {
            BindSecurityTable(response);
        }
    })
}

function BindSecurityTable(response) {
    $.ajax({
        url: "/Auth/UpsertSecurityQuestion",
        datatype: "json",
        type: "POST",
        success: function (response) {
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
    })
}