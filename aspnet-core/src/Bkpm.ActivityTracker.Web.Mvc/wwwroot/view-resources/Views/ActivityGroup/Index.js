(function ($) {
    var _ActivityGroupService = abp.services.app.activityGroup,
        l = abp.localization.getSource('ActivityTracker'),
        _$modal = $('#ActivityGroupCreateModal'),
        _$table = $('#ActivityGroupsTable');

    var _$ActivityGroupsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [[2, 'desc']],
        listAction: {
            ajaxFunction: _ActivityGroupService.getAll,
            inputFilter: function () {
                return $('#searchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$ActivityGroupsTable.draw(false)
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
                data: 'nama',
                sortable: true,
            },
            {
                targets: 2,
                data: 'activityAudit',
                sortable: true,
                render: data => moment(data).format('DD MMM YYYY hh:mm:ss') 
            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-ActivityGroup" data-entity-id="${row.id}" data-toggle="modal" data-target="#ActivityGroupCreateModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-ActivityGroup" data-entity-id="${row.id}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    _$modal.on('click', '.save-button', function (e) {
    //_$form.find('.save-button').click(function (e) {
        var _$form = _$modal.find('form');
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var ActivityGroup = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        _ActivityGroupService
            .createOrUpdate(ActivityGroup)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$ActivityGroupsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-ActivityGroup', function () {
        var ActivityGroupId = $(this).attr('data-entity-id');

        deleteActivityGroup(ActivityGroupId);
    });

    $(document).on('click', '.edit-ActivityGroup', function (e) {
        var ActivityGroupId = $(this).attr('data-entity-id');

        abp.ajax({
            url: abp.appPath + 'ActivityGroup/CreateOrEditModal?Id=' + ActivityGroupId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ActivityGroupCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    function deleteActivityGroup(ActivityGroupId, tenancyName) {
        abp.message.confirm(
            'Yakin ingin dihapus?',
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _ActivityGroupService
                        .delete({
                            id: ActivityGroupId
                        })
                        .done(() => {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            _$ActivityGroupsTable.ajax.reload();
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
        _$ActivityGroupsTable.ajax.reload();
    });

    $('.btn-clear').on('click', (e) => {
        $('input[name=Keyword]').val('');
        _$ActivityGroupsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$ActivityGroupsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
