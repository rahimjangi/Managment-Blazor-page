var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled");
    } 
    else if (url.includes("completed")) {
        loadList("completed");
    }
    else if (url.includes("inProcess")) {
        loadList("inProcess");
    }
    else if (url.includes("submitted")) {
        loadList("submitted");
    } else {
        loadAll()
    } 

});

function loadList(param) {
    dataTable = $('#orderListTable').DataTable(
        {
            "ajax": {
                "url": "/api/Order?status=" +param,
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "10%" },
                { "data": "pickUpName", "width": "20%" },
                { "data": "applicationUser.email", "width": "20%" },
                { "data": "orderTotal", "width": "20%" },
                { "data": "pickUpTime", "width": "20%" },
                {
                    "data": "id",
                    "render": function (ff) {
                        return `
                            <div class="w-75 btn-group">
                                <a href="/Admin/Order/OrderDetails?id=${ff}" class="btn btn-success"><i class="bi bi-pencil-square"></i>Edit</a>
                            </div>
                        `
                    }
                }
            ],
            "width": "100%"
        }
    );
}


function loadAll() {
    dataTable = $('#orderListTable').DataTable(
        {
            "ajax": {
                "url": "/api/Order",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "10%" },
                { "data": "pickUpName", "width": "20%" },
                { "data": "applicationUser.email", "width": "20%" },
                { "data": "orderTotal", "width": "20%" },
                { "data": "pickUpTime", "width": "20%" },
                {
                    "data": "id",
                    "render": function (ff) {
                        return `
                            <div class="w-75 btn-group">
                                <a href="/Admin/Order/OrderDetails?id=${ff}" class="btn btn-success"><i class="bi bi-pencil-square"></i>Edit</a>
                            </div>
                        `
                    }
                }
            ],
            "width": "100%"
        }
    );
}

