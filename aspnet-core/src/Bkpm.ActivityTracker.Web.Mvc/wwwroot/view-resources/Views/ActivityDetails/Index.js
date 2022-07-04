(function ($) {
    var _activityDetailsService = abp.services.app.activityDetails,
        l = abp.localization.getSource('ActivityTracker'),
        _$modal = $('#ActivityDetailsCreateModal'),
        _$table = $('#ActivityDetailssTable');

    var _$activityDetailssTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [[2, 'desc']],
        listAction: {
            ajaxFunction: _activityDetailsService.getAll,
            inputFilter: function () {
                var obj = $('#searchForm').serializeFormToObject(true);
                console.log(obj);
                return obj;
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$activityDetailssTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'tanggalKegiatan',
                sortable: true,
                render: data => moment(data).format('DD-MMM-YYYY')
            },
            {
                targets: 2,
                data: 'activityGroup.nama',
                sortable: true,
            },
            {
                targets: 3,
                data: 'nama',
                sortable: true,
            },
            {
                targets: 4,
                data: 'deskripsi',
                sortable: true,
            },
            {
                targets: 5,
                data: 'progress',
                sortable: true,
                render: data => data + "%"
            },
            {
                targets: 6,
                data: 'activityAudit',
                sortable: true,
                render: data => moment(data).format('DD-MMM-YYYY hh:mm:ss') 
            },
            {
                targets: 7,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-info view-activityDetails" data-entity-id="${row.id}">`,
                        `       <i class="fas fa-eye"></i> View`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-secondary edit-activityDetails" data-entity-id="${row.id}" data-toggle="modal" data-target="#ActivityDetailsCreateModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-activityDetails" data-entity-id="${row.id}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    _$modal.on('click', '.save-button', function (e) {
        var _$form = _$modal.find('form');
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var activityDetails = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        _activityDetailsService
            .createOrUpdate(activityDetails)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$activityDetailssTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-activityDetails', function () {
        var activityDetailsId = $(this).attr('data-entity-id');

        deleteActivityDetails(activityDetailsId);
    });

    $(document).on('click', '.edit-activityDetails', function (e) {
        var activityDetailsId = $(this).attr('data-entity-id');

        abp.ajax({
            url: abp.appPath + 'ActivityDetails/CreateOrEditModal?Id=' + activityDetailsId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ActivityDetailsCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', '.view-activityDetails', function (e) {
        var activityDetailsId = $(this).attr('data-entity-id');

        window.location.href = abp.appPath + 'ActivityDetails/View?Id=' + activityDetailsId;

    });

    abp.event.on('activityDetails.edited', (data) => {
        _$activityDetailssTable.ajax.reload();
    });

    function deleteActivityDetails(activityDetailsId, tenancyName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                tenancyName
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _activityDetailsService
                        .delete({
                            id: activityDetailsId
                        })
                        .done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            _$activityDetailssTable.ajax.reload();
                        });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        var _$form = _$modal.find('form');
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$activityDetailssTable.ajax.reload();
    });

    $('.btn-clear').on('click', (e) => {
        $('input[name=Keyword]').val('');
        _$activityDetailssTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$activityDetailssTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
