var table;
$(function () {
    table = $('#tblData').DataTable({
        ajax: {
            url: '/Admin/Product/GetAll', // dùng đường dẫn tuyệt đối
            dataSrc: 'data',
            error: function (xhr) {
                console.error('Ajax error', xhr.status, xhr.responseText);
            }
        },
        columns: [
            { data: 'title', width: '20%' },
            { data: 'isbn', width: '12%' },
            { data: 'author', width: '10%' },
            { data: 'category.name', width: '15%' },
            { data: 'coverType.name', width: '15%' },
            { data: 'price50', width: '10%' },
            {
                data: 'imageUrl',
                width: '10%',
                orderable: false,
                render: function (url) {
                    return `<img src="${url}" style="width:40px;height:40px;object-fit:cover;border-radius:4px">`;
                }
            },
            {
                data: 'id',
                width: '8%',
                orderable: false,
                render: function (id) {
                    return `
                    <tr>
                        <td>
                         <div>
                         <a href="/Admin/Product/Upsert?id=${id}"
                                class="btn btn-primary">
                                <i class="bi bi-pencil"></i>
                                Edit
                        </a>

                         <a onClick=Delete('/Admin/Product/DeletePostApi/${id}') class="btn btn-danger">
                           <i class="bi bi-trash3"></i>Delete</a>
                        </div>
                        </td>
                    </tr>
                    `;
                }
            }

        ]
    });

    console.log('Ajax URL:', table.ajax.url());
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
                type: "DELETE",
                success: function (data) {

                    //loadDatatable();
                    
                    if (data.success) {
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message)
                    }
                }
            })
        }
    })
}