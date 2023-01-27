$(document).ready(function () {
    $('#menuitem_data').DataTable(
        {
            "ajax": {
                "url": "/api/MenuItem",
                "type": "GET",
                "datatype":"json"
            },
            "columns": [
                { "data": "name", "width": "20%" },
                { "data": "price", "width": "20%" },
                { "data": "category.name", "width": "20%" },
                { "data": "foodType.name", "width": "20%" },
                {
                    "data": "id",
                    "render": function (ff) {
                        return `
                            <div>
                                <a href="/Admin/MenuItems/Upsert?id=${ff}" class="btn btn-success"><i class="bi bi-pencil-square"></i>Edit</a>
                                <a href="/Admin/MenuItems/Upsert?id=${ff}" class="btn btn-danger"><i class="bi bi-trash-fill"></i>Edit</a>
                            </div>
                        `
                    }
                }
            ],
            "width":"100%"
        }
    );
});