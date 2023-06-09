﻿$(document).ready(function () {
    let Id = 0;
    $('._deleteCategory').click(handleDelItem);
    $('.btn-submit').click(handleSubmit);
    $('._EditStatus').click(handleEditItem);
    $('.btn-submit-edit').click(handleSubmitEdit);
    function handleSubmitEdit() {
        $.ajax({
            url: '/Admin/Category/ChangeStatus',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    $('#editStatus').modal('hide');
                    var classStatus = "._status-" + Id;
                    if ($(classStatus).hasClass("fa-check")) {
                        $(classStatus).removeClass("fa-check color-tick");
                        $(classStatus).addClass("fa-xmark color-xmask");
                    }
                    else {
                        $(classStatus).removeClass("fa-xmark color-xmask");
                        $(classStatus).addClass("fa-check color-tick");
                    }
                }
            }
            })
    }
    function handleEditItem() {
        Id = $(this).data('id');
    }
    function handleSubmit() {

        $.ajax({
            url: '/Admin/Category/Delete',
            type: 'Post',
            dataType: 'JSON',
            data: {
                id: Id
            },
            success: function (res) {
                if (res.status == true) {
                    $('#deleteCategory').modal('hide');
                    var item = ".item-" + Id;
                    $(item).html('');
                }
            }
        })
    }
    function handleDelItem() {
        Id = $(this).data('id');
    }
})