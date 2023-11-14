$('#dalolatnomaTable').DataTable({
    "language": {

        "processing": "Подождите...",
        "search": "Поиск:",
        "lengthMenu": "Показать _MENU_ записей",
        "info": "Записи с _START_ до _END_ из _TOTAL_ записей",
        "infoEmpty": "Записи с 0 до 0 из 0 записей",
        "infoFiltered": "(отфильтровано из _MAX_ записей)",
        "infoPostFix": "",
        "loadingRecords": "Загрузка записей...",
        "zeroRecords": "Записи отсутствуют.",
        "emptyTable": "В таблице отсутствуют данные",
        "paginate": {
            "first": "Первая",
            "previous": "Предыдущая",
            "next": "Следующая",
            "last": "Последняя"
        },
        "aria": {
            "sortAscending": ": активировать для сортировки столбца по возрастанию",
            "sortDescending": ": активировать для сортировки столбца по убыванию"
        },
        "select": {
            "rows": {
                "0": "Кликните по записи для выбора",
                "1": "Выбрана одна запись",
                "_": "Выбрано записей: %d"
            }
        }
    },
    "ajax": {
        "url": "/Dalolatnomalar/GetDalolatnomalar/",
        "type": "GET",
        "datatype": "json"
    },
    "responsive": true, "autoWidth": false,
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            text: '<i class="fas fa-cloud-download-alt"></i>  Скачать'
        },

    ],
    columns: [
        { data: 'id' },
        { data: 'actNumberDate' },
        { data: 'sellerCompany' },
        { data: 'buyerCompany' },
        { data: 'gasMeterNetwork' },
        { data: 'gasAmount' },
        { data: 'beginDateEndDate' },
        {
            data: 'sendStatus', render: function (data, type, row) {

                if (data == 'SendSuccessfully') {
                    return '<i class="fa fa-check-circle text-green"></i> Юборилган'
                } else {
                    return '<small class="badge badge-primary"><i class="far fa-clock"></i> Янги</small>'
                }
            }
        },

        {
            data: "id",
            "render": function (data) {
                var editA = "", deleteA = "";
                editA = `<a href="/Dalolatnomalar/Details/${data}" class="btn btn-outline-secondary"><i class="fas fa-eye"></i></a>`;
                /*  deleteA = ` <a href="/Fields/delete?id=${data}" class="btn btn-outline-danger" @asp-route-id="${data}"><i class="fas fa-trash"></i></a>`;*/
              /*  if (edit) {

                }
                if (dele) {

                }*/
                return editA + "" + deleteA;
            }
        }
    ],
    /* initComplete: function () {
         this.api().columns([2]).every(function () {
             var column = this;
             var select = $('<select class="form-control"><option value=""></option></select>')
                 .appendTo($(column.footer()).empty())
                 .on('change', function () {
                     var val = $.fn.dataTable.util.escapeRegex(
                         $(this).val()
                     );

                     column
                         .search(val ? '^' + val + '$' : '', true, false)
                         .draw();
                 });
             column.data().unique().sort().each(function (d, j) {
                 select.append('<option value="' + d + '">' + d + '</option>')
             });
         });
     }*/
});