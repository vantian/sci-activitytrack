(function ($) {
    var _activityDetailsService = abp.services.app.activityDetails,
        l = abp.localization.getSource('ActivityTracker'),
        _$modal = $('#ActivityFileCreateModal'),
        _$table = $('#LampiranTable'),
        _activityDetailId = $('#activityDetailId-hidden').val();

    var _$activityDetailssTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [[2, 'desc']],
        listAction: {
            ajaxFunction: _activityDetailsService.getLampiranByActivityId,
            inputFilter: function () {
                return { activityDetailId: _activityDetailId };
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
                data: 'nama',
                sortable: true
            },
            {
                targets: 2,
                data: 'creationTime',
                sortable: true,
                render: data => moment(data).format('DD-MMM-YYYY')
            },
            {
                targets: 3,
                data: 'creatorName',
                sortable: true
            },
            {
                targets: 4,
                data: 'url',
                sortable: true,
                render: data => `<a href="${data}" target="_blank">Download File</a>`
            },
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
            .createLampiranByActivityId(activityDetails)
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
                $('#ActivityFileCreateModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
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
