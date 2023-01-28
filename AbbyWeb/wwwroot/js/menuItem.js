var dataTable;
$(document).ready(function () {
    dataTable=$('#menuitem_data').DataTable(
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
                                <a onClick=Delete('/api/MenuItem/'+${ff}) class="btn btn-danger"><i class="bi bi-trash-fill"></i>Delete</a>
                            </div>
                        `
                    }
                }
            ],
            "width":"100%"
        }
    );
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }

             })
        }
    })
}