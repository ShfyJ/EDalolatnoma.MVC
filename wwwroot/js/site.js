$(document).ready(function () {
    var create, edit,dele, view;
   
        $(function () {
            $('.nav .nav-item .nav-link').on('click', function () {
                console.log(this.href);
                var $parent = $(this).addClass('active');
                $parent.siblings('.active').find('> a').trigger('click');
                $parent.siblings().removeClass('active').find('li').removeClass('active');
            });
            $('.nav .nav-item .nav-link').each(function () {
                if (this.href === window.location.href) {
            $(this).addClass('active')
               
                }
            });

        });
   
    
    $.getJSON("/Fields/GetPermission/", function (data) {
        create = data.create;
        edit = data.edit;
        dele = data.delete;
        view = data.view;
    });

 

    $('#CompanyId').change(function () {
        var url = "GetFields";
        var ddlsource = "#CompanyId";
        $.getJSON(url, { id: $(ddlsource).val() }, function (data) {
            var items = ' ';
            $("#FieldId").empty();
            items = "<option value=''>Выберите...</option>";
            $.each(data, function (i, field) {
                items += "<option value='" + field.value + "'>" + field.text + "</option>";
            });
            $('#FieldId').html(items);
        });
    });


    $('#FieldId').change(function () {
        var url = "GetWells";
        var ddlsource = "#FieldId";
        $.getJSON(url, { id: $(ddlsource).val() }, function (data) {
            var items = ' ';
            $("#Well_id").empty();
            items = "<option value=''>Выберите...</option>";
            $.each(data, function (i, field) {
                items += "<option value='" + field.value + "'>" + field.text + "</option>";
            });
            $('#Well_id').html(items);
        });
    });

    $('#kerno').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Russian.json"
        },
        "ajax": {
            "url": "/KernoDatabase/GetKernoData/",
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
            { data: 'well.wellName' },
            { data: 'well.field.fieldName' },
            { data: 'well.field.company.companyShortName' },
            { data: 'interval' },
            { data: 'core_raise' },
            {
                data: 'date_Selection',
                "render": function (data) {
                    return `${moment(data).format('L')} г.`;
                }

            },
            { data: 'well.wellStatus.statusName' },
            {
                data: "id",
                "render": function (data) {
                    return `<a href="/KernoDatabase/Details?id=${data}" class="btn btn-sm btn-info"><i class="fa fa-eye"></i></a>`;
                }
            }
            
        ],
          
             initComplete: function () {
             this.api().columns([0,1,2]).every(function () {
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
         }

    });
    $('#fieldsTable').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Russian.json"
        },
        "ajax": {
            "url": "/Fields/GetFields/",
            "type": "GET",
            "datatype": "json"
        },
        "responsive": true, "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Экспорт <i class="far fa-file-excel"></i>'
            },

        ],
        columns: [
            
            { data: 'fieldName' },
            { data: 'company.companyFullName' },
            {
                data: "id",
                "render": function (data) {
                    var editA="", deleteA="";
                    if (edit) {
                        editA = `<a href="/Fields/Edit?id=${data}" class="btn btn-outline-secondary"><i class="fas fa-edit"></i></a>`;
                    }
                    if (dele) {
                        deleteA = ` <a href="/Fields/delete?id=${data}" class="btn btn-outline-danger" @asp-route-id="${data}"><i class="fas fa-trash"></i></a>`;
                    }
                    return editA + "" + deleteA;
                }
            }
        ],
        initComplete: function () {
            this.api().columns([1]).every(function () {
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
        }
    });


  











    $('#usersTable').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Russian.json"
        },
        "responsive": true, "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Экспорт <i class="far fa-file-excel"></i>'
            },

        ],
       
    });
    $('#wellsTable').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Russian.json"
        },
        "ajax": {
            "url": "/Wells/GetWells/",
            "type": "GET",
            "datatype": "json"
        },
        "responsive": true, "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Экспорт <i class="far fa-file-excel"></i>'
            },

        ],
        columns: [
            { data: 'wellName' },{ data: 'field.fieldName' },
            { data: 'field.company.companyShortName' }, { data: 'wellStatus.statusName' },
            {
                data: "id",
                "render": function (data) {
                    return `<a href="/Wells/Edit?id=${data}" class="btn btn-outline-secondary"><i class="fas fa-edit"></i></a>
                            <a href="/Wells/Delete?id=${data}" class="btn btn-outline-danger" @asp-route-id="${data}"><i class="fas fa-trash"></i></a>`;
                }
            }
        ],
        initComplete: function () {
            this.api().columns([1,2,3]).every(function () {
                var column = this;
                var select = $('<select class="form-control"><option value="">Все</option></select>')
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
        }

    });

});
